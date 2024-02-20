using Intersect.GameObjects.Events;
using Intersect.Server.Database;
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

        private void StateNeutral(long currentTime)
        {
            // No one around/fight ongoing? Don't do anything
            if (PlayerGuildIds.Length != 1)
            {
                return;
            }

            if (TryConquererSwitch() || mConqueringGuildId != Guid.Empty)
            {
                mNextHealthTick = currentTime + HEALTH_TICK_TIME;
                ChangeState(TerritoryState.Capturing, currentTime);
            }
        }

        private void StateOwned(long currentTime)
        {
            // No one around/fight ongoing OR only defenders? Don't do anything
            if (Invaders.Length == 0)
            {
                return;
            }

            if (TryConquererSwitch())
            {
                mNextHealthTick = currentTime + HEALTH_TICK_TIME;
                ChangeState(TerritoryState.Wresting, currentTime);
            }
        }

        private void StateCapturing(long currentTime)
        {
            TickHealth(currentTime);

            // Did someone else start taking over mid-capture? I.e your clan lost a fight and another clan begins taking over
            if (TryConquererSwitch())
            {
                ResetHealth();
                return;
            }
            
            // No one around? Reset to neutral
            if (PlayerGuildIds.Length == 0 || Defenders.Length == PlayerGuildIds.Length)
            {
                ResetHealth();
                ChangeState(TerritoryState.Neutral, currentTime);
                return;
            }

            if (PlayerGuildIds.Length > 1)
            {
                // Territory must be under contest!
                ChangeState(TerritoryState.Contested, currentTime);
            }

            if (Health >= Territory.CaptureMs)
            {
                GuildTakeOver(mConqueringGuildId, currentTime);
                return;
            }
        }

        private void StateWresting(long currentTime)
        {
            TickHealth(currentTime, true);

            // Did someone else start taking over mid-capture? I.e your clan lost a fight and another clan begins taking over
            if (TryConquererSwitch())
            {
                ResetHealth();
                return;
            }

            if (PlayerGuildIds.Length == 0 || Defenders.Length == PlayerGuildIds.Length)
            {
                ResetHealth();
                ChangeState(TerritoryState.Owned, currentTime);
                return;
            }

            if (PlayerGuildIds.Length > 1)
            {
                // Territory must be under contest!
                ChangeState(TerritoryState.Contested, currentTime);
            }

            if (Health <= 0)
            {
                TeritoryLost(currentTime);
                return;
            }
        }

        private void StateContested(long currentTime)
        {
            if (TryConquererSwitch())
            {
                // Go back to either capturing or wresting state, with new conquerer
                mNextHealthTick = currentTime + HEALTH_TICK_TIME;
                ChangeState(_prevState, currentTime);
                ResetHealth();
                return;
            }

            // Conquerer did not switch, continue wresting/capturing w/o health reset
            if (mConqueringGuildId != Guid.Empty)
            {
                mNextHealthTick = currentTime + HEALTH_TICK_TIME;
                ChangeState(_prevState, currentTime);
                return;
            }
        }

        private void ChangeState(TerritoryState state, long currentTime)
        {
            _prevState = State;
            State = state;

#if DEBUG
            Logging.Log.Debug($"Territory {Territory.Name} state change: {_prevState} -> {State}");
#endif
            if (_prevState != State)
            {
                Save();
            }
        }
    }
}
