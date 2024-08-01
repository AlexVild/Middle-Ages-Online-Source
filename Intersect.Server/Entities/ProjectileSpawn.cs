using System;
using System.Collections.Generic;

using Intersect.GameObjects;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.Maps;
using Intersect.Server.Database;
using Intersect.Enums;
using Intersect.Utilities;
using System.Linq;
using Intersect.Server.Networking;

namespace Intersect.Server.Entities
{

    public partial class ProjectileSpawn
    {

        public byte Dir;

        public int Distance;

        public Guid MapId;

        public Projectile Parent;

        public ProjectileBase ProjectileBase;

        public long TransmittionTimer = Timing.Global.Milliseconds;

        public long ProjectileActiveTime;

        public float X;
        
        private float InitX;

        public float Y;
        
        private float InitY;

        public byte Z;

        public bool Dead;

        public Guid MapInstanceId;

        private List<Guid> mEntitiesCollided = new List<Guid>();

        public int EntitiesHit => mEntitiesCollided.Count;

        public int SpawnNumber { get; set; }

        public ProjectileSpawn(
            byte dir,
            byte x,
            byte y,
            byte z,
            Guid mapId,
            Guid mapInstanceId,
            ProjectileBase projectileBase,
            Projectile parent,
            int spawnNumber // Refers to the projectile's quantity attribute this spawn was spawned on
        )
        {
            MapId = mapId;
            MapInstanceId = mapInstanceId;
            X = x;
            InitX = X;
            Y = y;
            InitY = Y;
            Z = z;
            Dir = dir;
            ProjectileBase = projectileBase;
            Parent = parent;
            TransmittionTimer = Timing.Global.Milliseconds +
                                (long) ((float) ProjectileBase.Speed / (float) ProjectileBase.Range);
            ProjectileActiveTime = Timing.Global.Milliseconds + (Options.Instance.Processing.ProjectileUpdateInterval * Options.Instance.Processing.ProjectileTicksUntilDamageInSpawn);
            SpawnNumber = spawnNumber;
        }

        public bool IsAtLocation(Guid mapId, int x, int y, int z)
        {
            return MapId == mapId && X == x && Y == y && Z == z;
        }

        public void AmmoDrop()
        {
            if (Parent.Base == null || !Parent.Base.AmmoDrop || !Parent.Base.RequiresAmmo || Parent.Owner == null)
            {
                return;
            }

            var map = MapController.Get(MapId);
            if (map == null)
            {
                return;
            }

            if (Parent.Owner is Player owner)
            {
                var ownerLuck = owner.GetLuckModifier();
                var randomChance = Randomization.Next(1, 100001);
                if (randomChance < (Options.AmmoRetrieveChance * 1000) * ownerLuck)
                {
                    if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
                    {
                        var ammoId = owner.GetProjectileAmmoId(Parent.Base);
                        mapInstance.SpawnItem((int)X, (int)Y, new Item(ammoId, 1), 1, Parent.Owner.Id, spawnType: MapInstance.ItemSpawnType.PlayerDeath, ownershipTimeOverride: Options.Instance.LootOpts.PlayerDeathItemDespawnTime);
                    }
                }
            }
        }

        public void PlayDeathAnimation()
        {
            if (Parent == null || Parent.Base == null || Parent.Base.ProjectileDeathAnimation == Guid.Empty)
            {
                return;
            }

            PacketSender.SendAnimationToProximity(Parent.Base.ProjectileDeathAnimation, -1, Guid.Empty, MapId, (byte)Math.Ceiling(X), (byte)Math.Ceiling(Y), (sbyte)Dir, MapInstanceId);
        }

        public bool HitEntity(Entity en)
        {
            bool projectileCantDamageYet = Timing.Global.Milliseconds < ProjectileActiveTime && InitX == X && InitY == Y;
            if (en == null ||
                Parent.Base == null ||
                en is EventPageInstance ||
                en == Parent.Owner ||
                projectileCantDamageYet)
            {
                return false;
            }
            
            var targetEntity = en;
            if (targetEntity is Player deadPlayer && deadPlayer.PlayerDead)
            {
                // Don't bother with players who are dead but haven't respawned - go right through
                return false;
            }

            // Have we collided with this entity before? If so, cancel out.
            if (mEntitiesCollided.Contains(targetEntity.Id) || Parent.ProjectileCollidedOnQuantity(SpawnNumber, targetEntity.Id))
            {
                if (!Piercing)
                {
                    var mapPassable = Options.Instance.Passability.Passable[(int)en.Map.ZoneType];
                    if (en is Player collidingPlayer)
                    {
                        // Collision rules for duels
                        mapPassable = mapPassable && !collidingPlayer.InDuel;
                    }

                    var friendlySpell = Parent.Spell?.Combat?.Friendly ?? false;
                    if (mapPassable && !friendlySpell)
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            
            mEntitiesCollided.Add(targetEntity.Id);
            Parent.AddEntityHitOnQuantity(SpawnNumber, targetEntity.Id);

            if (targetEntity is Player player)
            {
                if ((Parent.Spell?.Combat?.Friendly ?? false) || !player.IsAllyOf(Parent.Owner))
                {
                    Parent.Owner.ProjectileAttack(player, Parent, Parent.Spell, Parent.Item, false, Dir);
                }
                if (GrappleAvailable && Parent.Base.AttachToEntities)
                {
                    // If the projectile's spell can't affect the target, ignore
                    if (Parent.Base.Spell != null && !Parent.Owner.CanAttack(player, Parent.Base.Spell))
                    {
                        return false;
                    }
                    Grapple(Dir);
                }

                if (!Piercing)
                {
                    if (Parent.Spell != null)
                    {
                        // Friendly projectiles should never pass through, as they need to take effect.
                        if (Options.Instance.Passability.Passable[(int)targetEntity.Map.ZoneType] && !player.InDuel && !Parent.Spell.Combat.Friendly)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (player.InDuel)
                        {
                            return true;
                        }
                        // If on a passable map, allow passthrough
                        return !Options.Instance.Passability.Passable[(int)targetEntity.Map.ZoneType];
                    }
                }

                return false;
            }

            if (targetEntity is Npc npc)
            {
                // Handle NPC vs NPC
                var ownerNpc = Parent.Owner as Npc;
                if (ownerNpc == null || ownerNpc.CanNpcCombat(npc, FriendlySpell))
                {
                    Parent.Owner.ProjectileAttack(npc, Parent, Parent.Spell, Parent.Item, false, Dir);

                    if (GrappleAvailable && Parent.Base.AttachToEntities)
                    {
                        if (Parent.Base.Spell != null && !Parent.Owner.CanAttack(npc, Parent.Base.Spell))
                        {
                            return false;
                        }

                        Grapple(Dir);
                    }
                }

                return !Piercing;
            }

            if (targetEntity is Resource resourceTarget)
            {
                // If the owner is an NPC, don't bother trying to harvest
                if (Parent.Owner as Player != null)
                {
                    Parent.Owner.ProjectileAttack(resourceTarget, Parent, Parent.Spell, Parent.Item, true, Dir);                    
                }

                return !IgnoreResource(resourceTarget);
            }

            return false;
        }
    }

    public partial class ProjectileSpawn
    {
        public bool ValidGrappleDir => Dir <= 3;

        public bool GrappleAvailable => ValidGrappleDir && 
            (Parent?.Base?.GrappleHook ?? false) && 
            (!Parent?.HasGrappled ?? false);

        public bool Piercing => Parent?.Base?.PierceTarget ?? false;

        public bool FriendlySpell => Parent?.Spell?.Combat?.Friendly ?? false;

        public void Grapple(byte dir)
        {
            Parent.HasGrappled = true;

            Parent.Owner.Dir = dir;
            new Dash(
                Parent.Owner, Distance, dir, Parent.Base.IgnoreMapBlocks,
                Parent.Base.IgnoreActiveResources, Parent.Base.IgnoreExhaustedResources,
                Parent.Base.IgnoreZDimension
            );
        }

        public bool IgnoreResource(Resource resource)
        {
            if (ProjectileBase == null)
            {
                return false;
            }

            if (resource == null || resource.Base == null)
            {
                return true;
            }

            if (resource.Base.WalkableBefore)
            {
                return true;
            }

            if (resource.Base.WalkableAfter && resource.IsDead())
            {
                return true;
            }

            if (resource.IsDead() && ProjectileBase.IgnoreExhaustedResources)
            {
                return true;
            }

            if (!resource.IsDead() && ProjectileBase.IgnoreActiveResources)
            {
                return true;
            }

            return false;
        }
    }

}
