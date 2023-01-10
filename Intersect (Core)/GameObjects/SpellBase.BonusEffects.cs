using Intersect.Enums;
using Intersect.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Intersect.GameObjects
{
    public partial class SpellBase : DatabaseObject<SpellBase>, IFolderable
    {
        [NotMapped]
        public List<EffectData> BonusEffects { get; set; }

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
            get => BonusEffects.Select(effect => effect.Type).ToArray();
        }

        public void SetBonusEffectOfType(EffectType type, int value)
        {
            BonusEffects.FindAll(effect => effect.Type == type).Select(effect => effect.Percentage = value);
        }
    }
}
