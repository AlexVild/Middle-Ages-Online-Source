using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ExperiencePacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public ExperiencePacket()
        {
        }

        public ExperiencePacket(long exp, long tnl)
        {
            Experience = exp;
            ExperienceToNextLevel = tnl;
            AccumulatedComboExp = 0;
        }

        public ExperiencePacket(long exp, long tnl, int accCombExp)
        {
            Experience = exp;
            ExperienceToNextLevel = tnl;
            AccumulatedComboExp = accCombExp;
        }

        [Key(0)]
        public long Experience { get; set; }

        [Key(1)]
        public long ExperienceToNextLevel { get; set; }
        
        [Key(2)]
        public int AccumulatedComboExp { get; set; }

    }

}
