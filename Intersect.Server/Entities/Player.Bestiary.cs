using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public Dictionary<Guid, long> GetNpcKillCounts()
        {
            var npcRecords = PlayerRecords.Where(record => record.Type == GameObjects.Events.RecordType.NpcKilled).ToArray();
            var killCounts = new Dictionary<Guid, long>();

            foreach(var record in npcRecords)
            {
                killCounts[record.RecordId] = record.Amount;
            }

            return killCounts;
        }

        public long GetNpcKillCount(Guid npcId)
        {
            var npcRecord = PlayerRecords.Find(record => record.Type == GameObjects.Events.RecordType.NpcKilled && record.RecordId == npcId);
            return npcRecord?.Amount ?? 0;
        }
    }
}
