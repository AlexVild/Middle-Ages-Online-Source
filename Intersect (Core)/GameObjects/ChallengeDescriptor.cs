using Intersect.Attributes;
using Intersect.Enums;
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

        public Guid SpellUnlockId { get; set; }

        public ChallengeType Type { get; set; }

        [NotMapped]
        public SpellBase SpellUnlock => SpellBase.Get(SpellUnlockId);

        public int Sets { get; set; }

        public int Reps { get; set; }

        public int Param { get; set; }

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
    }
}
