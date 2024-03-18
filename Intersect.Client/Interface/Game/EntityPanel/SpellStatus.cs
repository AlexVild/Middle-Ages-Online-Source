using System;

using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.EntityPanel
{

    public class SpellStatus
    {

        public ImagePanel Container;

        private Guid mCurrentSpellId;

        private SpellDescriptionWindow mDescWindow;

        private Framework.Gwen.Control.Label mDurationLabel;

        //Drag/Drop References
        private EntityBox mEntityBox;

        private Status mStatus;

        private string mTexLoaded = "";

        public ImagePanel Pnl;

        public Framework.Gwen.Control.Label TimeLabel;

        public SpellStatus(EntityBox entityBox, Status status)
        {
            mEntityBox = entityBox;
            mStatus = status;
        }

        public void Setup()
        {
            Pnl = new ImagePanel(Container, "StatusIcon");
            Pnl.HoverEnter += pnl_HoverEnter;
            Pnl.HoverLeave += pnl_HoverLeave;
            mDurationLabel = new Framework.Gwen.Control.Label(Container, "DurationLabel");
        }

        public void pnl_HoverLeave(Base sender, EventArgs arguments)
        {
            if (mDescWindow != null)
            {
                mDescWindow.Dispose();
                mDescWindow = null;
            }
        }

        void pnl_HoverEnter(Base sender, EventArgs arguments)
        {
            if (mDescWindow != null)
            {
                mDescWindow.Dispose();
                mDescWindow = null;
            }

            mDescWindow = new SpellDescriptionWindow(
                mStatus.SpellId, mEntityBox.EntityWindow.X + Pnl.X + 16,
                mEntityBox.EntityWindow.Y + Container.Parent.Y + Container.Bottom + 2
            );
        }

        public FloatRect RenderBounds()
        {
            var rect = new FloatRect
            {
                X = Pnl.LocalPosToCanvas(new Point(0, 0)).X,
                Y = Pnl.LocalPosToCanvas(new Point(0, 0)).Y,
                Width = Pnl.Width,
                Height = Pnl.Height
            };

            return rect;
        }

        public void UpdateStatus(Status status)
        {
            mStatus = status;
        }

        private void SetDurationText(float remainingMs)
        {
            var secondsRemaining = remainingMs / 1000f;
            var minutesRemaining = secondsRemaining / 60f;
            var hoursRemaining = minutesRemaining / 60f;
            var daysRemaining = hoursRemaining / 24f;

            if (daysRemaining >= 1)
            {
                var hourRemainder = hoursRemaining - (hoursRemaining * daysRemaining);
                mDurationLabel.Text = $"{hoursRemaining.ToString("N0")}d {hourRemainder.ToString("N0")}h";
                return;
            }

            if (hoursRemaining >= 1)
            {
                var minuteRemainder = minutesRemaining - (hoursRemaining * 60);
                mDurationLabel.Text = $"{hoursRemaining.ToString("N0")}h {minuteRemainder.ToString("N0")}m";
                return;
            }

            if (secondsRemaining > 90f)
            {
                var secondsRemainder = secondsRemaining - (secondsRemaining * 60);
                mDurationLabel.Text = $"{minutesRemaining.ToString("N0")}m {secondsRemainder}s";
                return;
            }
            
            if (secondsRemaining > 10f)
            {
                mDurationLabel.Text =
                    Strings.EntityBox.cooldown.ToString(secondsRemaining.ToString("N0"));
                return;
            }

            mDurationLabel.Text = Strings.EntityBox.cooldown.ToString(
                (secondsRemaining).ToString("N1").Replace(".", Strings.Numbers.dec)
            );
        }

        public void Update()
        {
            if (mStatus != null)
            {
                var remaining = mStatus.RemainingMs;
                var spell = SpellBase.Get(mStatus.SpellId);

                SetDurationText(mStatus.RemainingMs);

                if ((mTexLoaded != "" && spell == null ||
                     spell != null && mTexLoaded != spell.Icon ||
                     mCurrentSpellId != mStatus.SpellId) &&
                    remaining > 0)
                {
                    Container.Show();
                    if (spell != null)
                    {
                        var spellTex = Globals.ContentManager.GetTexture(
                            GameContentManager.TextureType.Spell, spell.Icon
                        );

                        if (spellTex != null)
                        {
                            Pnl.Texture = spellTex;
                            Pnl.IsHidden = false;
                        }
                        else
                        {
                            if (Pnl.Texture != null)
                            {
                                Pnl.Texture = null;
                            }
                        }

                        mTexLoaded = spell.Icon;
                        mCurrentSpellId = mStatus.SpellId;
                    }
                    else
                    {
                        if (Pnl.Texture != null)
                        {
                            Pnl.Texture = null;
                        }

                        mTexLoaded = "";
                    }
                }
                else if (remaining <= 0)
                {
                    if (Pnl.Texture != null)
                    {
                        Pnl.Texture = null;
                    }

                    Container.Hide();
                    mTexLoaded = "";
                }
            }
        }

    }

}
