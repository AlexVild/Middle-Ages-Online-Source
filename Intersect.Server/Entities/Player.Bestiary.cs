using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public List<BestiaryUnlockInstance> BestiaryUnlocks { get; set; }

        public bool DisableChampionSpawns { get; set; } = false;

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
                // Check for event unlocks
                var playerUnlockAmt = BestiaryUnlocks.ToList().FindAll(unlock => unlock.NpcId == beast.Id && unlock.Unlocked).Count();

                if (beast.BestiaryUnlockAmount <= playerUnlockAmt)
                {
                    completed++;
                    continue;
                }

                // Check for KC unlocks
                if (!killCounts.TryGetValue(beast.Id, out var kc))
                {
                    continue;
                }

                if (beast.BeastCompleted(kc))
                {
                    completed++;
                    continue;
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

            var eventUnlock = BestiaryUnlocks.ToList().Find(playerUnlock => playerUnlock.NpcId == beast.Id && playerUnlock.Unlocked && playerUnlock.UnlockType == (int)unlock);

            if (eventUnlock != default)
            {
                return true;
            }

            // If the beast never had the unlock initialized, but the beast DOES exist, return true
            if (!beast.BestiaryUnlocks.TryGetValue((int)unlock, out var kc))
            {
                return true;
            }

            return GetNpcKillCount(npcId) >= kc;
        }

        public void ChangeBeastUnlockStatus(Guid npcId, BestiaryUnlock unlock, bool status)
        {
            var beasts = BestiaryUnlocks.FindAll(beastUnlock => beastUnlock.NpcId == npcId && beastUnlock.UnlockType == (int)unlock).ToArray();

            // Have we not hard-unlocked anything for this beast? If so, add a new instance
            if (beasts.Length == 0)
            {
                BestiaryUnlocks.Add(new BestiaryUnlockInstance(Id, npcId, unlock, status));
            }
            else // Otherwise, change the status for any found matches
            {
                foreach (var beast in beasts)
                {
                    beast.Unlocked = status;
                }
            }

            // And update the client
            PacketSender.SendBestiaryUnlocks(this, true);
        }

        public Dictionary<Guid, Dictionary<BestiaryUnlock, bool>> BuildUnlockedBeastList()
        {
            var unlocks = new Dictionary<Guid, Dictionary<BestiaryUnlock, bool>>();

            if (BestiaryUnlocks == default || BestiaryUnlocks.Count == 0)
            {
                return unlocks;
            }

            foreach(var instance in BestiaryUnlocks)
            {
                var npcId = instance.NpcId;
                var unlock = instance.UnlockType;
                var isUnlocked = instance.Unlocked;

                if (!unlocks.ContainsKey(npcId)) 
                {
                    unlocks[npcId] = new Dictionary<BestiaryUnlock, bool>();
                }

                unlocks[npcId][(BestiaryUnlock)unlock] = isUnlocked;
            }

            return unlocks;
        }
    }
}
