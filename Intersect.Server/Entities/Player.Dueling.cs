﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.GameObjects;
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

                if (exPoolSize >= Options.Instance.DuelOpts.OpenMeleeMinParticipants && instanceController.DuelPool.Count < Options.Instance.DuelOpts.OpenMeleeMinParticipants)
                {
                    PacketSender.SendProximityMsgToLayer($"There are too few players signed up for the open melee to take place. At least {Options.Instance.DuelOpts.OpenMeleeMinParticipants} players must be signed up.", Enums.ChatMessageType.Notice, MapId, MapInstanceId, Color.FromName("Orange", Strings.Colors.presets));
                }
            }
        }

        public void LeaveDuel(bool warp)
        {
            FullHeal();
            if (warp)
            {
                Warp(MeleeEndMapId, MeleeEndX, MeleeEndY);
                InDuel = false;
            }
            LastDuelTimestamp = Timing.Global.MillisecondsUtc; // use this to make matchmaking pool smaller and avoid duplicates
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
    }
}
