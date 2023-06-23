using System;
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

        [NotMapped] // false
        public bool InDuel { get; set; }

        [NotMapped] // false
        public long LastDuelTimestamp { get; set; } = 0L;

        [NotMapped]
        public bool OpenForDuel { get; set; }

        [NotMapped] // false
        public Guid DuelEndMapId { get; set; }

        [NotMapped] // false
        public int DuelEndX { get; set; }

        [NotMapped] // false
        public int DuelEndY { get; set; }

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

        public void EnterInstanceDuelPool()
        {
            if (!InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController))
            {
                return;
            }

            instanceController.JoinDuelPool(this);
        }

        public void EnterDuel(Duel duel, int spawnIdx)
        {
            if (duel == null)
            {
                return;
            }

            CurrentDuel = duel;
            DuelEndMapId = Guid.Parse(CurrentDuel.DuelEndMapId);
            DuelEndX = CurrentDuel.DuelEndX;
            DuelEndY = CurrentDuel.DuelEndY;
            InDuel = true;

            // This only supports 1-on-1 duels atm
            if (spawnIdx == 0)
            {
                Warp(Guid.Parse(CurrentDuel.DuelMapId), CurrentDuel.DuelMapX1, CurrentDuel.DuelMapY1);
            }
            else
            {
                Warp(Guid.Parse(CurrentDuel.DuelMapId), CurrentDuel.DuelMapX2, CurrentDuel.DuelMapY2);
            }
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
                instanceController.LeaveDuelPool(this);
            }
        }

        public void LeaveDuel(bool warp)
        {
            FullHeal();
            if (warp)
            {
                Warp(DuelEndMapId, DuelEndX, DuelEndY);
                InDuel = false;
            }
            LastDuelTimestamp = Timing.Global.MillisecondsUtc; // use this to make matchmaking pool smaller and avoid duplicates
        }
    }
}
