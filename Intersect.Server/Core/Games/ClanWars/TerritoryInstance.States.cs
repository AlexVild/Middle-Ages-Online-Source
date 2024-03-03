using Intersect.GameObjects.Events;
using Intersect.Server.Database;
using Intersect.Utilities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Core.Games.ClanWars
{
    public partial class TerritoryInstance
    {
        [Column("State")]
        public int _state { get; set; }

        [NotMapped, JsonIgnore]
        public TerritoryState State
        {
            get
            {
                if (Enum.IsDefined(typeof(TerritoryState), _state))
                {
                    return (TerritoryState)_state;
                }
                return TerritoryState.Neutral;
            }

            set => _state = (int)value;
        }

        [NotMapped, JsonIgnore]
        private TerritoryState _prevState { get; set; }

        [NotMapped, JsonIgnore] 
        private Guid PrevConquerer { get; set; }

        private void StateNeutral(long currentTime)
        {
            Health = 0;
            // No one around/fight ongoing? Don't do anything
            if (PlayerGuildIds.Length != 1)
            {
                return;
            }

            FindCurrentConquerer();
            if (ConqueringGuildId != Guid.Empty)
            {
                mNextHealthTick = Timing.Global.Milliseconds + Options.Instance.ClanWar.HealthTickMs;
                PrevConquerer = ConqueringGuildId;
                ChangeState(TerritoryState.Capturing, currentTime);
            }
        }

        private void StateOwned(long currentTime)
        {
            // Defenders exist, or the territory is empty? We done
            if (Defenders.Length > 0 || Players.Count == 0)
            {
                return;
            }

            FindCurrentConquerer();
            if (ConqueringGuildId != Guid.Empty)
            {
                mNextHealthTick = Timing.Global.Milliseconds + Options.Instance.ClanWar.HealthTickMs;
                PrevConquerer = ConqueringGuildId;
                ChangeState(TerritoryState.Wresting, currentTime);
            }
        }

        private void StateCapturing(long currentTime)
        {
            // Capture abandoned
            if (Invaders.Length == 0)
            {
                ResetHealth();
                ChangeState(TerritoryState.Neutral, currentTime);
                return;
            }

            // Otherwise, is there competition?
            if (PlayerGuildIds.Length > 1)
            {
                // Territory must be under contest!
                ChangeState(TerritoryState.Contested, currentTime);
                return;
            }

            // Did a new team take over the capture?
            FindCurrentConquerer();
            if (PrevConquerer != ConqueringGuildId)
            {
                PrevConquerer = ConqueringGuildId;
                ResetHealth();
                return;
            }

            // Otherwise, proceed with capture
            TerritoryHelper.TickHealth(ref mNextHealthTick, ref Health, Invaders.Length, Timing.Global.Milliseconds, false);

            if (Health < Territory.CaptureMs)
            {
                return;
            }
            GuildTakeOver(ConqueringGuildId, currentTime);
        }

        private void StateWresting(long currentTime)
        {
            // Wresting failed or everyone left
            if (Invaders.Length == 0 || Players.Count == 0)
            {
                ResetHealth();
                ChangeState(TerritoryState.Owned, currentTime);
                return;
            }

            // Otherwise, is there competition?
            if (PlayerGuildIds.Length > 1)
            {
                // Territory must be under contest!
                ChangeState(TerritoryState.Contested, currentTime);
                return;
            }

            // Did a new team take over the capture?
            FindCurrentConquerer();
            if (PrevConquerer != ConqueringGuildId)
            {
                PrevConquerer = ConqueringGuildId;
                ResetHealth();
                return;
            }

            // Otherwise, proceed with wresting control
            TerritoryHelper.TickHealth(ref mNextHealthTick, ref Health, Invaders.Length, Timing.Global.Milliseconds, true);
            if (Health > 0)
            {
                return;
            }
            TeritoryLost(currentTime);
        }

        private void StateContested(long currentTime)
        {
            // Did the defenders succeed in contest?
            if (Invaders.Length == 0 && GuildId != Guid.Empty)
            {
                ResetHealth();
                ChangeState(TerritoryState.Owned, currentTime);
                return;
            }

            // Otherwise, did everyone leave an otherwise neutral territory before ownership was swapped?
            if (Players.Count == 0 && GuildId == Guid.Empty)
            {
                ChangeState(TerritoryState.Neutral, currentTime);
                return;
            }

            // There's not one single conquering guild - remain contested
            FindCurrentConquerer();
            if (ConqueringGuildId == Guid.Empty)
            {
                return;
            }


            // If an invading guild was victorious, go back to wrest/capture
            mNextHealthTick = Timing.Global.Milliseconds + Options.Instance.ClanWar.HealthTickMs;
            ChangeState(_prevState, currentTime);
            
            // If the conquerer changed since contest was entered
            if (ConqueringGuildId != PrevConquerer)
            {
                ResetHealth();
            }
        }

        private void ChangeState(TerritoryState state, long currentTime)
        {
            _prevState = State;
            State = state;

#if DEBUG
            Logging.Log.Debug($"Territory {Territory.Name} state change: {_prevState} -> {State}");
#endif
            if (StateChanged)
            {
                Debounce();
            }
            else
            {
                StateChanged = true;
            }
            CachePlayerLookups();
        }

        private void BroadcastStateChange()
        {
            ClanWarManager.BroadcastTerritoryUpdate(this);
            Save();
            Debounce();
        }

    }
}
