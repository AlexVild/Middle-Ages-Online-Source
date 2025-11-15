using System;
using System.Collections.Generic;
using System.Linq;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;

namespace Intersect.Server.Entities.Combat
{

    public partial class Status
    {

        public string Data = "";

        public long Duration;

        private Entity mEntity;

        public Entity Attacker;

        public SpellBase Spell;

        public long StartTime;

        public StatusTypes Type;

        public static List<StatusTypes> TenacityExcluded = new List<StatusTypes>()
        {
            StatusTypes.None,
            StatusTypes.Stealth,
            StatusTypes.Cleanse,
            StatusTypes.Invulnerable,
            StatusTypes.OnHit,
            StatusTypes.Shield,
            StatusTypes.Transform,
            StatusTypes.Swift,
            StatusTypes.Accurate,
            StatusTypes.Haste,
        };

        public static List<StatusTypes> InterruptStatusses = new List<StatusTypes>()
        {
            StatusTypes.Silence,
            StatusTypes.Sleep,
            StatusTypes.Stun,
            StatusTypes.Confused,
        };

        public Status(Entity en, Entity attacker, SpellBase spell, StatusTypes type, int duration, string data)
        {
            mEntity = en;
            Attacker = attacker;
            Spell = spell;
            Type = type;
            Data = data;

            if (!spell.Combat.Friendly && en.CachedStatuses.Any(AttackingEntity.PredicateCleansed) && spell.Combat.Effect != StatusTypes.Cleanse)
            {
                PacketSender.SendActionMsg(en, Strings.Combat.immunetoeffect, CustomColors.Combat.Status);
                return;
            }

            // Handle Player specific stuff such as retrieving their tenacity.
            var tenacity = 0.0;
            if (en is Player player)
            {
                // Get our player's Tenacity stat!
                if (!TenacityExcluded.Contains(type))
                {
                    tenacity = player.GetBonusEffectTotal(EffectType.Tenacity) + player.GetStaleTenacityMod(type);
                } 
            }

            // Handle Npc specific stuff, such as loot tables!
            if (en is Npc npc)
            {
                // Add to loot map if not already eligible.
                if (!npc.LootMap.ContainsKey(Attacker.Id))
                {
                    npc.LootMap.TryAdd(Attacker.Id, true);
                    npc.LootMapCache = npc.LootMap.Keys.ToArray();
                }

                if (!TenacityExcluded.Contains(type))
                {
                    tenacity = npc.Base.Tenacity + npc.GetStaleTenacityMod(type);
                }
            }

            tenacity = MathHelper.Clamp(tenacity, -100.0, 100.0);

            // Interrupt their spellcast if we are running a Silence, Sleep or Stun!
            if (InterruptStatusses.Contains(type) && en is AttackingEntity victim)
            {
                victim.CancelCast();
            }

            // If we're adding a shield, actually add that according to the settings.
            if (type == StatusTypes.Shield)
            {
                for (var i = (int)Vitals.Health; i < (int)Vitals.VitalCount; i++)
                {
                    var vitalDiff = spell.Combat.VitalDiff[i];

                    shield[i] = Math.Abs(vitalDiff);
                }
                if (en is Player p)
                {
                    for (var i = 0; i < p.Party.Count; i++)
                    {
                        PacketSender.SendPartyUpdateTo(p.Party[i], p, shield[(int) Vitals.Health]);
                    }
                }
            }

            // If new Cleanse spell, remove all opposite statusses. (ie friendly dispels unfriendly and vice versa)
            if (Type == StatusTypes.Cleanse)
            {
                foreach (var status in en.CachedStatuses)
                {
                    if (spell.Combat.Friendly != status.Spell.Combat.Friendly)
                    {
                        status.RemoveStatus();
                    }
                }

                foreach (var dot in en.CachedDots)
                {
                    if (spell.Combat.Friendly != dot.SpellBase.Combat.Friendly)
                    {
                        dot.Expire();
                    }
                }
            }

            // Remove existing taunts if this is one and there are any others.
            // We'll be overwriting it, baby!
            if (Type == StatusTypes.Taunt)
            {
                foreach(var status in en.CachedStatuses)
                {
                    if (status.Type == StatusTypes.Taunt)
                    {
                        status.RemoveStatus();
                    }
                }
            }

            // Calculate our final duration and pass it on!
            tenacity = MathHelper.Clamp(tenacity, -100, 100);
            var finalDuration = duration - (duration * (tenacity / 100f));
            
            // If this spell was already applied, only update the status if the new duration is going to be longer than the existing.
            if (en.Statuses.ContainsKey(spell) && Timing.Global.Milliseconds + (long)finalDuration > en.Statuses[spell].Duration)
            {
                en.Statuses[spell].StartTime = Timing.Global.Milliseconds;
                en.Statuses[spell].Duration = Timing.Global.Milliseconds + (long)finalDuration;
                en.Statuses[spell].StartTime = StartTime;
                en.CachedStatuses = en.Statuses.Values.ToArray();

                if (en is AttackingEntity attEn)
                {
                    attEn.CCApplied(type);
                }
            }
            else
            { 
                StartTime = Timing.Global.Milliseconds;
                Duration = Timing.Global.Milliseconds + (long) finalDuration;
                en.Statuses.TryAdd(Spell, this);
                en.CachedStatuses = en.Statuses.Values.ToArray();

                if (en is AttackingEntity attEn)
                {
                    attEn.CCApplied(type);
                }
            }

            // If this is a taunt, force the target properly for players and NPCs
            if (Type == StatusTypes.Taunt)
            {
                
                // If player, force send target!
                if (en is Player targetPlayer)
                {
                    en.Target = Attacker;
                    PacketSender.SetPlayerTarget(targetPlayer, Attacker.Id);
                }
                // If NPC, force assign target and make sure we have the highest threat +1 so we in theory have the highest threat.. for now..
                else if (en is Npc targetNpc)
                {
                    targetNpc.AssignTarget(Attacker);

                    if (!targetNpc.DamageMap.ContainsKey(Attacker))
                    {
                        if (targetNpc.DamageMap.Count > 0)
                        {
                            targetNpc.DamageMap.TryAdd(Attacker, targetNpc.DamageMap.ToArray().Max(x => x.Value) + 1);
                        }
                        else
                        {
                            targetNpc.DamageMap.TryAdd(Attacker, 1);
                        }
                    }
                    else
                    {
                        targetNpc.DamageMap[Attacker] = targetNpc.DamageMap.ToArray().Max(x => x.Value) + 1;
                    }
                }
                else
                {
                    en.Target = Attacker;
                }
                
            }
        }

        public int[] shield { get; set; } = new int[(int) Enums.Vitals.VitalCount];

        public void TryRemoveStatus()
        {
            if (Duration <= Timing.Global.Milliseconds) //Check the timer
            {
                RemoveStatus();
            }

            //If shield check for out of hp
            if (Type == StatusTypes.Shield)
            {
                for (var i = (int) Vitals.Health; i < (int) Vitals.VitalCount; i++)
                {
                    if (shield[i] > 0)
                    {
                        return;
                    }
                }
                RemoveStatus();
            }
        }

        public void RemoveStatus()
        {
            mEntity.Statuses.TryRemove(Spell, out Status val);
            mEntity.CachedStatuses = mEntity.Statuses.Values.ToArray();

            // if this was a taunt status being removed, we have to scan for a new target!
            if (mEntity is Npc npc && Type == StatusTypes.Taunt)
            {
                npc.TryFindNewTarget(0, Guid.Empty, true);
            }
            if (Type == StatusTypes.Shield)
            {
                if (mEntity is Player p)
                {
                    for (var i = 0; i < p.Party.Count; i++)
                    {
                        PacketSender.SendPartyUpdateTo(p.Party[i], p, 0);
                    }
                }
            }
        }

        public void DamageShield(Vitals vital, ref int amount)
        {
            if (Type == StatusTypes.Shield)
            {
                shield[(int) vital] -= amount;
                if (shield[(int) vital] <= 0)
                {
                    amount = -shield[(int) vital]; //Return piercing damage.
                    shield[(int) vital] = 0;
                    TryRemoveStatus();
                }
                else
                {
                    amount = 0; //Sheild is stronger than the damage dealt, so no piercing damage.
                }
            }
        }

    }

}
