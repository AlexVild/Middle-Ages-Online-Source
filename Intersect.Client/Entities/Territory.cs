using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Maps;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Config;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Entities
{

    public class Territory
    {
        #region Textures
        private GameTexture TILE_TEXTURE = Globals.ContentManager.GetTexture(TextureType.Misc, "territory_tiles.png");
        private GameTexture FLAG_TEXTURE = Globals.ContentManager.GetTexture(TextureType.Misc, "territory_flags.png");
        #endregion

        #region Source rectangles for tile textures
        private FloatRect NEUTRAL_SRC = new FloatRect(0, 0, 16, 16);
        private FloatRect OWNED_SRC = new FloatRect(0, 16, 16, 16);
        private FloatRect FRIENDLY_CAPTURE_SRC = new FloatRect(0, 16 * 2, 16, 16);
        private FloatRect ENEMY_OWNED_SRC = new FloatRect(0, 16 * 3, 16, 16);
        private FloatRect WRESTING_SRC = new FloatRect(0, 0, 16 * 4, 16);
        private FloatRect ENEMY_CAPTURE_SRC = new FloatRect(0, 16 * 6, 16, 16);
        private FloatRect CONTESTED_SRC = new FloatRect(0, 16 * 6, 16, 16);
        #endregion

        #region Source rectangles for flag textures
        private FloatRect FLAG_BG_SRC = new FloatRect(0, 0, 16, 30);
        private FloatRect NEUTRAL_FLAG_SRC = new FloatRect(16, 0, 16, 30);
        private FloatRect HOSTILE_FLAG_SRC = new FloatRect(0, 30, 16, 30);
        private FloatRect FRIENDLY_FLAG_SRC = new FloatRect(16, 30, 16, 30);
        #endregion

        #region Text colors
        private readonly Color NEUTRAL_NAME = new Color(200, 255, 255, 255);
        private readonly Color HOSTILE_NAME = new Color(200, 222, 124, 112);
        private readonly Color FRIENDLY_NAME = new Color(200, 99, 196, 70);
        #endregion

        private Color TextColor
        {
            get
            {
                if (Owned)
                {
                    return FRIENDLY_NAME;
                }
                if (!string.IsNullOrEmpty(Owner))
                {
                    return HOSTILE_NAME;
                }

                return NEUTRAL_NAME;
            }
        }

        public MapInstance Map { get; set; }

        public byte X { get; set; }

        public byte Y { get; set; }

        public int Radius { get; set; }

        public TerritoryState State { get; set; }

        public Guid TerritoryId { get; set; }

        public TerritoryDescriptor Descriptor { get; set; }

        public long LastStateChangeTime { get; set; }

        public long TimeInState { get; set; }

        public string Owner { get; set; }

        public string Conquerer { get; set; }

        private const int ALPHA_AMT = 2;
        private const int ALPHA_MIN = 25;
        private const int ALPHA_MAX = 65;

        private readonly List<TerritoryState> CAPTURING_STATES = new List<TerritoryState>() { TerritoryState.Capturing, TerritoryState.Wresting };
        private long ALPHA_UPDATE => CAPTURING_STATES.Contains(State) ? 65 : 100;

        private int mAlpha { get; set; }
        private bool mAlphaDir { get; set; }

        private long mLastAlphaUpdate { get; set; }

        private Color TileRenderColor { get; set; }

        private Color StaticRenderColor { get; set; } = new Color(200, 255, 255, 255);

        private bool mAwaitingServer { get; set; }

        private long mLastFgFlash { get; set; }
        private long mFlashRate = 250;
        private bool mFlashed = false;

        private long mLastRequestToServer { get; set; }

        private long mHealth;

        private long mNextHealthTick;

        private double mHealthPct => mHealth / (float)(Descriptor?.CaptureMs ?? 100);

        private List<Player> mCompetitors { get; set; } = new List<Player>();

        /// <summary>
        /// If exceeded, will request an update again
        /// </summary>
        private const long SERVER_WAIT_TIME = 5000;

        public Territory(MapInstance map, byte x, byte y, int radius, Guid descriptorId)
        {
            Map = map;
            X = x;
            Y = y;
            Radius = radius;
            TerritoryId = descriptorId;
            Descriptor = TerritoryDescriptor.Get(TerritoryId);
            State = TerritoryState.Neutral;
            LastStateChangeTime = Timing.Global.MillisecondsUtcUnsynced;
            Owner = string.Empty;
            Conquerer = string.Empty;
            
            ResetAlpha();
            TileRenderColor = new Color(mAlpha, 255, 255, 255);

            // We need to fetch the current state from the server when we're ready
            mAwaitingServer = true;
            // Default of 500 to allow for server to catch up before sending another packet
            mLastRequestToServer = Timing.Global.MillisecondsUtcUnsynced + 250;
            mNextHealthTick = Timing.Global.MillisecondsUtcUnsynced + Options.Instance.ClanWar.HealthTickMs;
            mLastFgFlash = Timing.Global.MillisecondsUtcUnsynced + mFlashRate;
        }

        public void Update()
        {
            if (mAwaitingServer && mLastRequestToServer < Timing.Global.MillisecondsUtcUnsynced)
            {
                PacketSender.SendTerritorySyncRequest(Map.Id, TerritoryId);
                mLastRequestToServer = Timing.Global.MillisecondsUtcUnsynced + SERVER_WAIT_TIME;
            }

            if (Globals.Me == null || Map == null)
            {
                return;
            }

            mCompetitors = Globals.Entities.Values.ToArray().OfType<Player>().Where(pl =>
            {
                if (pl.MapInstance?.Id != Map.Id)
                {
                    return false;
                }

                int left = X - Radius;
                int right = X + Radius;
                int top = Y - Radius;
                int bottom = Y + Radius;

                var dist = Intersect.Utilities.MathHelper.CalculateDistanceToPoint(X, Y, pl.X, pl.Y);
                if (Math.Floor(dist) > Radius)
                {
                    return false;
                }
                return true;
            }).ToList();

            if (State == TerritoryState.Capturing || State == TerritoryState.Wresting)
            {
                TerritoryHelper.TickHealth(ref mNextHealthTick, 
                    ref mHealth, 
                    mCompetitors.Where(comp => comp.InGuild && comp.Guild == Conquerer).Count(), 
                    Timing.Global.MillisecondsUtcUnsynced, 
                    State == TerritoryState.Wresting);
            }
            else if (State == TerritoryState.Contested)
            {
                // Flash the flag
            }
            else
            {
                mHealth = State == TerritoryState.Owned ? Descriptor.CaptureMs : 0;
            }
        }

        public void HandleServerUpdate(TerritoryState state, string newOwner, string conquerer, long health, long timeOffset)
        {
            mAwaitingServer = false;
            mHealth = health;
            ChangeState(state);
            Owner = newOwner;
            Conquerer = conquerer;
            TimeInState = timeOffset;
            mNextHealthTick = Timing.Global.MillisecondsUtcUnsynced + (Options.Instance.ClanWar.HealthTickMs - timeOffset);
        }

        public void Draw()
        {
            DrawTiles();
            DrawFlag();
            DrawName();
            UpdateAlpha();
        }

        private void DrawTiles()
        {
            int left = X - Radius;
            int right = X + Radius;
            int top = Y - Radius;
            int bottom = Y + Radius;

            for (int yy = top; yy <= bottom; yy++)
            {
                for (int xx = left; xx <= right; xx++)
                {
                    var dist = Intersect.Utilities.MathHelper.CalculateDistanceToPoint(X, Y, xx, yy);
                    if (Math.Floor(dist) > Radius)
                    {
                        continue;
                    }

                    if (!MapInstance.TryGetMapInstanceFromCoords(Map?.Id ?? Guid.Empty, xx, yy, out var currMap, out var mapX, out var mapY))
                    {
                        continue;
                    }

                    var tile = Entity.GetTileRectangle(currMap, (byte)mapX, (byte)mapY);
                    Graphics.DrawGameTexture(TILE_TEXTURE, TileStateSrc, tile, TileRenderColor);
                }
            }
        }

        private void DrawFlag()
        {
            DrawFlagBg();
            DrawFlagFg();
        }
        private void DrawName()
        {
            if (!MapInstance.TryGetMapInstanceFromCoords(Map?.Id ?? Guid.Empty, X, Y, out var currMap, out var mapX, out var mapY))
            {
                return;
            }

            var tile = Entity.GetTileRectangle(currMap, (byte)mapX, (byte)(mapY + 1));
            var center = UiHelper.GetTextCenteredStart(Descriptor.DisplayName, Graphics.TerritoryFont, tile.CenterX, tile.CenterY);

            var yPadding = 16;

            Graphics.Renderer.DrawString(Descriptor.DisplayName, Graphics.TerritoryFont, center.X, center.Y + yPadding, 1.0f, TextColor);
        }

        private void DrawFlagBg()
        {
            if (!MapInstance.TryGetMapInstanceFromCoords(Map?.Id ?? Guid.Empty, X, Y, out var currMap, out var mapX, out var mapY))
            {
                return;
            }

            var tile = Entity.GetTileRectangle(currMap, (byte)mapX, (byte)mapY);
            var centerPoint = FloatRect.CenterBetween(tile, FLAG_BG_SRC, true);

            var destRect = new FloatRect(centerPoint.X, 
                centerPoint.Y, 
                FLAG_TEXTURE.ScaledWidth / 2, 
                FLAG_TEXTURE.ScaledHeight / 2);

            Graphics.DrawGameTexture(FLAG_TEXTURE, FLAG_BG_SRC, destRect, StaticRenderColor);
        }

        private void DrawFlagFg()
        {
            if (State == TerritoryState.Neutral)
            {
                return;
            }

            var color = new Color(StaticRenderColor);

            // Flag flashing
            if (State == TerritoryState.Contested)
            {
                if (mLastFgFlash < Timing.Global.MillisecondsUtcUnsynced)
                {
                    mFlashed = !mFlashed;
                    mLastFgFlash = Timing.Global.MillisecondsUtcUnsynced + mFlashRate;
                }
                if (mFlashed)
                {
                    color.A = 100;
                }
            }
            else
            {
                mLastFgFlash = Timing.Global.MillisecondsUtcUnsynced + mFlashRate;
                mFlashed = false;
            }

            if (!MapInstance.TryGetMapInstanceFromCoords(Map?.Id ?? Guid.Empty, X, Y, out var currMap, out var mapX, out var mapY))
            {
                return;
            }

            // SrcRect is sometimes chopped depending on health, so we need to represent that with the destination to draw
            // the flag at the base of the pole
            if (!FlagStateSrc.HasValue)
            {
                return;
            }

            var srcRect = FlagStateSrc.Value;
            
            var tile = Entity.GetTileRectangle(currMap, (byte)mapX, (byte)mapY);
            var centeredDest = FloatRect.CenterBetween(tile, srcRect, true);

            var destRect = new FloatRect(centeredDest.X,
                // at cropAmt == 0, y == centerPoint.Y
                // at cropAmt == 120, y == centerPoint.Y + height / 2
                centeredDest.Y + ((FLAG_TEXTURE.ScaledHeight / 2 - srcRect.Height * Options.Scale) / 2),
                FLAG_TEXTURE.ScaledWidth / 2,
                srcRect.Height * Options.Scale);

            Graphics.DrawGameTexture(FLAG_TEXTURE, srcRect, destRect, StaticRenderColor);
        }

        private void ChangeState(TerritoryState newState)
        {
            if (newState != State)
            {
                LastStateChangeTime = Timing.Global.MillisecondsUtcUnsynced;
            }
            State = newState;
            ResetAlpha();
        }

        private bool Owned => Globals.Me?.Guild == Owner;
        private bool Conquering => Globals.Me?.Guild == Conquerer;

        private FloatRect TileStateSrc
        {
            get
            {
                switch(State)
                {
                    case TerritoryState.Neutral:
                        return NEUTRAL_SRC;
                    case TerritoryState.Owned:
                        if (Owned)
                        {
                            return OWNED_SRC;
                        }
                        else
                        {
                            return ENEMY_OWNED_SRC;
                        }
                    case TerritoryState.Contested:
                        return CONTESTED_SRC;
                    case TerritoryState.Wresting:
                        if (Owned)
                        {
                            return FRIENDLY_CAPTURE_SRC;
                        }

                        return WRESTING_SRC;
                    case TerritoryState.Capturing:
                        if (Conquerer == Globals.Me.Guild)
                        {
                            return FRIENDLY_CAPTURE_SRC;
                        }
                        else
                        {
                            return ENEMY_CAPTURE_SRC;
                        }
                    default:
                        return NEUTRAL_SRC;
                }
            }
        }

        private FloatRect? FlagStateSrc
        {
            get
            {
                FloatRect? src = null;

                if (!string.IsNullOrEmpty(Owner))
                {
                    if (Owned)
                    {
                        src = FRIENDLY_FLAG_SRC;
                    }
                    else
                    {
                        src = HOSTILE_FLAG_SRC;
                    }
                }
                else
                {
                    if (Conquering)
                    {
                        src = FRIENDLY_FLAG_SRC;
                    }
                    else
                    {
                        src = HOSTILE_FLAG_SRC;
                    }
                }

                if (!src.HasValue)
                {
                    return null;
                }

                var rect = src.Value;

                var ogHeight = rect.Height;
                rect.Height = (float)Intersect.Utilities.MathHelper.Clamp((float)Math.Floor(ogHeight * mHealthPct), 0, ogHeight);
                rect.Y += ogHeight - rect.Height;

                return rect;
            }
        }

        private void ResetAlpha()
        {
            mAlpha = ALPHA_MAX;
            mAlphaDir = false;
            mLastAlphaUpdate = Timing.Global.MillisecondsUtcUnsynced;
        }

        private void UpdateAlpha()
        {
            if (Timing.Global.MillisecondsUtcUnsynced < mLastAlphaUpdate + ALPHA_UPDATE)
            {
                return;
            }

            var sign = mAlphaDir ? 1 : -1;

            mAlpha = Intersect.Utilities.MathHelper.Clamp(mAlpha += (sign * ALPHA_AMT), ALPHA_MIN, ALPHA_MAX);
            if (mAlpha == ALPHA_MIN || mAlpha == ALPHA_MAX)
            {
                mAlphaDir = !mAlphaDir;
            }

            mLastAlphaUpdate = Timing.Global.MillisecondsUtcUnsynced;
            TileRenderColor.A = (byte)mAlpha;
        }
    }
}
