using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Client.Networking;
using Intersect.Config;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Entities
{

    public class Territory
    {
        private GameTexture TILE_TEXTURE = Globals.ContentManager.GetTexture(TextureType.Misc, "territory_tiles.png");
        private FloatRect NEUTRAL_SRC = new FloatRect(0, 0, 16, 16);
        private FloatRect OWNED_SRC = new FloatRect(0, 16, 16, 16);
        private FloatRect FRIENDLY_CAPTURE_SRC = new FloatRect(0, 16 * 2, 16, 16);
        private FloatRect ENEMY_OWNED_SRC = new FloatRect(0, 16 * 3, 16, 16);
        private FloatRect WRESTING_SRC = new FloatRect(0, 0, 16 * 4, 16);
        private FloatRect ENEMY_CAPTURE_SRC = new FloatRect(0, 16 * 6, 16, 16);
        private FloatRect CONTESTED_SRC = new FloatRect(0, 16 * 6, 16, 16);

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

        private Color RenderColor { get; set; }

        private bool mAwaitingServer { get; set; }

        private long mLastRequestToServer { get; set; }

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
            RenderColor = new Color(mAlpha, 255, 255, 255);

            // We need to fetch the current state from the server when we're ready
            mAwaitingServer = true;
            // Default of 500 to allow for server to catch up before sending another packet
            mLastRequestToServer = Timing.Global.MillisecondsUtcUnsynced + 500;
        }

        public void Update()
        {
            if (mAwaitingServer && mLastRequestToServer < Timing.Global.MillisecondsUtcUnsynced)
            {
                PacketSender.SendTerritorySyncRequest(Map.Id, TerritoryId);
                mLastRequestToServer = Timing.Global.MillisecondsUtcUnsynced + SERVER_WAIT_TIME;
            }
        }

        public void HandleServerUpdate(TerritoryState state, string newOwner, string conquerer, long timeOffset = 0)
        {
            mAwaitingServer = false;
            ChangeState(state);
            Owner = newOwner;
            Conquerer = conquerer;
            TimeInState = timeOffset;
        }

        public void Draw()
        {
            DrawTiles();
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
                    var dist = MathHelper.CalculateDistanceToPoint(X, Y, xx, yy);
                    if (Math.Floor(dist) > Radius)
                    {
                        continue;
                    }

                    if (!MapInstance.TryGetMapInstanceFromCoords(Map?.Id ?? Guid.Empty, xx, yy, out var currMap, out var mapX, out var mapY))
                    {
                        continue;
                    }

                    var tile = Entity.GetTileRectangle(currMap, (byte)mapX, (byte)mapY);
                    Graphics.DrawGameTexture(TILE_TEXTURE, StateSrc, tile, RenderColor);
                }
            }
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

        private FloatRect StateSrc
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

            mAlpha = MathHelper.Clamp(mAlpha += (sign * ALPHA_AMT), ALPHA_MIN, ALPHA_MAX);
            if (mAlpha == ALPHA_MIN || mAlpha == ALPHA_MAX)
            {
                mAlphaDir = !mAlphaDir;
            }

            mLastAlphaUpdate = Timing.Global.MillisecondsUtcUnsynced;
            RenderColor.A = (byte)mAlpha;
        }
    }
}
