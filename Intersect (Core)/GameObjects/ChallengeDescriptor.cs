using Intersect.Attributes;
using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.GameObjects
{
    public enum ChallengeType
    {
        [Description("X combo earned Y times")]
        ComboEarned,

        [Description("X damage dealt over Z time, Y times")]
        DamageOverTime,

        [Description("X max hit, Y times")]
        MaxHit,

        [Description("X miss-free streak, Y times")]
        MissFreeStreak,

        [Description("X hits without being hit, Y times")]
        HitFreeStreak,

        [Description("X Damage taken over Z time, Y times")]
        DamageTakenOverTime,

        [Description("X beasts killed over Z time, Y times")]
        BeastsKilledOverTime,

        [Description("X damage done at Z range, Y times")]
        DamageAtRange,

        [Description("X damage healed at Z% health, Y times")]
        DamageHealedAtHealth,

        [Description("X combo exp earned, Y times")]
        ComboExpEarned,

        [Description("X enemies hit with AoE, Y times")]
        AoEHits,

        [Description("X miss-free streak at Z range, Y times")]
        MissFreeAtRange,

        [Description("Event Controlled, Y times")]
        EventControlled,
    }

    public enum ChallengeParamType
    {
        None,

        [RelatedTable(GameObjectType.Npc)]
        BeastsKilled,
    }

    public class ChallengeDescriptor : DatabaseObject<ChallengeDescriptor>, IFolderable
    {
        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public string DisplayName { get; set; }

        public Guid EnhancementUnlockId { get; set; }

        [NotMapped]
        public EnhancementDescriptor UnlockedEnhancement => EnhancementDescriptor.Get(EnhancementUnlockId);

        public Guid SpellUnlockId { get; set; }

        public ChallengeType Type { get; set; }

        [NotMapped]
        public SpellBase SpellUnlock => SpellBase.Get(SpellUnlockId);

        public int Sets { get; set; }

        public int Reps { get; set; }

        public int Param { get; set; }

        public string Icon { get; set; } = string.Empty;

        public int MinTier { get; set; }

        [NotMapped]
        public ConditionLists ContractRequirements { get; set; } = new ConditionLists();

        public string RequirementsString { get; set; } = string.Empty;

        [Column("ContractRequirements")]
        [JsonIgnore]
        public string JsonRequirements
        {
            get => ContractRequirements.Data();
            set => ContractRequirements.Load(value);
        }

        public bool RequiresContract { get; set; }

        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public Dictionary<Stats, int> StatBoosts { get; set; }

        [Column("StatBoosts")]
        public string StatBoostsJson
        {
            get => JsonConvert.SerializeObject(StatBoosts);
            set => StatBoosts = JsonConvert.DeserializeObject<Dictionary<Stats, int>>(value);
        }

        [NotMapped]
        public List<EffectData> BonusEffects { get; set; }

        [NotMapped]
        public List<EffectData> ActiveEffects => BonusEffects?.Where(effect => effect.Percentage != 0)?.ToList() ?? new List<EffectData>();

        [Column("BonusEffects")]
        [JsonIgnore]
        public string BonusEffectsJson
        {
            get => JsonConvert.SerializeObject(BonusEffects);
            set => BonusEffects = JsonConvert.DeserializeObject<List<EffectData>>(value ?? "") ?? new List<EffectData>();
        }

        public int GetBonusEffectPercentage(EffectType type)
        {
            return BonusEffects
                .FindAll(effect => effect.Type == type)
                .Aggregate(0, (int prev, EffectData next) => prev + next.Percentage);
        }

        [NotMapped, JsonIgnore]
        public EffectType[] BonusEffectsEnabled
        {
            get => BonusEffects.Where(effect => effect.Percentage != 0).Select(effect => effect.Type).ToArray();
        }

        public void SetBonusEffectOfType(EffectType type, int value)
        {
            var effects = BonusEffects.FindAll(effect => effect.Type == type);
            if (effects.Count == 0)
            {
                BonusEffects.Add(new EffectData(type, value));
                return;
            }

            BonusEffects.FindAll(effect => effect.Type == type).ForEach(effect => effect.Percentage = value);
        }

        [NotMapped]
        public Dictionary<Vitals, int> VitalBoosts{ get; set; }

        [Column("VitalBoosts")]
        public string VitalBoostsJson
        {
            get => JsonConvert.SerializeObject(VitalBoosts);
            set => VitalBoosts = JsonConvert.DeserializeObject<Dictionary<Vitals, int>>(value);
        }

        public string EventDescription { get; set; }

        public Guid CompletionEventId { get; set; }
        
        public int ChallengeParamType { get; set; }

        public Guid ChallengeParamId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public EventBase CompletionEvent
        {
            get => EventBase.Get(CompletionEventId);
            set => CompletionEventId = value?.Id ?? Guid.Empty;
        }

        public ChallengeDescriptor() : this(default)
        {

        }

        public ChallengeDescriptor(Guid id) : base(id)
        {
            Name = "New Challenge";
        }

        /// <summary>
        /// Used when we want to display the param as seconds
        /// </summary>
        [NotMapped, JsonIgnore]
        public string SecondsParam
        {
            get
            {
                var seconds = (Param / 1000).ToString("N1");
                if (Param % 1000 == 0)
                {
                    seconds = (Param / 1000).ToString("N0");
                }

                return seconds;
            }
        }

        public string GetDescription()
        {
            switch (Type)
            {
                case ChallengeType.ComboEarned:
                    if (Reps > 1)
                    {
                        return $"While killing Tier {MinTier}+ enemies, earn a combo of {Reps}, {Sets} times.";
                    }
                    else
                    {
                        return $"While killing Tier {MinTier}+ enemies, earn a combo of {Reps}.";
                    }

                case ChallengeType.BeastsKilledOverTime:
                    if (Reps > 1)
                    {
                        return $"Kill {Reps} Tier {MinTier}+ enemies over a period of {SecondsParam} seconds or less, {Sets} times.";
                    }
                    else
                    {
                        return $"Kill {Reps} Tier {MinTier}+ enemies over a period of {SecondsParam} seconds or less.";
                    }

                case ChallengeType.DamageAtRange:
                    if (Reps > 1)
                    {
                        return $"Deal {Reps} damage in one hit to an enemy at a range of {Param} tiles, {Sets} times.";
                    }
                    else
                    {
                        return $"Deal {Reps} damage in one hit to an enemy at a range of {Param} tiles.";
                    }

                case ChallengeType.DamageHealedAtHealth:
                    if (Reps > 1)
                    {
                        return $"Heal {Reps} health when your target is at {Param}% HP or lower, {Sets} times.";
                    }
                    else
                    {
                        return $"Heal {Reps} health when your target is at {Param}% HP or lower.";
                    }

                case ChallengeType.DamageOverTime:
                    if (Reps > 1)
                    {
                        return $"Deal {Reps} damage to Tier {MinTier}+ enemies over a period of {SecondsParam} seconds, {Sets} times.";
                    }
                    else
                    {
                        return $"Deal {Reps} damage to Tier {MinTier}+ enemies over a period of {SecondsParam} seconds.";
                    }

                case ChallengeType.DamageTakenOverTime:
                    if (Reps > 1)
                    {
                        return $"Receive {Reps} damage over a period of {SecondsParam} seconds, {Sets} times.";
                    }
                    else
                    {
                        return $"Receive {Reps} damage over a period of {SecondsParam} seconds.";
                    }

                case ChallengeType.HitFreeStreak:
                    if (Reps > 1)
                    {
                        return $"Land {Reps} successful attacks on Tier {MinTier}+ enemies without receiving any damage, {Sets} times.";
                    }
                    else
                    {
                        return $"Land {Reps} successful attacks on Tier {MinTier}+ enemies without receiving any damage.";
                    }

                case ChallengeType.MaxHit:
                    if (Reps > 1)
                    {
                        return $"Deal {Reps} damage on an enemy, {Sets} times.";
                    }
                    else
                    {
                        return $"Deal {Reps} damage on an enemy.";
                    }

                case ChallengeType.MissFreeStreak:
                    if (Reps > 1)
                    {
                        return $"Land {Reps} successful attacks on Tier {MinTier}+ enemies without missing, {Sets} times.";
                    }
                    else
                    {
                        return $"Land {Reps} successful attacks on Tier {MinTier}+ enemies without missing.";
                    }

                case ChallengeType.AoEHits:
                    if (Reps > 1)
                    {
                        return $"Hit {Reps} enemies with an area-of-effect attack, {Sets} times.";
                    }
                    else
                    {
                        return $"Hit {Reps} enemies with an area-of-effect attack.";
                    }
                
                case ChallengeType.ComboExpEarned:
                    if (Reps > 1)
                    {
                        return $"Earn {Reps} EXP through a combo chain, {Sets} times.";
                    }
                    else
                    {
                        return $"Earn {Reps} EXP through a combo chain.";
                    }
                
                case ChallengeType.MissFreeAtRange:
                    if (Reps > 1)
                    {
                        return $"Deal damage to Tier {MinTier}+ enemies {Reps} times without missing, at a range of {Param} tiles or more, {Sets} times.";
                    }
                    else
                    {
                        return $"Deal damage to Tier {MinTier}+ enemies {Reps} times without missing, at a range of {Param} tiles or more.";
                    }
                
                case ChallengeType.EventControlled:
                    if (Reps > 1)
                    {
                        return $"{Description}, {Sets} times.";
                    }
                    return Description;
                
                default:
                    return "No description";
            }
        }
    
        /// <summary>
        /// Informs whether or not the challenge has stat, vital, or bonus boosts
        /// </summary>
        /// <returns>True if the challenge gives stat boosts</returns>
        public bool HasStatBoosts()
        {
            if (StatBoosts != null && StatBoosts.Values.Any(v => v != 0))
            {
                return true;
            }

            if (VitalBoosts != null && VitalBoosts.Values.Any(v => v != 0))
            {
                return true;
            }

            if (BonusEffects != null && BonusEffects.Count > 0 && BonusEffects.Any(data => data.Percentage != 0))
            {
                return true;
            }

            return false;
        }
    
    }
}
