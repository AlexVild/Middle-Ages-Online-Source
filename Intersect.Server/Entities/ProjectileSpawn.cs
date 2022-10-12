using System;
using System.Collections.Generic;

using Intersect.GameObjects;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Networking;
using Intersect.Server.Maps;
using Intersect.Server.Database;
using Intersect.Enums;
using Intersect.Utilities;

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

        public ProjectileSpawn(
            byte dir,
            byte x,
            byte y,
            byte z,
            Guid mapId,
            Guid mapInstanceId,
            ProjectileBase projectileBase,
            Projectile parent
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
        }

        public bool IsAtLocation(Guid mapId, int x, int y, int z)
        {
            return MapId == mapId && X == x && Y == y && Z == z;
        }

        public void AmmoDrop()
        {
            var map = MapController.Get(MapId);
            if (map != null && Parent.Base.AmmoItemId != Guid.Empty && Parent.Owner is Player owner)
            {
                if (owner != null)
                {
                    var ownerLuck = 1 + owner.GetEquipmentBonusEffect(EffectType.Luck) / 100f;
                    var randomChance = Randomization.Next(1, 100001);
                    if (randomChance < (Options.AmmoRetrieveChance * 1000) * ownerLuck)
                    {
                        if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
                        {
                            mapInstance.SpawnItem((int)X, (int)Y, new Item(Parent.Base.AmmoItemId, 1), 1, Parent.Owner.Id);
                        }
                    }
                }
            }
        }

        public bool HitEntity(Entity en)
        {
            var targetEntity = en;
            if (targetEntity is EventPageInstance || en == null) return false;

            bool projectileCantDamageYet = Timing.Global.Milliseconds < ProjectileActiveTime && InitX == X && InitY == Y;
            if (projectileCantDamageYet)
            {
                return false;
            }

            var scalingStat = Enums.Stats.StatCount;

            if (Parent.Spell != null && Parent.Spell.Combat != null)
            {
                scalingStat = (Enums.Stats) Parent.Spell.Combat.ScalingStat;
            }
            if (Parent.Item != null)
            {
                scalingStat = (Enums.Stats) Parent.Item.ScalingStat;
            }

            if (targetEntity is Player player && player.PlayerDead)
            {
                return false;
            }

            if (targetEntity != null && targetEntity != Parent.Owner)
            {
                // Have we collided with this entity before? If so, cancel out.
                if (mEntitiesCollided.Contains(targetEntity.Id))
                {
                    if (!Parent.Base.PierceTarget)
                    {
                        if ((en != null && Options.Instance.Passability.Passable[(int)en.Map.ZoneType]) && (Parent.Spell != null && !Parent.Spell.Combat.Friendly))
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

                if (targetEntity.GetType() == typeof(Player)) //Player
                {
                    if (Parent.Owner != Parent.Target)
                    {
                        Parent.Owner.TryAttack(targetEntity, Parent.Base, Parent.Spell, Parent.Item, Dir);
                        // Do not grapple players - commented out

                        /*
                        if (Dir <= 3 && Parent.Base.GrappleHook && !Parent.HasGrappled
                        ) //Don't handle directional projectile grapplehooks
                        {
                            Parent.HasGrappled = true;
                            Parent.Owner.Dir = Dir;
                            new Dash(
                                Parent.Owner, Distance, (byte) Parent.Owner.Dir, Parent.Base.IgnoreMapBlocks,
                                Parent.Base.IgnoreActiveResources, Parent.Base.IgnoreExhaustedResources,
                                Parent.Base.IgnoreZDimension
                            );
                        }
                        */

                        if (!Parent.Base.PierceTarget)
                        {
                            if (Parent.Spell != null)
                            {
                                // Friendly projectiles should never pass through, as they need to take effect.
                                if (Options.Instance.Passability.Passable[(int)targetEntity.Map.ZoneType] && !Parent.Spell.Combat.Friendly)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            } else
                            {
                                // If on a passable map, allow passthrough
                                return !Options.Instance.Passability.Passable[(int)targetEntity.Map.ZoneType];
                            }
                        }
                    }
                }
                else if (targetEntity.GetType() == typeof(Resource))
                {
                    var resourceTarget = targetEntity as Resource;

                    if (ProjectileBase.Tool != -1 || resourceTarget.Base.Tool == -1) // if the projectile can be handled as a tool or the resource does not demand a tool, do some things differently
                    {
                        // If the projectile is the right tool for the job, make an attack
                        var validTool = false;
                        if (!((Resource)targetEntity).IsDead() && (ProjectileBase.Tool == ((Resource)targetEntity).Base.Tool)) 
                        {
                            Parent.Owner.TryAttack(targetEntity, Parent.Base, Parent.Spell, Parent.Item, Dir);
                            validTool = true;
                        }
                        // then, determine if the projectile spawn should die

                        if (Parent.Base.PierceTarget)
                        {
                            return false;
                        }
                        else
                        {
                            if (validTool)
                            {
                                return true;
                            } 
                            else
                            {
                                return ((Resource)targetEntity).IsDead() && !ProjectileBase.IgnoreExhaustedResources ||
                                    !((Resource)targetEntity).IsDead() && !ProjectileBase.IgnoreActiveResources;
                            }
                        }
                    }
                    else
                    {
                        if (((Resource)targetEntity).IsDead() && !ProjectileBase.IgnoreExhaustedResources ||
                        !((Resource)targetEntity).IsDead() && !ProjectileBase.IgnoreActiveResources)
                        {
                            if (Parent.Owner.GetType() == typeof(Player) && !((Resource)targetEntity).IsDead())
                            {
                                Parent.Owner.TryAttack(targetEntity, Parent.Base, Parent.Spell, Parent.Item, Dir);

                                // Do not grapple resources -- commented out

                                /*
                                if (Dir <= 3 && Parent.Base.GrappleHook && !Parent.HasGrappled) //Don't handle directional projectile grapplehooks
                                {
                                    Parent.HasGrappled = true;
                                    Parent.Owner.Dir = Dir;
                                    new Dash(
                                        Parent.Owner, Distance, (byte)Parent.Owner.Dir, Parent.Base.IgnoreMapBlocks,
                                        Parent.Base.IgnoreActiveResources, Parent.Base.IgnoreExhaustedResources,
                                        Parent.Base.IgnoreZDimension
                                    );
                                }
                                */
                            }

                            return true;
                        }
                    }
                }
                else //Any other Parent.Target
                {
                    var ownerNpc = Parent.Owner as Npc;
                    if (ownerNpc == null ||
                        ownerNpc.CanNpcCombat(targetEntity, Parent.Spell != null && Parent.Spell.Combat.Friendly))
                    {
                        Parent.Owner.TryAttack(targetEntity, Parent.Base, Parent.Spell, Parent.Item, Dir);

                        // Do not grapple NPCs - commented out

                        /*
                        if (Dir <= 3 && Parent.Base.GrappleHook && !Parent.HasGrappled
                        ) //Don't handle directional projectile grapplehooks
                        {
                            Parent.HasGrappled = true;
                            Parent.Owner.Dir = Dir;
                            new Dash(
                                Parent.Owner, Distance, (byte) Parent.Owner.Dir, Parent.Base.IgnoreMapBlocks,
                                Parent.Base.IgnoreActiveResources, Parent.Base.IgnoreExhaustedResources,
                                Parent.Base.IgnoreZDimension
                            );
                        }
                        */

                        if (!Parent.Base.PierceTarget)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }

}
