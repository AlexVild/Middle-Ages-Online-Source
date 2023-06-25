using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Network.Packets.Server;
using Intersect.Server.Core;
using Intersect.Server.Core.Instancing.Controller;
using Intersect.Server.Core.Instancing.Controller.Components;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities.Events;
using Intersect.Server.Entities.PlayerData;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public enum DuelResponse
    {
        None,
        Accept,
        Decline
    }

    public partial class Player : AttackingEntity
    {
        [NotMapped]
        public List<Player> Dueling => CurrentDuel?.Duelers?.Where(d => d.Id != Id)?.ToList() ?? new List<Player>();

        [NotMapped]
        public Duel CurrentDuel { get; set; }

        public bool InDuel { get; set; }

        [NotMapped] // false
        public long LastDuelTimestamp { get; set; } = 0L;

        [NotMapped]
        public bool OpenForDuel { get; set; }

        public Guid MeleeEndMapId { get; set; }

        public int MeleeEndX { get; set; }

        public int MeleeEndY { get; set; }

        [NotMapped]
        public Guid MeleeMapId { get; set; }

        [NotMapped]
        public int MeleeSpawn1X { get; set; }

        [NotMapped]
        public int MeleeSpawn1Y { get; set; }

        [NotMapped]
        public int MeleeSpawn2X { get; set; }

        [NotMapped]
        public int MeleeSpawn2Y { get; set; }

        [NotMapped]
        public long DuelRequestSentAt { get; set; }

        [NotMapped]
        public DuelResponse DuelResponse { get; set; }

        [NotMapped]
        public bool CanDuel => Online && !IsDead();

        public void PromptDuelWith(Player dueling)
        {
            DuelRequestSentAt = Timing.Global.MillisecondsUtc;
            DuelResponse = DuelResponse.None;
        }

        public void EnterInstanceMeleePool()
        {
            if (!InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController))
            {
                return;
            }

            var exPoolSize = instanceController.DuelPool.Count;
            instanceController.JoinMeleePool(this);

            if (exPoolSize < Options.Instance.DuelOpts.OpenMeleeMinParticipants && instanceController.DuelPool.Count >= Options.Instance.DuelOpts.OpenMeleeMinParticipants)
            {
                PacketSender.SendProximityMsgToLayer($"Enough players have signed up for the open melee! Duels will commence shortly.", Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Pink", Strings.Colors.presets));
            }
        }

        public void EnterDuel(Duel duel, int spawnIdx)
        {
            if (duel == null)
            {
                return;
            }

            CurrentDuel = duel;
            InDuel = true;

            // This only supports 1-on-1 duels atm
            if (spawnIdx == 0)
            {
                Warp(MeleeMapId, MeleeSpawn1X, MeleeSpawn1Y);
            }
            else
            {
                Warp(MeleeMapId, MeleeSpawn2X, MeleeSpawn2Y);
            }

            GiveMeleeItems();
            FullHeal();
        }

        public void ForfeitDuel(bool withdrawFromPool)
        {
            if (CurrentDuel == default)
            {
                return;
            }

            CurrentDuel.Leave(this, false);
            CurrentDuel = default;
            if (withdrawFromPool && InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController))
            {
                var exPoolSize = instanceController.DuelPool.Count;

                instanceController.LeaveMeleePool(this);
                PacketSender.SendProximityMsgToLayer($"{Name} forfeited their melee duel.", Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Blue", Strings.Colors.presets));

                if (exPoolSize >= Options.Instance.DuelOpts.OpenMeleeMinParticipants && instanceController.DuelPool.Count < Options.Instance.DuelOpts.OpenMeleeMinParticipants)
                {
                    PacketSender.SendProximityMsgToLayer($"There are too few players signed up for the open melee to take place. At least {Options.Instance.DuelOpts.OpenMeleeMinParticipants} players must be signed up.", Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Orange", Strings.Colors.presets));
                }
            }
        }

        public void LeaveDuel(bool warp)
        {
            TakeMeleeItems();
            FullHeal();
            if (warp)
            {
                Warp(MeleeEndMapId, MeleeEndX, MeleeEndY);
                InDuel = false;
            }
            PacketSender.SendSetDuelOpponent(this, Array.Empty<Player>());
            LastDuelTimestamp = Timing.Global.MillisecondsUtc; // use this to make matchmaking pool smaller and avoid duplicates
        }

        public void TakeMeleeItems()
        {
            var healSlots = FindInventoryItemSlots(Guid.Parse(Options.Instance.DuelOpts.MeleeHealItemId));
            var manaSlots = FindInventoryItemSlots(Guid.Parse(Options.Instance.DuelOpts.MeleeManaItemId));
            foreach (var heal in healSlots)
            {
                TakeItem(heal, int.MaxValue);
            }
            foreach (var mana in manaSlots)
            {
                TakeItem(mana, int.MaxValue);
            }
        }

        public void GiveMeleeItems()
        {
            var healSlots = FindInventoryItemSlots(Guid.Parse(Options.Instance.DuelOpts.MeleeHealItemId));
            var manaSlots = FindInventoryItemSlots(Guid.Parse(Options.Instance.DuelOpts.MeleeManaItemId));

            var healItemId = Guid.Parse(Options.Instance.DuelOpts.MeleeHealItemId);
            var manaItemId = Guid.Parse(Options.Instance.DuelOpts.MeleeManaItemId);

            if (healSlots.Count <= 0)
            {
                var healItemQuantity = ItemBase.Get(healItemId)?.MaxInventoryStack ?? 1;
                if (!TryGiveItem(healItemId, healItemQuantity))
                {
                    PacketSender.SendChatMsg(this, "You did not have enough inventory space to receive your melee healing items!", Enums.ChatMessageType.Notice, CustomColors.General.GeneralDisabled);
                }
            }
            if (manaSlots.Count <= 0)
            {
                var manaItemQuantity = ItemBase.Get(manaItemId)?.MaxInventoryStack ?? 1;
                if (!TryGiveItem(manaItemId, manaItemQuantity))
                {
                    PacketSender.SendChatMsg(this, "You did not have enough inventory space to receive your melee mana items!", Enums.ChatMessageType.Notice, CustomColors.General.GeneralDisabled);
                }
            }
        }

        public void MeleeSignup(Guid mapId, Guid respawnMapId, int spawn1X, int spawn1Y, int spawn2X, int spawn2Y, int respawnX, int respawnY)
        {
            // In-memory
            MeleeMapId = mapId;
            MeleeSpawn1X = spawn1X;
            MeleeSpawn1Y = spawn1Y;
            MeleeSpawn2X = spawn2X;
            MeleeSpawn2Y = spawn2Y;

            // Persistent so we can spawn here on D/C
            MeleeEndMapId = respawnMapId;
            MeleeEndX = respawnX;
            MeleeEndY = respawnY;

            PacketSender.SendToast(this, "You have signed up for the open melee.");
            PacketSender.SendProximityMsgToLayer($"{Name} has signed up for the open melee!", Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Yellow", Strings.Colors.presets));
        }

        public void WithdrawFromMelee()
        {
            if (CurrentDuel != default)
            {
                ForfeitDuel(true);
            }
            else if (InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController))
            {
                var exPoolSize = instanceController.DuelPool.Count;

                instanceController.LeaveMeleePool(this);

                if (exPoolSize >= Options.Instance.DuelOpts.OpenMeleeMinParticipants && instanceController.DuelPool.Count < Options.Instance.DuelOpts.OpenMeleeMinParticipants)
                {
                    PacketSender.SendProximityMsgToLayer($"There are too few players signed up for the open melee to take place. At least {Options.Instance.DuelOpts.OpenMeleeMinParticipants} players must be signed up.", Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Orange", Strings.Colors.presets));
                }
            }

            PacketSender.SendChatMsg(this, "You have been withdrawn from the open melee pool.", Enums.ChatMessageType.Local, sendToast: true);
            PacketSender.SendProximityMsgToLayer($"{Name} has withdrawn from the open melee!", Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Yellow", Strings.Colors.presets));
        }

        public void SendDuelFinishMessage(string loserName, string winnerName)
        {
            var choice = Randomization.Next(0, 20);

            var msg = $"{winnerName} was victorious over {loserName} in their open duel!";
            switch (choice)
            {
                case 0:
                    msg = $"{winnerName} smoked {loserName} like a salmon.";
                    break;
                case 1:
                    msg = $"{winnerName} humiliated {loserName}.";
                    break;
                case 2:
                    msg = $"{winnerName} made a club sandwich out of {loserName}.";
                    break;
                case 3:
                    msg = $"{winnerName} made {loserName} say uncle.";
                    break;
                case 4:
                    msg = $"{winnerName} gave {loserName} the Brooklyn.";
                    break;
                case 5:
                    msg = $"{winnerName} hit {loserName} with the ol' \"Right there, Fred\".";
                    break;
                case 6:
                    msg = $"{winnerName} whacked {loserName}.";
                    break;
                case 7:
                    msg = $"{winnerName} eviscerated {loserName}.";
                    break;
                case 8:
                    msg = $"{winnerName} publically lashed {loserName}.";
                    break;
                case 9:
                    msg = $"{winnerName} gave {loserName} a soft kiss on the cheek.";
                    break;
                case 10:
                    msg = $"{winnerName} made {loserName} into mustard.";
                    break;
                case 11:
                    msg = $"{winnerName} poked a hole in {loserName}.";
                    break;
                case 12:
                    msg = $"{winnerName} spanked {loserName}.";
                    break;
                case 13:
                    msg = $"{winnerName} made a fool out of {loserName}.";
                    break;
                case 14:
                    msg = $"{winnerName} cancelled {loserName}.";
                    break;
                case 15:
                    msg = $"{winnerName} hyzer-flipped {loserName}.";
                    break;
                case 16:
                    msg = $"{winnerName} patted down {loserName} before they got on the plane.";
                    break;
                case 17:
                    msg = $"{winnerName} $&%#'d {loserName}.";
                    break;
                case 18:
                    msg = $"{winnerName} passed the unplugged controller to {loserName}.";
                    break;
                case 19:
                    msg = $"{winnerName} turned off {loserName}'s router.";
                    break;
            }

            PacketSender.SendProximityMsgToLayer(msg, Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Blue", Strings.Colors.presets));
        }

        public void WinMeleeOver(Player defeated)
        {
            // Tell the proximity who won
            SendDuelFinishMessage(defeated?.Name ?? "NOT FOUND", Name);
            IncrementRecord(RecordType.MeleeVictories, Guid.Empty);
            TryGiveItem(Guid.Parse(Options.Instance.DuelOpts.MeleeMedalId), 1);
        }
    }
}
