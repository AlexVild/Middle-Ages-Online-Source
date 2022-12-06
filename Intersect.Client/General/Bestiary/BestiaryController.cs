using Intersect.Client.Interface.Game.Toasts;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.General.Bestiary
{
    sealed public class Bestiary
    {
        /// <summary>
        /// A reference of some NPC Guid => A dictionary containing bestiary unlocks and their unlock status.
        /// </summary>
        public Dictionary<Guid, Dictionary<BestiaryUnlock, bool>> Unlocks { get; set; } = new Dictionary<Guid, Dictionary<BestiaryUnlock, bool>>();

        public string UnlockToastMessage(Guid npcGuid, BestiaryUnlock unlockType)
        {
            var beast = NpcBase.GetName(npcGuid);
            var unlock = unlockType.GetDescription();

            return $"Bestiary update for \"{beast}\": {unlock}";
        }

        public string UnlockToastMessageGroup(Guid npcGuid, BestiaryUnlock[] unlockTypes)
        {
            var beast = NpcBase.GetName(npcGuid);
            var descriptions = unlockTypes.Select(type => type.GetDescription());

            var sb = new StringBuilder();

            var unlocks = string.Join(", ", descriptions);

            return $"Bestiary update for \"{beast}\": {unlocks}";
        }

        public void UpdateUnlocksFor(Guid npcGuid, long playersKillCount, bool suppressMessaging = true)
        {
            // Get bestiary information from the slain NPC
            var beast = NpcBase.Get(npcGuid);
            if (beast == default)
            {
                return;
            }

            var beastUnlocks = beast.BestiaryUnlocks;
            if (!Unlocks.ContainsKey(npcGuid))
            {
                Unlocks[npcGuid] = new Dictionary<BestiaryUnlock, bool>();
            }

            // For messaging
            List<BestiaryUnlock> newUnlocks = new List<BestiaryUnlock>();

            // For every flavor of bestiary unlock
            foreach(BestiaryUnlock bestiaryUnlock in Enum.GetValues(typeof(BestiaryUnlock)))
            {
                var requiredKillCount = 0;
                
                // Does the NPC have this property unlocked by default/not exist/is not loggable?
                if (beastUnlocks == null || beast.NotInBestiary || !beastUnlocks.TryGetValue((int)bestiaryUnlock, out requiredKillCount))
                {
                    Unlocks[npcGuid][bestiaryUnlock] = true;
                    continue;
                }

                // Otherwise have we crossed the kill threshold?
                Unlocks[npcGuid].TryGetValue(bestiaryUnlock, out var unlockStatus);
                if (playersKillCount >= requiredKillCount)
                {
                    Unlocks[npcGuid][bestiaryUnlock] = true;
                    // If we did not previous have this unlocked, push it to our new list of unlocks for messaging
                    if (unlockStatus != true)
                    {
                        newUnlocks.Add(bestiaryUnlock);
                    }
                }
                else
                {
                    Unlocks[npcGuid][bestiaryUnlock] = false;
                }
            }

            if (!suppressMessaging && newUnlocks.Count > 0)
            {
                ToastService.SetToast(new Toast(UnlockToastMessageGroup(npcGuid, newUnlocks.ToArray())));
            }
        }

        public bool HasUnlock(Guid npcGuid, BestiaryUnlock unlockType)
        {
            if (npcGuid == default)
            {
                return true;
            }
            if (!Unlocks.TryGetValue(npcGuid, out var unlocks))
            {
                return false;
            }
            if (!unlocks.TryGetValue(unlockType, out var unlockStatus))
            {
                return false;
            }
            return unlockStatus;
        }
    }

    public static class BestiaryController
    {
        public static Bestiary MyBestiary = new Bestiary();

        public static Dictionary<Guid, long> KnownKillCounts = new Dictionary<Guid, long>();

        public static void RefreshUnlocks(bool suppressMessaging)
        {
            foreach(var killCount in KnownKillCounts)
            {
                MyBestiary.UpdateUnlocksFor(killCount.Key, killCount.Value, suppressMessaging);
            }
        }
    }
}
