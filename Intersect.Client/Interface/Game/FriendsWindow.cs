using System;

using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game
{

    partial class FriendsWindow
    {

        private Button mAddButton;

        private Button mAddPopupButton;

        private ListBox mFriends;

        //Controls
        private WindowControl mFriendsWindow;

        private TextBox mSearchTextbox;

        //Temp variables
        private string mTempName;

        private ImagePanel mTextboxContainer;

        //Init
        public FriendsWindow(Canvas gameCanvas)
        {
            mFriendsWindow = new WindowControl(gameCanvas, Strings.Friends.title, false, "FriendsWindow");
            mFriendsWindow.DisableResizing();

            mTextboxContainer = new ImagePanel(mFriendsWindow, "SearchContainer");
            mSearchTextbox = new TextBox(mTextboxContainer, "SearchTextbox");
            Interface.FocusElements.Add(mSearchTextbox);

            mFriends = new ListBox(mFriendsWindow, "FriendsList");

            mAddButton = new Button(mFriendsWindow, "AddFriendButton");
            mAddButton.SetText("+");
            mAddButton.Clicked += addButton_Clicked;

            mAddPopupButton = new Button(mFriendsWindow, "AddFriendPopupButton");
            mAddPopupButton.IsHidden = true;
            mAddPopupButton.SetText(Strings.Friends.addfriend);
            mAddPopupButton.Clicked += addPopupButton_Clicked;

            UpdateList();

            _FriendsWindow(gameCanvas);

            mFriendsWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        //Methods
        public void Update()
        {
            if (mFriendsWindow.IsHidden)
            {
                return;
            }
        }

        public void Show()
        {
            mFriendsWindow.IsHidden = false;
        }

        public bool IsVisible()
        {
            return !mFriendsWindow.IsHidden;
        }

        public void Hide()
        {
            mFriendsWindow.IsHidden = true;
        }

        public void UpdateList()
        {
            //Clear previous instances if already existing
            if (mFriends != null)
            {
                mFriends.Clear();
            }

            foreach (var f in Globals.Me.Friends)
            {
                var mapName = f.Map;
                var mapSplit = f.Map?.Split('-');
                if ((mapSplit?.Length ?? 0) > 1)
                {
                    mapName = mapSplit[1].Trim();
                }

                var friendNameWidth = (int)Graphics.Renderer.MeasureText(f.Name, Graphics.HUDFontSmall, 1).X;
                var row = f.Online ? 
                    mFriends.AddRow($"{f.Name} ({UiHelper.TruncateString(mapName, Graphics.HUDFontSmall, 190 - friendNameWidth)})") : 
                    mFriends.AddRow($"${UiHelper.TruncateString(f.Name, Graphics.HUDFontSmall, 204)}");
                row.UserData = f;
                row.Clicked += friends_Clicked;
                row.RightClicked += friend_RightClicked;

                //Row Render color (red = offline, green = online)
                if (f.Online == true)
                {
                    row.SetTextColor(CustomColors.General.GeneralCompleted);
                }
                else
                {
                    row.SetTextColor(CustomColors.General.GeneralDisabled);
                }

                row.RenderColor = new Color(50, 255, 255, 255);
            }
        }

        void addButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mSearchTextbox.Text.Trim().Length >= 3) //Don't bother sending a packet less than the char limit
            {
                if (Globals.Me.CombatTimer < Timing.Global.Milliseconds)
                {
                    PacketSender.SendAddFriend(mSearchTextbox.Text);
                }
                else
                {
                    PacketSender.SendChatMsg(Strings.Friends.infight.ToString(), 4);
                }
            }
        }

        void addPopupButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var iBox = new InputBox(
                Strings.Friends.addfriend, Strings.Friends.addfriendprompt, true, InputBox.InputType.TextInput,
                AddFriend, null, 0
            );
        }

        void friends_Clicked(Base sender, ClickedEventArgs arguments)
        {
            //Only pm online players
            mSelectedFriend = (FriendInstance)sender.UserData;
            if (mSelectedFriend.Online == true)
            {
                Interface.GameUi.SetChatboxText("/pm " + (string)mSelectedFriend.Name + " ");
            }
        }

        void removeOption_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var row = (MenuItem) sender;

            var iBox = new InputBox(
                Strings.Friends.removefriend, Strings.Friends.removefriendprompt.ToString(mSelectedFriend.Name), true,
                InputBox.InputType.YesNo, RemoveFriend, null, 0
            );
        }

        private void RemoveFriend(Object sender, EventArgs e)
        {
            PacketSender.SendRemoveFriend(mSelectedFriend.Name);
        }

        private void AddFriend(Object sender, EventArgs e)
        {
            var ibox = (InputBox) sender;
            if (ibox.TextValue.Trim().Length >= 3) //Don't bother sending a packet less than the char limit
            {
                if (Globals.Me.CombatTimer < Timing.Global.Milliseconds)
                {
                    PacketSender.SendAddFriend(ibox.TextValue);
                }
                else
                {
                    PacketSender.SendChatMsg(Strings.Friends.infight.ToString(), 4);
                }
            }
        }

    }


    partial class FriendsWindow
    {
        //Context Menu
        private Framework.Gwen.Control.Menu mContextMenu;
        private MenuItem mPmOption;
        private MenuItem mInviteToPartyOption;
        private MenuItem mInviteToGuildOption;
        private MenuItem mRemoveOption;
        private FriendInstance mSelectedFriend;

        void _FriendsWindow(Canvas gameCanvas)
        {
            mContextMenu = new Framework.Gwen.Control.Menu(gameCanvas, "FriendContextMenu");
            mContextMenu.IsHidden = true;
            mContextMenu.IconMarginDisabled = true;

            //Add Context Menu Options
            //TODO: Is this a memory leak?
            mContextMenu.Children.Clear();

            mPmOption = mContextMenu.AddItem(Strings.Friends.Pm.ToString());
            mPmOption.Clicked += pmOption_Clicked;

            mInviteToPartyOption = mContextMenu.AddItem(Strings.Friends.PartyInvite.ToString());
            mInviteToPartyOption.Clicked += partyOption_Clicked;

            mInviteToGuildOption = mContextMenu.AddItem(Strings.Friends.GuildInvite.ToString());
            mInviteToGuildOption.Clicked += guildOption_Clicked;

            mRemoveOption = mContextMenu.AddItem(Strings.Friends.Remove.ToString());
            mRemoveOption.Clicked += removeOption_Clicked;

            mContextMenu.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void pmOption_Clicked(Base sender, ClickedEventArgs arguments)
        {
            //Only pm online players
            if (mSelectedFriend?.Online == true)
            {
                Interface.GameUi.SetChatboxText("/pm " + mSelectedFriend.Name + " ");
            }
        }

        private void partyOption_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mSelectedFriend?.Online == true)
            {
                var iBox = new InputBox(
                    Strings.Friends.PartyInviteTitle, Strings.Friends.PartyInvitePrompt.ToString(mSelectedFriend?.Name), true, InputBox.InputType.YesNo,
                    SendPartyInvite, null, mSelectedFriend
                );
            }
        }

        private void SendPartyInvite(object sender, EventArgs e)
        {
            var input = (InputBox)sender;
            var friend = (FriendInstance)input.UserData;
            PacketSender.SendPartyInviteName(friend.Name);
        }

        private void guildOption_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mSelectedFriend?.Online == true && !string.IsNullOrEmpty(Globals.Me?.Guild))
            {
                var iBox = new InputBox(
                    Strings.Friends.GuildInviteTitle, Strings.Friends.GuildInvitePrompt.ToString(mSelectedFriend?.Name, Globals.Me.Guild), true, InputBox.InputType.YesNo,
                    SendGuildInvite, null, mSelectedFriend
                );
            }
        }

        private void SendGuildInvite(object sender, EventArgs e)
        {
            var input = (InputBox)sender;
            var friend = (FriendInstance)input.UserData;
            PacketSender.SendInviteGuild(friend.Name);
        }

        private void friend_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            var row = (ListBoxRow)sender;
            var member = (FriendInstance)row.UserData;

            //Only pm online players
            if (member != null)
            {
                mSelectedFriend = member;

                //Remove and re-add children
                foreach (var child in mContextMenu.Children.ToArray())
                {
                    mContextMenu.RemoveChild(child, false);
                }

                var rankIndex = Globals.Me.Rank;
                var isOwner = rankIndex == 0;

                
                if (mSelectedFriend?.Online ?? false)
                {
                    mContextMenu.AddChild(mPmOption);
                    mContextMenu.AddChild(mInviteToPartyOption);
                    if (!string.IsNullOrEmpty(Globals.Me?.Guild))
                    {
                        mContextMenu.AddChild(mInviteToGuildOption);
                    }   
                }
                mContextMenu.AddChild(mRemoveOption);

                mContextMenu.IsHidden = false;
                mContextMenu.SetSize(mContextMenu.Width, mContextMenu.Height);
                mContextMenu.Open(Framework.Gwen.Pos.None);
                mContextMenu.MoveTo(mContextMenu.X, mContextMenu.Y);
            }
        }
    }
}
