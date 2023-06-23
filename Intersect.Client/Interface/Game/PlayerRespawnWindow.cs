using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.ScreenAnimations;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game
{
    public class PlayerRespawnWindow : Base
    {
        public Canvas GameCanvas;
        public bool RequestingRespawn = false;

        private ImagePanel Background;

        private ScrollControl TextContainer;
        private RichLabel DeathText;
        private Label DeathTextTemplate;

        private Button NormalRespawnButton;
        private Button LeaveInstanceButton;
        private Button DungeonRespawnButton;

        private Gravestone GravestoneAnimation;

        public PlayerRespawnWindow(Canvas gameCanvas)
        {
            GameCanvas = gameCanvas;

            Background = new ImagePanel(GameCanvas, "PlayerRespawnWindow");

            TextContainer = new ScrollControl(Background, "TextContainer");
            DeathTextTemplate = new Label(TextContainer, "DeathInfoLabel");
            DeathText = new RichLabel(TextContainer);

            NormalRespawnButton = new Button(Background, "NormalRespawnButton")
            {
                Text = Strings.RespawnWindow.RespawnButton,
            };
            NormalRespawnButton.Clicked += NormalRespawnButton_Clicked;

            LeaveInstanceButton = new Button(Background, "LeaveInstanceButton")
            {
                Text = Strings.RespawnWindow.LeaveInstanceButton
            };
            LeaveInstanceButton.Clicked += LeaveInstanceButton_Clicked;

            DungeonRespawnButton = new Button(Background, "DungeonRespawnButton")
            {
                Text = Strings.RespawnWindow.InstanceRespawnButton
            };
            DungeonRespawnButton.Clicked += NormalRespawnButton_Clicked;

            Interface.InputBlockingElements.Add(Background);

            Background.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            SetType(DeathType.Safe, -1, new List<string>());

            GravestoneAnimation = new Gravestone();
        }

        public void Update()
        {
            Background.IsHidden = !Globals.Me?.IsDead ?? true;
            if (Background.IsHidden)
            {
                return;
            }

            Graphics.DrawGameTexture(Graphics.Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), Graphics.CurrentView, new Color(150, 0, 0, 0));
            GravestoneAnimation.Draw();

            NormalRespawnButton.IsDisabled = RequestingRespawn;
            LeaveInstanceButton.IsDisabled = RequestingRespawn;
            DungeonRespawnButton.IsDisabled = RequestingRespawn || Globals.Me?.DungeonLives > Options.Instance.Instancing.MaxSharedInstanceLives;
        }

        public void SetType(DeathType deathType, long expLost, List<string> itemsLost)
        {
            DeathText.ClearText();
            DungeonRespawnButton.Hide();
            LeaveInstanceButton.Hide();
            NormalRespawnButton.Hide();
            DeathText.Width = TextContainer.Width - TextContainer.GetVerticalScrollBar().Width;
            if (deathType == DeathType.PvE)
            {
                DeathText.AddText(Strings.RespawnWindow.DeathPvE.ToString(expLost), DeathTextTemplate);
                NormalRespawnButton.Show();
            }
            if (deathType == DeathType.Duel)
            {
                DeathText.AddText(Strings.RespawnWindow.DeathDuel, DeathTextTemplate);
                NormalRespawnButton.Show();
            }
            if (deathType == DeathType.PvP)
            {
                DeathText.AddText(Strings.RespawnWindow.DeathItems.ToString(expLost), DeathTextTemplate);
                NormalRespawnButton.Show();
            }
            if (deathType == DeathType.Safe)
            {
                DeathText.AddText(Strings.RespawnWindow.DeathSafe, DeathTextTemplate);
                NormalRespawnButton.Show();
            }
            if (deathType == DeathType.Dungeon)
            {
                if (Globals.Me?.DungeonLives > Options.Instance.Instancing.MaxSharedInstanceLives)
                {
                    DeathText.AddText(Strings.RespawnWindow.DeathDungeonFinal, DeathTextTemplate);
                }
                else
                {
                    if (Globals.Me?.IsInParty() ?? false)
                    {
                        DeathText.AddText(Strings.RespawnWindow.DeathDungeon.ToString(Globals.Me?.DungeonLives + 1 ?? 0), DeathTextTemplate);
                    }
                    else
                    {
                        DeathText.AddText(Strings.RespawnWindow.DeathDungeonSolo.ToString(Globals.Me?.DungeonLives + 1 ?? 0), DeathTextTemplate);
                    }
                }
                
                DungeonRespawnButton.Show();
                LeaveInstanceButton.Show();
            }

            DeathText.SizeToChildren(false, true);
            TextContainer.ScrollToTop();
        }

        #region Handlers
        void NormalRespawnButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            RequestRespawn();
        }

        void LeaveInstanceButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.Me?.DungeonLives <= Options.Instance.Instancing.MaxSharedInstanceLives)
            {
                OpenInstanceLeaveMenu();
            }
            else
            {
                RequestInstanceLeave(null, null);
            }
        }

        private void RequestRespawn()
        {
            RequestingRespawn = true;

            FadeService.FadeOut(false, false, () =>
            {
                PacketSender.SendRequestRespawn();
                FadeService.FadeIn();
            });
        }

        private void OpenInstanceLeaveMenu()
        {
            var box = new InputBox(
                Strings.RespawnWindow.LeaveInstanceTitle, Strings.RespawnWindow.LeaveInstancePrompt, true, InputBox.InputType.YesNo,
                RequestInstanceLeave, null, null
            );
        }

        private void RequestInstanceLeave(object sender, EventArgs e)
        {
            RequestingRespawn = true;

            FadeService.FadeOut(false, false, () =>
            {
                PacketSender.SendRequestInstanceLeave();
                FadeService.FadeIn();
            });
        }

        public void ServerRespawned()
        {
            RequestingRespawn = false;
        }
        #endregion
    }
}
