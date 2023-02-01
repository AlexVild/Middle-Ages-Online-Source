using Intersect.Enums;
using Intersect.GameObjects.Events;
using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class UpgradeStatPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public UpgradeStatPacket()
        {
        }

        public UpgradeStatPacket(byte stat)
        {
            Stat = stat;
        }

        [Key(0)]
        public byte Stat { get; set; }

    }

    [MessagePackObject]
    public class UpgradeStatsPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public UpgradeStatsPacket()
        {
        }

        public UpgradeStatsPacket(Dictionary<LevelUpAssignments, int> assignments)
        {
            StatAssignments = new int[(int)Stats.StatCount];
            VitalAssignments = new int[(int)Vitals.VitalCount];
            if (assignments == default)
            {
                return;
            }

            foreach (var kv in assignments)
            {
                var stat = kv.Key;
                var allocations = kv.Value;

                switch(stat)
                {
                    case LevelUpAssignments.Health:
                        VitalAssignments[(int)Vitals.Health] = allocations;
                        continue;
                    
                    case LevelUpAssignments.Mana:
                        VitalAssignments[(int)Vitals.Mana] = allocations;
                        continue;
                    
                    case LevelUpAssignments.Evasion:
                        StatAssignments[(int)Stats.Evasion] = allocations;
                        continue;
                    
                    case LevelUpAssignments.Accuracy:
                        StatAssignments[(int)Stats.Accuracy] = allocations;
                        continue;
                    
                    case LevelUpAssignments.Speed:
                        StatAssignments[(int)Stats.Speed] = allocations;
                        continue;
                }
            }
        }

        [Key(0)]
        public int[] StatAssignments { get; set; }

        [Key(1)]
        public int[] VitalAssignments { get; set; }
    }

}
