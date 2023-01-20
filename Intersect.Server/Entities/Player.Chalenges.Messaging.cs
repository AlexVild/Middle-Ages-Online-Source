using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.Network.Packets.Server;
using Intersect.Server.Core;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public void SendChallengeUpdate(bool isComplete, string challenge)
        {
            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.ChallengeComplete.ToString(challenge),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.NewChallenge.ToString(challenge),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
        }

        public void SendChallengeUpdate(bool isComplete, List<string> weaponChallenges)
        {
            if (weaponChallenges == null || weaponChallenges.Count == 0)
            {
                return;
            }

            if (weaponChallenges.Count == 1)
            {
                SendChallengeUpdate(isComplete, weaponChallenges.First());
            }

            var challenges = string.Join(", ", weaponChallenges);

            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.ChallengesComplete.ToString(challenges),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.NewChallenges.ToString(challenges),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
        }

        public void SendMasteryUpdate(bool isComplete, string mastery)
        {
            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponCompletion.ToString(mastery),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponProgression.ToString(mastery),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
        }

        public void SendMasteryUpdate(bool isComplete, List<string> masteries)
        {
            if (masteries == null || masteries.Count == 0)
            {
                return;
            }

            if (masteries.Count == 1)
            {
                SendMasteryUpdate(isComplete, masteries.First());
            }

            var masteryString = string.Join(", ", masteries);

            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponCompletions.ToString(masteryString),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponProgressions.ToString(masteryString),
                    ChatMessageType.Combat,
                    sendToast: true);
            }
        }
    }
}
