using System;

using Intersect.GameObjects;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Enums;
using Intersect.Server.Maps;
using Intersect.Utilities;
using System.Collections.Generic;

namespace Intersect.Server.Classes.Maps
{

    public partial class MapTrapInstance
    {
        public Guid Id { get; } = Guid.NewGuid();

        private long Duration;

        public Guid MapId;

        public Guid MapInstanceId;

        public AttackingEntity Owner;

        public SpellBase ParentSpell;

        public bool Triggered = false;

        public byte X;

        public byte Y;

        public byte Z;

        public Dictionary<Guid, long> EntitiesHit { get; set; }

        public long StartTime { get; set; }

        /// <summary>
        /// Exists to provide a buffer for when a trap spawns and when it starts doing damage.
        /// </summary>
        public bool IsActive => StartTime < Timing.Global.Milliseconds + 500;

        public MapTrapInstance(AttackingEntity owner, SpellBase parentSpell, Guid mapId, Guid mapInstanceId, byte x, byte y, byte z)
        {
            Owner = owner;
            ParentSpell = parentSpell;
            StartTime = Timing.Global.Milliseconds;
            Duration = Timing.Global.Milliseconds + ParentSpell.Combat.TrapDuration;
            MapId = mapId;
            MapInstanceId = mapInstanceId;
            X = x;
            Y = y;
            Z = z;
            EntitiesHit = new Dictionary<Guid, long>();
        }

        public void CheckEntityHasDetonatedTrap(Entity entity)
        {
            if (Triggered)
            {
                return;
            }

            if (entity == null || entity.MapId != MapId || entity.X != X || entity.Y != Y || entity.Z != Z)
            {
                return;
            }

            if (entity.Id == Owner.Id)
            {
                return;
            }

            var isAlly = entity.IsAllyOf(Owner);

            if (isAlly && !ParentSpell.Combat.Friendly)
            {
                return;
            }

            if (!isAlly && ParentSpell.Combat.Friendly)
            {
                return;
            }

            Detonate(entity);
        }

        protected void Detonate(Entity target)
        {
            if (target == null || target is EventPageInstance || target is Resource)
            {
                return;
            }
            
            if (!ParentSpell.Combat.TrapMultiUse)
            {
                Triggered = true;
                Owner.HandleAoESpell(ParentSpell.Id, target.MapId, target.X, target.Y, null, detonation: true);
            }
            else
            {
                if (EntitiesHit.TryGetValue(target.Id, out var lastHitTimestamp))
                {
                    if (Timing.Global.Milliseconds < lastHitTimestamp)
                    {
                        return;
                    }

                    EntitiesHit[target.Id] = Timing.Global.Milliseconds + ParentSpell.Combat.TrapDamageCooldown;
                    Owner.HandleAoESpell(ParentSpell.Id, target.MapId, target.X, target.Y, null, detonation: true);
                    return;
                }

                EntitiesHit.Add(target.Id, Timing.Global.Milliseconds + ParentSpell.Combat.TrapDamageCooldown);
                Owner.HandleAoESpell(ParentSpell.Id, target.MapId, target.X, target.Y, null, detonation: true);
            }
        }

        public void Update()
        {
            if (!MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
            {
                return;
            }

            if (ParentSpell == null || 
                ParentSpell.Combat == null ||
                Triggered || 
                Timing.Global.Milliseconds > Duration)
            {
                mapInstance.RemoveTrap(this);
            }
        }

    }

}
