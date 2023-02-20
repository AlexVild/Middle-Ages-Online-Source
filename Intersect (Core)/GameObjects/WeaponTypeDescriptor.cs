using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.GameObjects
{
    public class WeaponTypeDescriptor : DatabaseObject<WeaponTypeDescriptor>, IFolderable
    {
        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public string VisibleName => string.IsNullOrEmpty(DisplayName) ? GetName(Id) : DisplayName;

        public string DisplayName { get; set; }

        public int MaxLevel { get; set; }

        [NotMapped]
        public Dictionary<int, WeaponLevel> Unlocks { get; set; }

        [Column("ExpRequirements")]
        public string ExpRequirementsJson {
            get => JsonConvert.SerializeObject(Unlocks);
            set => Unlocks = JsonConvert.DeserializeObject<Dictionary<int, WeaponLevel>>(value ?? string.Empty) ?? new Dictionary<int, WeaponLevel>();
        }


        public WeaponTypeDescriptor() : this(default)
        {
        }

        public WeaponTypeDescriptor(Guid id) : base(id)
        {
            Name = "New Weapon Type";
            Unlocks = new Dictionary<int, WeaponLevel>();
        }
    }

    public class WeaponLevel
    {
        public List<Guid> ChallengeIds { get; set; }

        public int RequiredExp { get; set; }

        public int EnhancementCostPerPoint { get; set; }

        public WeaponLevel(int requiredExp, int enhancementCost)
        {
            RequiredExp = requiredExp;
            ChallengeIds = new List<Guid>();
            EnhancementCostPerPoint = enhancementCost;
        }

        public override string ToString()
        {
            var challenges = new List<string>();
            foreach(var challengeId in ChallengeIds)
            {
                var challenge = ChallengeDescriptor.Get(challengeId);
                if (challenge == default)
                {
                    continue;
                }

                challenges.Add(challenge.Name);
            }

            return $"EXP: {RequiredExp}, Challenges: [{string.Join(", ", challenges.ToArray())}], EP Cost: {EnhancementCostPerPoint}";
        }
    }
}
