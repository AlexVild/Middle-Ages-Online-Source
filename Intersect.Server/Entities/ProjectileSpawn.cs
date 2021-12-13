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

        public long TransmittionTimer = Globals.Timing.Milliseconds;

        public float X;

        public float Y;

        public byte Z;

        public bool Dead;

        private List<Guid> mEntitiesCollided = new List<Guid>();

        public ProjectileSpawn(
            byte dir,
            byte x,
            byte y,
            byte z,
            Guid mapId,
            ProjectileBase projectileBase,
            Projectile parent
        )
        {
            MapId = mapId;
            X = x;
            Y = y;
            Z = z;
            Dir = dir;
            ProjectileBase = projectileBase;
            Parent = parent;
            TransmittionTimer = Globals.Timing.Milliseconds +
                                (long) ((float) ProjectileBase.Speed / (float) ProjectileBase.Range);
        }

        public bool IsAtLocation(Guid mapId, int x, int y, int z)
        {
            return MapId == mapId && X == x && Y == y && Z == z;
        }

        public void AmmoDrop()
        {
            var map = MapInstance.Get(MapId);
            if (map != null && Parent.Base.AmmoItemId != Guid.Empty && Parent.Owner is Player owner)
            {
                if (owner != null)
                {
                    var ownerLuck = 1 + owner.GetEquipmentBonusEffect(EffectType.Luck) / 100f;
                    var randomChance = Randomization.Next(1, 100001);
                    if (randomChance < (Options.AmmoRetrieveChance * 1000) * ownerLuck)
                    {
                        MapInstance.Get(MapId).SpawnItem((int)X, (int)Y, new Item(Parent.Base.AmmoItemId, 1), 1, Parent.Owner.Id);
                    }
                }
            }
        }

        public bool HitEntity(Entity en)
        {
            var targetEntity = en;
            if (targetEntity is EventPageInstance) return false;

            var scalingStat = Enums.Stats.StatCount;

            if (Parent.Spell != null && Parent.Spell.Combat != null)
            {
                scalingStat = (Enums.Stats) Parent.Spell.Combat.ScalingStat;
            }
            if (Parent.Item != null)
            {
                scalingStat = (Enums.Stats) Parent.Item.ScalingStat;
            }

            if (targetEntity != null && targetEntity != Parent.Owner)
            {

                // Have we collided with this entity before? If so, cancel out.
                if (mEntitiesCollided.Contains(en.Id))
                {
                    if (!Parent.Base.PierceTarget)
                    {
                        if (Options.Instance.Passability.Passable[(int)en.Map.ZoneType] && !Parent.Spell.Combat.Friendly)
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
                mEntitiesCollided.Add(en.Id);

                if (targetEntity.GetType() == typeof(Player)) //Player
                {
                    if (Parent.Owner != Parent.Target)
                    {
                        Parent.Owner.TryAttack(targetEntity, Parent.Base, Parent.Spell, Parent.Item, Dir);
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
                    if (((Resource) targetEntity).IsDead() && !ProjectileBase.IgnoreExhaustedResources ||
                        !((Resource) targetEntity).IsDead() && !ProjectileBase.IgnoreActiveResources)
                    {
                        if (Parent.Owner.GetType() == typeof(Player) && !((Resource) targetEntity).IsDead())
                        {
                            Parent.Owner.TryAttack(targetEntity, Parent.Base, Parent.Spell, Parent.Item, Dir);
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
                        }

                        return true;
                    }
                }
                else //Any other Parent.Target
                {
                    var ownerNpc = Parent.Owner as Npc;
                    if (ownerNpc == null ||
                        ownerNpc.CanNpcCombat(targetEntity, Parent.Spell != null && Parent.Spell.Combat.Friendly))
                    {
                        Parent.Owner.TryAttack(targetEntity, Parent.Base, Parent.Spell, Parent.Item, Dir);
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
