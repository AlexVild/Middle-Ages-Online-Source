using System;

using Intersect.Admin.Actions;
using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Maps;
using Intersect.Client.Networking;
using Intersect.Logging;
using Intersect.Updater;
using Intersect.Utilities;
using static Intersect.Client.Core.Fade;

namespace Intersect.Client.Core
{

    public static class Input
    {

        public delegate void HandleKeyEvent(Keys key);

        public static HandleKeyEvent KeyDown;

        public static HandleKeyEvent KeyUp;

        public static HandleKeyEvent MouseDown;

        public static HandleKeyEvent MouseUp;

        public static void HandleZoomOut(bool wrap = true)
        {
            var nextZoom = Globals.Database.WorldZoom / 2;
            if (nextZoom < Graphics.BaseWorldScale)
            {
                if (wrap)
                {
                    Audio.AddGameSound("ui_click.wav", false);
                    Globals.Database.WorldZoom = Graphics.BaseWorldScale * 4;
                }
                return;
            }

            Audio.AddGameSound("ui_release.wav", false);
            Globals.Database.WorldZoom = nextZoom;
        }

        public static void HandleZoomIn(bool wrap = true)
        {
            var nextZoom = Globals.Database.WorldZoom * 2;
            if (nextZoom > Graphics.BaseWorldScale * 4)
            {
                if (wrap)
                {
                    Audio.AddGameSound("ui_release.wav", false);
                    Globals.Database.WorldZoom = Graphics.BaseWorldScale;
                }
                return;
            }
            
            Audio.AddGameSound("ui_click.wav", false);
            Globals.Database.WorldZoom = nextZoom;
        }

        public static void OnKeyPressed(Keys key)
        {
            if (key == Keys.None)
            {
                return;
            }

            var consumeKey = false;
            bool canFocusChat = true;

            KeyDown?.Invoke(key);
            switch (key)
            {
                case Keys.Escape:
                    if (Globals.GameState == GameStates.Menu && Graphics.LogoAlpha < 255)
                    {
                        Graphics.EndLogoFade();
                        break;
                    }
                    if (Globals.GameState != GameStates.Intro || FadeService.FadeType == FadeType.Out)
                    {
                        break;
                    }
                    
                    Audio.StopAllSounds();
                    FadeService.FadeOut(callback: () =>
                    {
                        Globals.GameState = GameStates.Menu;
                        Graphics.ResetMenu();
                        FadeService.FadeIn();
                    });
                    
                    return;

                case Keys.Enter:

                    for (int i = Interface.Interface.InputBlockingElements.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            var iBox = (InputBox)Interface.Interface.InputBlockingElements[i];
                            if (iBox != null && !iBox.IsHidden)
                            {
                                iBox.okayBtn_Clicked(null, null);
                                canFocusChat = false;

                                break;
                            }
                        }
                        catch { }
                    }

                    if (TryEventDialogResponse(0))
                    {
                        return;
                    }

                    break;

                case Keys.Space:
                    if (TryEventDialogResponse(0))
                    {
                        return;
                    }
                    break;

                case Keys.NumPad1:
                case Keys.D1:
                    if (TryEventDialogResponse(0))
                    {
                        return;
                    }
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    if (TryEventDialogResponse(1))
                    {
                        return;
                    }
                    break;
                case Keys.NumPad3:
                case Keys.D3:
                    if (TryEventDialogResponse(2))
                    {
                        return;
                    }
                    break;
                case Keys.NumPad4:
                case Keys.D4:
                    if (TryEventDialogResponse(3))
                    {
                        return;
                    }
                    break;
            }

            if (Controls.Controls.ControlHasKey(Control.OpenMenu, key))
            {
                if (Globals.GameState != GameStates.InGame)
                {
                    return;
                }

                // First try and unfocus chat then close all UI elements, then untarget our target.. and THEN open the escape menu.
                // Most games do this, why not this?
                if (Interface.Interface.GameUi != null && Interface.Interface.GameUi.ChatFocussed)
                {
                    Interface.Interface.GameUi.UnfocusChat = true;
                }
                else if (Interface.Interface.GameUi != null && Interface.Interface.GameUi.CloseAllWindows())
                {
                    // We've closed our windows, don't do anything else. :)
                }
                else if (Globals.Me != null && Globals.Me.TargetIndex != Guid.Empty)
                {
                    Globals.Me.ClearTarget();
                }
                else
                {
                    Interface.Interface.GameUi?.EscapeMenu?.ToggleHidden();
                }
            }

            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            Controls.Controls.GetControlsFor(key)
                ?.ForEach(
                    control =>
                    {
                        if (consumeKey)
                        {
                            return;
                        }

                        switch (control)
                        {
                            case Control.Screenshot:
                                Graphics.Renderer?.RequestScreenshot();

                                break;

                            case Control.ToggleGui:
                                if (Globals.GameState == GameStates.InGame)
                                {
                                    Interface.Interface.HideUi = !Interface.Interface.HideUi;
                                }

                                break;
                        }

                        switch (Globals.GameState)
                        {
                            case GameStates.Preloading:
                                break;

                            case GameStates.Intro:
                                break;

                            case GameStates.Menu:
                                break;

                            case GameStates.InGame:
                                switch (control)
                                {
                                    case Control.MoveUp:
                                        break;

                                    case Control.MoveLeft:
                                        break;

                                    case Control.MoveDown:
                                        break;

                                    case Control.MoveRight:
                                        break;

                                    case Control.TurnClockwise:
                                        break;

                                    case Control.TurnCounterClockwise:
                                        break;

                                    case Control.AttackInteract:

                                    case Control.Block:
                                        Globals.Me?.TryBlock();

                                        break;

                                    case Control.AutoTarget:
                                        Globals.Me?.TryAutoTarget(false);

                                        break;

                                    case Control.PickUp:
                                        Globals.Me?.TryPickupItem(Globals.Me.MapInstance.Id, Globals.Me.Y * Options.MapWidth + Globals.Me.X);

                                        break;

                                    case Control.Enter:
                                        if (Interface.Interface.GameUi.ChatIsHidden())
                                        {
                                            Interface.Interface.GameUi.OpenQuickChat();
                                        }

                                        if (canFocusChat)
                                        {
                                            Interface.Interface.GameUi.FocusChat = true;
                                            consumeKey = true;
                                        }

                                        return;

                                    case Control.Hotkey1:
                                    case Control.Hotkey2:
                                    case Control.Hotkey3:
                                    case Control.Hotkey4:
                                    case Control.Hotkey5:
                                    case Control.Hotkey6:
                                    case Control.Hotkey7:
                                    case Control.Hotkey8:
                                    case Control.Hotkey9:
                                    case Control.Hotkey0:
                                        break;

                                    case Control.OpenInventory:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleInventoryWindow();

                                        break;

                                    case Control.OpenQuests:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleQuestsWindow();

                                        break;

                                    case Control.OpenCharacterInfo:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleCharacterWindow();

                                        break;

                                    case Control.OpenParties:
                                        Interface.Interface.GameUi?.GameMenu?.TogglePartyWindow();

                                        break;

                                    case Control.OpenSpells:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleSpellsWindow();

                                        break;

                                    case Control.OpenFriends:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleFriendsWindow();

                                        break;

                                    case Control.OpenSettings:
                                        Interface.Interface.GameUi?.EscapeMenu?.OpenSettingsWindow();

                                        break;

                                    case Control.ToggleZoomIn:
                                        HandleZoomIn();

                                        break;

                                    case Control.ToggleZoomOut:
                                        HandleZoomOut();

                                        break;

                                    case Control.OpenDebugger:
                                        Interface.Interface.GameUi?.ShowHideDebug();

                                        break;

                                    case Control.OpenAdminPanel:
                                        PacketSender.SendOpenAdminWindow();

                                        break;

                                    case Control.OpenGuild:
                                        Interface.Interface.GameUi?.GameMenu.ToggleGuildWindow();

                                        break;

                                    case Control.FaceTarget:
                                        Globals.Me.ToggleCombatMode(true);

                                        break;
                                    case Control.TargetParty1:
                                        Globals.Me.TargetPartyMember(0);
                                        
                                        break;
                                    case Control.TargetParty2:
                                        Globals.Me.TargetPartyMember(1);

                                        break;
                                    case Control.TargetParty3:
                                        Globals.Me.TargetPartyMember(2);

                                        break;
                                    case Control.TargetParty4:
                                        Globals.Me.TargetPartyMember(4);

                                        break;
                                    case Control.OpenOverworldMap:
                                        Interface.Interface.GameUi?.Map.ToggleOpen();

                                        break;
                                    case Control.OpenBestiary:
                                        Interface.Interface.GameUi?.GameMenu.ToggleBestiaryWindow();

                                        break;
                                    default:
                                        break;
                                }

                                break;

                            case GameStates.Loading:
                                break;

                            case GameStates.Error:
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(
                                    nameof(Globals.GameState), Globals.GameState, null
                                );
                        }
                    }
                );
        }

        public static bool TryEventDialogResponse(int response)
        {
            try
            {
                var eventWindow = Interface.Interface.GameUi?.EventWindow;
                if (eventWindow == null || eventWindow.IsHidden || Globals.EventDialogs.Count <= 0)
                {
                    return false;
                }

                eventWindow.SkipTypewriting();

                if (!eventWindow.ResponseAvailable(response))
                {
                    return false;
                }

                switch (response)
                {
                    case 0:
                        eventWindow.EventResponse1_Clicked(null, null);
                        return true;
                    case 1:
                        eventWindow.EventResponse2_Clicked(null, null);
                        return true;
                    case 2:
                        eventWindow.EventResponse3_Clicked(null, null);
                        return true;
                    case 3:
                        eventWindow.EventResponse4_Clicked(null, null);
                        return true;
                }
            }
            catch { }

            return false;
        }

        public static void OnKeyReleased(Keys key)
        {
            KeyUp?.Invoke(key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (Controls.Controls.ControlHasKey(Control.Block, key))
            {
                Globals.Me.StopBlocking();
            }

            if (Controls.Controls.ControlHasKey(Control.TurnClockwise, key) || Controls.Controls.ControlHasKey(Control.TurnCounterClockwise, key))
            {
                Globals.Me.DirKeyPressed = false;
            }
        }

        public static void OnMouseDown(GameInput.MouseButtons btn)
        {
            var key = Keys.None;
            switch (btn)
            {
                case GameInput.MouseButtons.Left:
                    key = Keys.LButton;

                    break;

                case GameInput.MouseButtons.Right:
                    key = Keys.RButton;

                    break;

                case GameInput.MouseButtons.Middle:
                    key = Keys.MButton;

                    break;
                case GameInput.MouseButtons.X1:
                    key = Keys.XButton1;

                    break;
                case GameInput.MouseButtons.X2:
                    key = Keys.XButton2;

                    break;
            }

            MouseDown?.Invoke(key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.GameState != GameStates.InGame || Globals.Me == null)
            {
                return;
            }

            if (Interface.Interface.MouseHitGui())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (Controls.Controls.ControlHasKey(Control.FaceTarget, key))
            {
                if (Globals.Database.ClassicMode && Globals.Me.TryFaceTarget(false, true))
                {
                    return;
                }
                else if (!Globals.Database.ClassicMode)
                {
                    Globals.Me.ToggleCombatMode();
                }
            }

            if (Globals.Database.LeftClickTarget)
            {
                if (key == Keys.LButton && Globals.Me.TryTarget())
                {
                    return;
                }
            } else
            {
                if (key == Keys.RButton && Globals.Me.TryTarget())
                {
                    return;
                }
            }

            if (Controls.Controls.ControlHasKey(Control.PickUp, key))
            {
                if (Globals.Me.TryPickupItem(Globals.Me.MapInstance.Id, Globals.Me.Y * Options.MapWidth + Globals.Me.X, Guid.Empty, true))
                {
                    return;
                }

                if (Globals.Me.AttackTimer < Timing.Global.Ticks / TimeSpan.TicksPerMillisecond)
                {
                    Globals.Me.AttackTimer = Timing.Global.Ticks / TimeSpan.TicksPerMillisecond + Globals.Me.CalculateAttackTime();
                }
            }

            if (Controls.Controls.ControlHasKey(Control.Block, key))
            {
                if (Globals.Me.TryBlock())
                {
                    return;
                }
            }

            if (key != Keys.None)
            {
                OnKeyPressed(key);
            }
        }

        public static void OnMouseUp(GameInput.MouseButtons btn)
        {
            var key = Keys.LButton;
            switch (btn)
            {
                case GameInput.MouseButtons.Right:
                    key = Keys.RButton;

                    break;

                case GameInput.MouseButtons.Middle:
                    key = Keys.MButton;

                    break;
                case GameInput.MouseButtons.X1:
                    key = Keys.XButton1;

                    break;
                case GameInput.MouseButtons.X2:
                    key = Keys.XButton2;

                    break;
            }

            MouseUp?.Invoke(key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (Controls.Controls.ControlHasKey(Control.Block, key))
            {
                Globals.Me.StopBlocking();
            }

            if (btn != GameInput.MouseButtons.Right)
            {
                return;
            }


            var inventoryOpen = Interface.Interface.GameUi?.GameMenu?.InventoryWindowIsVisible() ?? false;
            if (Globals.InputManager.KeyDown(Keys.Alt) != true)
            {
                return;
            }

            var mouseInWorld = Graphics.ConvertToWorldPoint(Globals.InputManager.GetMousePosition());
            var x = (int)mouseInWorld.X;
            var y = (int)mouseInWorld.Y;

            foreach (MapInstance map in MapInstance.Lookup.Values)
            {
                if (!(x >= map.GetX()) || !(x <= map.GetX() + Options.MapWidth * Options.TileWidth))
                {
                    continue;
                }

                if (!(y >= map.GetY()) || !(y <= map.GetY() + Options.MapHeight * Options.TileHeight))
                {
                    continue;
                }

                //Remove the offsets to just be dealing with pixels within the map selected
                x -= (int) map.GetX();
                y -= (int) map.GetY();

                //transform pixel format to tile format
                x /= Options.TileWidth;
                y /= Options.TileHeight;
                var mapNum = map.Id;

                if (Globals.Me.GetRealLocation(ref x, ref y, ref mapNum))
                {
                    PacketSender.SendAdminAction(new WarpToLocationAction(map.Id, (byte) x, (byte) y));
                }

                return;
            }
        }

        public static bool QuickModifierActive()
        {
            return Globals.InputManager.KeyDown(Keys.Shift);
        }
    }

}
