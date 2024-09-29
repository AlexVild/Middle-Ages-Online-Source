using Intersect.Enums;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        public List<Guid> PlayerIds => Players.Select(pl => pl.Id).ToList();

        public InstanceController(Guid instanceId, Player creator)
        {
            InstanceId = instanceId;
            AddPlayer(creator);

            InitializeInstanceVariables();
        }

        public void AddPlayer(Player player)
        {
            if (player == null || !player.Online)
            {
                return;
            }

            if (PlayerIds.Contains(player.Id))
            {
                return;
            }

            Logging.Log.Debug($"{player.Name} has entered instance controller {InstanceId}!");
            Players.Add(player);
        }

        public void RemovePlayer(Guid playerId)
        {
            Logging.Log.Debug($"{playerId} has left instance controller {InstanceId}...");
            Players.RemoveAll(pl => pl?.Id == playerId);

            if (Dungeon != null)
            {
                RemovePlayerFromDungeon(playerId);
            }
        }

        public void AddPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                if (PlayerIds.Contains(player.Id))
                {
                    continue;
                }
                
                Players.Add(player);
            }
        }

        Guid InstanceId { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        public int PlayerCount => Players.Count;

        public int InstanceLives { get; set; }

        public bool OutOfLives => InstanceLives < 0 && Options.Instance.Instancing.MaxSharedInstanceLives > 0;

        public bool InitializedLives { get; set; }

        public void TryInitializeLives(int instanceLives, Player requester)
        {
            if (InitializedLives)
            {
                // Update the joining player with the amount of lives in the instance
                PacketSender.SendInstanceLivesPacket(requester, (byte)InstanceLives, false);

                // Did the player try to join a life-less instance?
                if (OutOfLives)
                {
                    requester.Die();
                }

                return;
            }

            SendInstanceLifeUpdate(instanceLives, true);
            InitializedLives = true;
        }

        public void SendInstanceLifeUpdate(int instanceLives, bool noChat = false)
        {
            if (Options.Instance.Instancing.MaxSharedInstanceLives <= 0)
            {
                // Lives aren't enabled for this server
                return;
            }

            InstanceLives = instanceLives;

            foreach (var player in Players.ToList())
            {
                if (player == null || !player.Online)
                {
                    continue;
                }

                PacketSender.SendInstanceLivesPacket(player, (byte)InstanceLives, false);

                if (!noChat)
                {
                    if (InstanceLives > 0)
                    {
                        PacketSender.SendChatMsg(player, Strings.Parties.instancelivesremaining.ToString(InstanceLives + 1), ChatMessageType.Party, CustomColors.Chat.PartyChat);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player, Strings.Parties.nomorelivesremaining.ToString(InstanceLives + 1), ChatMessageType.Party, CustomColors.Chat.PartyChat);
                    }
                }

                if (OutOfLives && !player.IsDead())
                {
                    player.Die();
                }
            }
        }

        public void LoseInstanceLife()
        {
            if (OutOfLives)
            {
                return;
            }

            SendInstanceLifeUpdate(InstanceLives - 1);
        }
    }
}
