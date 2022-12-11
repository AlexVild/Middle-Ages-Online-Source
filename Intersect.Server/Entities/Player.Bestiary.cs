using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public Dictionary<Guid, long> GetNpcKillCounts()
        {
            var npcRecords = PlayerRecords.Where(record => record.Type == RecordType.NpcKilled).ToArray();
            var killCounts = new Dictionary<Guid, long>();

            foreach(var record in npcRecords)
            {
                killCounts[record.RecordId] = record.Amount;
            }

            return killCounts;
        }

        public long GetNpcKillCount(Guid npcId)
        {
            var npcRecord = PlayerRecords.Find(record => record.Type == RecordType.NpcKilled && record.RecordId == npcId);
            return npcRecord?.Amount ?? 0;
        }

        /// <summary>
        /// Gets the total amount of beasts that have been completed in the bestiary
        /// </summary>
        /// <returns>The number of beasts completed</returns>
        public int GetBeastsCompleted()
        {
            int completed = 0;

            var validBeasts = NpcBase.Lookup
                .Select(npc => ((NpcBase)npc.Value))
                .Where(npc => !npc.NotInBestiary)
                .ToArray();

            var killCounts = GetNpcKillCounts();

            foreach (var beast in validBeasts)
            {
                // The player has never killed this valid beast
                if (!killCounts.TryGetValue(beast.Id, out var kc))
                {
                    continue;
                }

                var isComplete = beast.BestiaryUnlocks.Values.All(reqKc => kc >= reqKc);
                if (isComplete)
                {
                    completed++;
                }
            }

            return completed;
        }

        public bool HasUnlockFor(Guid npcId, BestiaryUnlock unlock)
        {
            var beast = NpcBase.Get(npcId);
            if (beast == null)
            {
                Logging.Log.Error("Null beast given to check unlock for");
                return false;
            }

            // If the beast never had the unlock initialized, but the beast DOES exist, return true
            if (!beast.BestiaryUnlocks.TryGetValue((int)unlock, out var kc))
            {
                return true;
            }

            return GetNpcKillCount(npcId) >= kc;
        }
    }
}
