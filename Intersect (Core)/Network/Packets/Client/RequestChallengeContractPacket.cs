using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestChallengeContractPacket : IntersectPacket
    {
        [Key(0)]
        public Guid ChallengeId { get; set; }

        public RequestChallengeContractPacket(Guid challengeId)
        {
            ChallengeId = challengeId;
        }
    }
}
