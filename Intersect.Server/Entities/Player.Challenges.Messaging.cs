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
        public void SendChallengeUpdate(bool isComplete, Guid challengeId)
        {
            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.ChallengeComplete.ToString(ChallengeDescriptor.GetName(challengeId)),
                    ChatMessageType.Experience,
                    sendToast: true);

                SendChallengeStatUpdates(ChallengeDescriptor.Get(challengeId));
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.NewChallenge.ToString(ChallengeDescriptor.GetName(challengeId)),
                    ChatMessageType.Experience,
                    sendToast: true);
            }
        }

        public void SendChallengeUpdate(bool isComplete, List<Guid> weaponChallenges)
        {
            if (weaponChallenges == null || weaponChallenges.Count == 0)
            {
                return;
            }

            if (weaponChallenges.Count == 1)
            {
                SendChallengeUpdate(isComplete, weaponChallenges.First());
                return;
            }

            var challengeNames = new List<string>();
            foreach ( var challengeId in weaponChallenges)
            {
                challengeNames.Add(ChallengeDescriptor.GetName(challengeId));
            }

            var challengeList = string.Join(", ", challengeNames);

            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.ChallengesComplete.ToString(challengeList),
                    ChatMessageType.Experience,
                    sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.NewChallenges.ToString(challengeList),
                    ChatMessageType.Experience,
                    sendToast: true);
            }
        }

        public void SendMasteryUpdate(bool isComplete, string mastery)
        {
            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponCompletion.ToString(mastery),
                    ChatMessageType.Experience,
                    sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponProgression.ToString(mastery),
                    ChatMessageType.Experience,
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
                return;
            }

            var masteryString = string.Join(", ", masteries);

            if (isComplete)
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponCompletions.ToString(masteryString),
                    ChatMessageType.Experience,
                    sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this,
                    Strings.Player.WeaponProgressions.ToString(masteryString),
                    ChatMessageType.Experience,
                    sendToast: true);
            }
        }

        public void SendChallengeStatUpdates(ChallengeDescriptor challenge)
        {
            if (challenge == null || !challenge.HasStatBoosts())
            {
                return;
            }

            List<string> updates = new List<string>();
            
            if (challenge.StatBoosts != null)
            {
                foreach (var boost in  challenge.StatBoosts)
                {
                    if (boost.Value == 0)
                    {
                        continue;
                    }

                    var symbol = Math.Sign(boost.Value) == 1 ? "+" : "-";
                    updates.Add($"{symbol}{boost.Value} {boost.Key.GetDescription()}");
                }
            }

            if (challenge.VitalBoosts != null)
            {
                foreach (var boost in challenge.VitalBoosts)
                {
                    if (boost.Value == 0)
                    {
                        continue;
                    }

                    var symbol = Math.Sign(boost.Value) == 1 ? "+" : "-";
                    updates.Add($"{symbol}{boost.Value} {boost.Key.GetDescription()}");
                }
            }

            if (challenge.BonusEffects != null)
            {
                foreach (var boost in challenge.BonusEffects)
                {
                    if (boost.Percentage == 0)
                    {
                        continue;
                    }

                    var symbol = Math.Sign(boost.Percentage) == 1 ? "+" : "-";
                    updates.Add($"{symbol}{boost.Percentage}% {boost.Type.GetDescription()}");
                }
            }

            if (updates.Count == 0)
            {
                return;
            }

            var boostStr = string.Join(",", updates);
            
            PacketSender.SendChatMsg(this,
                Strings.Player.ChallengeStatBoosts.ToString(ChallengeDescriptor.GetName(challenge.Id), boostStr),
                ChatMessageType.Experience,
                sendToast: true);
        }

        public void SendNewTrack(string mastery)
        {
            PacketSender.SendChatMsg(this,
                Strings.Player.NewWeaponType.ToString(mastery),
                ChatMessageType.Experience,
                sendToast: true);
        }

        public void SendNewTrack(List<string> masteries)
        {
            if (masteries == null || masteries.Count == 0)
            {
                return;
            }

            if (masteries.Count == 1)
            {
                SendNewTrack(masteries.First());
                return;
            }

            var masteryString = string.Join(", ", masteries);

            PacketSender.SendChatMsg(this,
                Strings.Player.NewWeaponTypes.ToString(masteryString),
                ChatMessageType.Experience,
                sendToast: true);
        }

        void SendWeaponMaxedMessage(WeaponTypeDescriptor weaponType)
        {
            return; // Not doing this for now! Change when removed weapon lvl requirement for challenge completion

            if (WeaponMaxedReminder)
            {
                return;
            }
            PacketSender.SendChatMsg(this,
                Strings.Player.WeaponFinished.ToString(weaponType.Name ?? "NOT FOUND"),
                Enums.ChatMessageType.Experience,
                CustomColors.General.GeneralWarning);

            WeaponMaxedReminder = true;
        }
    }
}
