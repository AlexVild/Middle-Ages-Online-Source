using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class WeaponTypeProgress
    {
        [Key(0)]
        public Guid WeaponTypeId { get; set; }

        [Key(1)]
        public int Level { get; set; }

        [Key(2)]
        public long Exp { get; set; }

        [Key(3)]
        public List<ChallengeProgression> Challenges { get; set; }

        public WeaponTypeProgress(Guid weaponTypeId, int level, long exp, List<ChallengeProgression> challenges)
        {
            WeaponTypeId = weaponTypeId;
            Level = level;
            Exp = exp;
            Challenges = challenges;
        }
    }

    [MessagePackObject]
    public class ChallengeProgression
    {
        [Key(0)]
        public Guid ChallengeId { get; set; }

        [Key(1)]
        public int Sets { get; set; }

        [Key(2)]
        public bool Completed { get; set; }

        public ChallengeProgression(Guid challengeId, int sets, bool completed)
        {
            ChallengeId = challengeId;
            Sets = sets;
            Completed = completed;
        }
    }

    [MessagePackObject]
    public class ChallengeProgressPacket : IntersectPacket
    {
        [Key(0)]
        public List<WeaponTypeProgress> WeaponTypes { get; set; }

        [Key(1)]
        public Guid TrackedWeaponTypeId { get; set; }

        [Key(2)]
        public Guid CurrentContractId { get; set; }

        // EF
        public ChallengeProgressPacket()
        {
        }

        public ChallengeProgressPacket(List<WeaponTypeProgress> weaponTypes, Guid trackedWeaponTypeId, Guid currentContractId)
        {
            WeaponTypes = weaponTypes;
            TrackedWeaponTypeId = trackedWeaponTypeId;
            CurrentContractId = currentContractId;
        }
    }
}
