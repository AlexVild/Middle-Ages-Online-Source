using MessagePack;
using System;

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

        public ExperiencePacket(long exp, long tnl, int accCombExp, long weaponExp, long weaponExpTnl, int weaponLevel, Guid trackedWeaponId)
        {
            Experience = exp;
            ExperienceToNextLevel = tnl;
            AccumulatedComboExp = accCombExp;
            WeaponExp = weaponExp;
            WeaponExpTnl = weaponExpTnl;
            WeaponLevel = weaponLevel;
            TrackedWeaponId = trackedWeaponId;
        }

        [Key(0)]
        public long Experience { get; set; }

        [Key(1)]
        public long ExperienceToNextLevel { get; set; }
        
        [Key(2)]
        public int AccumulatedComboExp { get; set; }

        [Key(3)]
        public long WeaponExp { get; set; }

        [Key(4)]
        public long WeaponExpTnl { get; set; }

        [Key(5)]
        public int WeaponLevel { get; set; }

        [Key(6)]
        public Guid TrackedWeaponId { get; set; }
    }

}
