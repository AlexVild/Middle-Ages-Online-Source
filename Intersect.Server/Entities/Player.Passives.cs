using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<PassiveSpell> PassiveSpells { get; set; } = new List<PassiveSpell>();

        [NotMapped]
        public List<SpellBase> ActivePassives => PassiveSpells
            .Where(s => s.IsActive)
            .Select(s => SpellBase.Get(s.SpellId))
            .Where(s => s != default)
            .ToList();

        public bool TryGetPassive(Guid spellId, out PassiveSpell passive)
        {
            passive = PassiveSpells.Find(p => p.SpellId == spellId);

            return passive != default;
        }

        public void ActivatePassive(Guid spellId)
        {
            if (!TryGetPassive(spellId, out var passive))
            {
                PassiveSpells.Add(new PassiveSpell(spellId, Id, true));
                return;
            }
            passive.IsActive = true;
            PacketSender.SendEntityStatsToProximity(this);
        }

        public void DeactivatePassive(Guid spellId)
        {
            if (!TryGetPassive(spellId, out var passive))
            {
                return;
            }
            passive.IsActive = false;
            PacketSender.SendEntityStatsToProximity(this);
        }

        public void RemovePassive(Guid spellId)
        {
            if (!TryGetPassive(spellId, out var passive))
            {
                return;
            }

            var passivesToRemove = PassiveSpells.FindAll(p => p.SpellId == spellId);
            PassiveSpells.RemoveAll(p => p.SpellId == spellId);
            foreach(var removedPassive in passivesToRemove)
            {
                DbInterface.Pool.QueueWorkItem(removedPassive.RemoveFromDb);
            }
            PacketSender.SendEntityStatsToProximity(this);
        }

        public Tuple<int, int> GetPassiveStatBuffs(Stats statType)
        {
            var flatStats = 0;
            var percentageStats = 0;

            foreach(var passive in ActivePassives)
            {
                flatStats += passive.Combat.StatDiff[(int)statType];
                percentageStats += passive.Combat.PercentageStatDiff[(int)statType];
            }

            return new Tuple<int, int>(flatStats, percentageStats);
        }

        public int PassiveEffectTotal(EffectType effect)
        {
            var total = 0;

            foreach(var passive in ActivePassives.ToArray())
            {
                total += passive.GetBonusEffectPercentage(effect);
            }

            return total;
        }
    }
}
