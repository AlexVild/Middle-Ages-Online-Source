﻿using Intersect.Client.Core;
using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Character;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.Game.Spells;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game
{

    public partial class Menu
    {

        private readonly ImagePanel mCharacterBackground;

        private readonly Button mCharacterButton;

        private readonly CharacterWindowMAO mCharacterWindow;

        private readonly ImagePanel mFriendsBackground;

        private readonly Button mFriendsButton;

        private readonly FriendsWindow mFriendsWindow;

        private readonly ImagePanel mInventoryBackground;

        private readonly Button mInventoryButton;

        private readonly InventoryWindow mInventoryWindow;

        private readonly ImagePanel mMenuBackground;

        private readonly Button mMenuButton;

        //Menu Container
        private readonly ImagePanel mMenuContainer;

        private readonly ImagePanel mPartyBackground;

        private readonly Button mPartyButton;

        private readonly PartyWindow mPartyWindow;

        private readonly ImagePanel mQuestsBackground;

        private readonly Button mQuestsButton;

        private readonly QuestsWindow mQuestsWindow;

        private readonly ImagePanel mSpellsBackground;

        private readonly Button mSpellsButton;

        private readonly SpellsWindow mSpellsWindow;

        private readonly ImagePanel mGuildBackground;

        private readonly Button mGuildButton;

        private readonly GuildWindow mGuildWindow;

        private int mBackgroundHeight = 42;

        private int mBackgroundWidth = 42;

        private int mButtonHeight = 34;

        private int mButtonMargin = 8;

        private int mButtonWidth = 34;

        private long mNotificationTimer = 0;

        private bool mNotificationFlash = false;

        private string characterImage = Options.Chat.MenuCharacterIcon;

        //Canvas instance
        private Canvas mGameCanvas;

        //Init
        public Menu(Canvas gameCanvas)
        {
            mGameCanvas = gameCanvas;

            mMenuContainer = new ImagePanel(gameCanvas, "MenuContainer");
            mMenuContainer.ShouldCacheToTexture = true;

            mInventoryBackground = new ImagePanel(mMenuContainer, "InventoryContainer");
            mInventoryButton = new Button(mInventoryBackground, "InventoryButton");
            mInventoryButton.SetToolTipText(Strings.GameMenu.items);
            mInventoryButton.Clicked += InventoryButton_Clicked;

            mSpellsBackground = new ImagePanel(mMenuContainer, "SpellsContainer");
            mSpellsButton = new Button(mSpellsBackground, "SpellsButton");
            mSpellsButton.SetToolTipText(Strings.GameMenu.spells);
            mSpellsButton.Clicked += SpellsButton_Clicked;

            mCharacterBackground = new ImagePanel(mMenuContainer, "CharacterContainer");
            mCharacterButton = new Button(mCharacterBackground, "CharacterButton");
            mCharacterButton.SetToolTipText(Strings.GameMenu.character);
            mCharacterButton.Clicked += CharacterButton_Clicked;

            mQuestsBackground = new ImagePanel(mMenuContainer, "QuestsContainer");
            mQuestsButton = new Button(mQuestsBackground, "QuestsButton");
            mQuestsButton.SetToolTipText(Strings.GameMenu.quest);
            mQuestsButton.Clicked += QuestBtn_Clicked;

            mFriendsBackground = new ImagePanel(mMenuContainer, "FriendsContainer");
            mFriendsButton = new Button(mFriendsBackground, "FriendsButton");
            mFriendsButton.SetToolTipText(Strings.GameMenu.friends);
            mFriendsButton.Clicked += FriendsBtn_Clicked;

            mPartyBackground = new ImagePanel(mMenuContainer, "PartyContainer");
            mPartyButton = new Button(mPartyBackground, "PartyButton");
            mPartyButton.SetToolTipText(Strings.GameMenu.party);
            mPartyButton.Clicked += PartyBtn_Clicked;

            mGuildBackground = new ImagePanel(mMenuContainer, "GuildContainer");
            mGuildButton = new Button(mGuildBackground, "GuildButton");
            mGuildButton.SetToolTipText(Strings.Guilds.Guild);
            mGuildButton.Clicked += GuildBtn_Clicked;

            mMenuBackground = new ImagePanel(mMenuContainer, "MenuContainer");
            mMenuButton = new Button(mMenuBackground, "MenuButton");
            mMenuButton.SetToolTipText(Strings.GameMenu.Menu);
            mMenuButton.Clicked += MenuButtonClicked;

            _Menu();

            mMenuContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            //Assign Window References
            mPartyWindow = new PartyWindow(gameCanvas);
            mFriendsWindow = new FriendsWindow(gameCanvas);
            mInventoryWindow = new InventoryWindow(gameCanvas);
            mSpellsWindow = new SpellsWindow(gameCanvas);
            mCharacterWindow = new CharacterWindowMAO(gameCanvas);
            mQuestsWindow = new QuestsWindow(gameCanvas);
            mGuildWindow = new GuildWindow(gameCanvas);
        }

        //Methods
        public void Update(bool updateQuestLog)
        {
            if (Globals.Me.InCutscene())
            {
                mMenuContainer.Hide();
            }
            else
            {
                mMenuContainer.Show();
            }

            if (Globals.Me.StatPoints > 0 && !mCharacterWindow.IsVisible())
            {
                var time = Timing.Global.Milliseconds;
                if (!mNotificationFlash)
                {
                    mNotificationFlash = true;
                    mNotificationTimer = time + Options.Chat.MenuNotificationFlashInterval;

                    if (characterImage == Options.Chat.MenuCharacterIconFlashed)
                    {
                        characterImage = Options.Chat.MenuCharacterIcon;
                    }
                    else
                    {
                        characterImage = Options.Chat.MenuCharacterIconFlashed;
                    }
                }
                
                if (mNotificationTimer < time)
                {
                    mNotificationFlash = false;
                }

                mCharacterButton.SetImage(null, characterImage, Button.ControlState.Normal);
                mCharacterButton.SetImage(null, characterImage, Button.ControlState.Hovered);
                mCharacterButton.Redraw();
            } else
            {
                if (mCharacterButton.GetImage(Button.ControlState.Normal).GetName() != Options.Chat.MenuCharacterIcon)
                {
                    mCharacterButton.SetImage(null, Options.Chat.MenuCharacterIcon, Button.ControlState.Normal);
                    mCharacterButton.SetImage(null, Options.Chat.MenuCharacterIcon, Button.ControlState.Hovered);
                    mCharacterButton.Redraw();
                }
            }

            mInventoryWindow.Update();
            mSpellsWindow.Update();
            mCharacterWindow.Update();
            mPartyWindow.Update();
            mFriendsWindow.Update();
            mQuestsWindow.Update(updateQuestLog);
            mGuildWindow.Update();
        }

        public void UpdateFriendsList()
        {
            mFriendsWindow.UpdateList();
        }

        public void UpdateGuildList()
        {
            mGuildWindow.UpdateList();
        }

        public void HideWindows()
        {
            if (!Globals.Database.HideOthersOnWindowOpen)
            {
                return;
            }

            mCharacterWindow.Hide();
            mFriendsWindow.Hide();
            mInventoryWindow.Hide();
            mPartyWindow.Hide();
            mQuestsWindow.Hide();
            mSpellsWindow.Hide();
            mGuildWindow.Hide();
        }

        public void ToggleCharacterWindow()
        {
            if (mCharacterWindow.IsVisible())
            {
                mCharacterWindow.Hide();
            }
            else
            {
                HideWindows();
                PacketSender.SendCraftingInfoPacket(); // fetch additional player info, i.e mining tier; class rank
                mCharacterWindow.Show();
            }
        }

        public bool ToggleFriendsWindow()
        {
            if (mFriendsWindow.IsVisible())
            {
                mFriendsWindow.Hide();
            }
            else
            {
                HideWindows();
                PacketSender.SendRequestFriends();
                mFriendsWindow.UpdateList();
                mFriendsWindow.Show();
            }

            return mFriendsWindow.IsVisible();
        }

        public bool ToggleGuildWindow()
        {
            if (mGuildWindow.IsVisible())
            {
                mGuildWindow.Hide();
            }
            else
            {
                HideWindows();
                PacketSender.SendRequestGuild();
                mGuildWindow.UpdateList();
                mGuildWindow.Show();
            }

            return mGuildWindow.IsVisible();
        }

        public void HideGuildWindow()
        {
            mGuildWindow.Hide();
        }

        public void ToggleInventoryWindow()
        {
            if (mInventoryWindow.IsVisible())
            {
                mInventoryWindow.Hide();
            }
            else
            {
                HideWindows();
                mInventoryWindow.Show();
            }
        }

        public void OpenInventory()
        {
            mInventoryWindow.Show();
        }

        public InventoryWindow GetInventoryWindow()
        {
            return mInventoryWindow;
        }

        public bool InventoryWindowIsVisible()
        {
            return mInventoryWindow.IsVisible();
        }

        public void TogglePartyWindow()
        {
            if (mPartyWindow.IsVisible())
            {
                mPartyWindow.Hide();
            }
            else
            {
                HideWindows();
                mPartyWindow.Show();
            }
        }

        public void ToggleQuestsWindow()
        {
            if (mQuestsWindow.IsVisible())
            {
                mQuestsWindow.Hide();
            }
            else
            {
                PacketSender.SendQuestPointRequestPacket(); // request quest point info here
                HideWindows();
                mQuestsWindow.Show();
            }
        }

        public void ToggleSpellsWindow()
        {
            if (mSpellsWindow.IsVisible())
            {
                mSpellsWindow.Hide();
            }
            else
            {
                HideWindows();
                mSpellsWindow.Show();
            }
        }

        public void CloseAllWindows()
        {
            mCharacterWindow.Hide();

            mFriendsWindow.Hide();

            mInventoryWindow.Hide();

            mQuestsWindow.Hide();

            mSpellsWindow.Hide();

            mPartyWindow.Hide();

            mGuildWindow.Hide();

            _CloseAllWindows();
        }

        public bool HasWindowsOpen()
        {
            var windowsOpen = false;

            if (mCharacterWindow.IsVisible())
            {
                windowsOpen = true;
            }

            if (mFriendsWindow.IsVisible())
            {
                windowsOpen = true;
            }

            if (mInventoryWindow.IsVisible())
            {
                windowsOpen = true;
            }

            if (mQuestsWindow.IsVisible())
            {
                windowsOpen = true;
            }

            if (mSpellsWindow.IsVisible())
            {
                windowsOpen = true;
            }

            if (mPartyWindow.IsVisible())
            {
                windowsOpen = true;
            }

            if (mGuildWindow.IsVisible())
            {
                windowsOpen = true;
            }

            windowsOpen = windowsOpen || _HasWindowsOpen();

            return windowsOpen;
        }

        //Input Handlers
        private static void MenuButtonClicked(Base sender, ClickedEventArgs arguments)
        {
            Interface.GameUi?.EscapeMenu?.ToggleHidden();
        }

        private void PartyBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            TogglePartyWindow();
        }

        private void FriendsBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ToggleFriendsWindow();
        }

        private void GuildBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (!string.IsNullOrEmpty(Globals.Me.Guild))
            {
                ToggleGuildWindow();
            }
            else
            {
                ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Guilds.NotInGuild, CustomColors.Alerts.Error, ChatMessageType.Guild));
                var dialog = new Dialog();
                dialog.Prompt = Strings.Guilds.NotInGuildInstructions;

                Globals.EventDialogs.Add(dialog);
            }
        }

        private void QuestBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ToggleQuestsWindow();
        }

        private void InventoryButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ToggleInventoryWindow();
        }

        private void SpellsButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ToggleSpellsWindow();
        }

        private void CharacterButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ToggleCharacterWindow();
        }
    }

    public partial class Menu
    {
        private ImagePanel mMapBackground;
        private Button mMapButton;
        private void _Menu()
        {
            mMapBackground = new ImagePanel(mMenuContainer, "MapContainer");
            mMapButton = new Button(mMapBackground, "MapButton");
            mMapButton.SetToolTipText(Strings.GameMenu.Map);
            mMapButton.Clicked += MapButton_Clicked;
        }

        private void _CloseAllWindows()
        {
            Interface.GameUi.Map.Close();
        }

        public bool _HasWindowsOpen()
        {
            if (Interface.GameUi.Map.IsOpen)
            {
                return true;
            }

            return false;
        }

        private void MapButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Interface.GameUi.Map.ToggleOpen();
        }
    }
}
