using Intersect.Network.Packets.Server;
using Intersect.Server.Entities;
using System.Collections.Generic;

namespace Intersect.Server.Maps
{
    public class MapActionMessages
    {
        public List<ActionMsgPacket> mActionMessages = new List<ActionMsgPacket>();
        public List<CombatNumberPacket> mCombatNumbers = new List<CombatNumberPacket>();

        public void Add(ActionMsgPacket pkt)
        {
            lock (mActionMessages)
            {
                mActionMessages.Add(pkt);
            }
        }

        public void Add(CombatNumberPacket pkt)
        {
            lock (mCombatNumbers)
            {
                mCombatNumbers.Add(pkt);
            }
        }

        public void SendPackets(HashSet<Player> nearbyPlayers)
        {
            if (mActionMessages.Count > 0)
            {
                lock (mActionMessages)
                {
                    var pkt = new ActionMsgPackets()
                    {
                        Packets = mActionMessages.ToArray()
                    };
                    foreach (var plyr in nearbyPlayers)
                    {
                        plyr.SendPacket(pkt, Network.TransmissionMode.Any);
                    }
                    mActionMessages.Clear();
                }
            }
            if (mCombatNumbers.Count > 0)
            {
                lock (mCombatNumbers)
                {
                    var pkt = new CombatNumberPackets()
                    {
                        Packets = mCombatNumbers.ToArray()
                    };
                    foreach (var plyr in nearbyPlayers)
                    {
                        plyr.SendPacket(pkt, Network.TransmissionMode.Any);
                    }
                    mCombatNumbers.Clear();
                }
            }
        }
    }
}
