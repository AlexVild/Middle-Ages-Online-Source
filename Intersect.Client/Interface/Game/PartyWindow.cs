using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game
{

    public partial class PartyWindow
    {
        private ScrollControl mMemberContainer;
        private Button mLeaveButton;
        private Button mInviteButton;

        //Controls
        private WindowControl mPartyWindow;

        //Init
        public PartyWindow(Canvas gameCanvas)
        {
            mPartyWindow = new WindowControl(gameCanvas, Strings.Parties.title, false, "PartyWindow");
            mPartyWindow.DisableResizing();

            _PartyWindow(gameCanvas);

            mLeaveButton = new Button(mPartyWindow, "LeavePartyButton");
            mLeaveButton.Text = Strings.Parties.leave;
            mLeaveButton.Clicked += leave_Clicked;

            mInviteButton = new Button(mPartyWindow, "InvitePartyButton");
            mInviteButton.Text = Strings.Parties.Invite;
            mInviteButton.Clicked += addPopupButton_Clicked;
            mInviteButton.Show();

            mPartyWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            _InitPartyMembers();
        }

        //Methods
        public void Update()
        {
            if (mPartyWindow.IsHidden)
            {
                return;
            }

            mLeaveButton.IsHidden = !Globals.Me.IsInParty();
            mInviteButton.IsDisabled = Globals.Me.IsInParty() && Globals.Me.Party.Count >= Options.Instance.PartyOpts.MaximumMembers;

            for (var i = 0; i < Options.Instance.PartyOpts.MaximumMembers; i++)
            {
                if (i < Globals.Me.Party.Count)
                {
                    var xPadding = Members[i].Container.Margin.Left + Members[i].Container.Margin.Right;
                    var yPadding = Members[i].Container.Margin.Top + Members[i].Container.Margin.Bottom;
                    // Draw things at different heights depending on the existence of the kick button
                    Members[i].Container.Height = 50;
                    if (i > 0 && Members[i].KickButton.IsVisible)
                    {
                        Members[i].Container.Height = 92;
                    }

                    var y = 0;
                    if (i != 0)
                    {
                        y = Members[i - 1].Container.Bottom;
                    }

                    Members[i]
                        .Container.SetPosition(
                            i %
                            (mMemberContainer.Width / (Members[i].Container.Width + xPadding)) *
                            (Members[i].Container.Width + xPadding) +
                            xPadding,
                            y + yPadding
                        );
                        
                    Members[i].Show();
                }
                else if (!Members[i].Container.IsHidden && i >= Globals.Me.Party.Count)
                {
                    Members[i].Hide();
                    Members[i].Container.SetPosition(0, 0);
                }
                Members[i].Update();
            }
        }

        public void Show()
        {
            mPartyWindow.IsHidden = false;
        }

        public bool IsVisible()
        {
            return !mPartyWindow.IsHidden;
        }

        public void Hide()
        {
            mPartyWindow.IsHidden = true;
        }

        void leave_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.Me.Party.Count > 0)
            {
                PacketSender.SendPartyLeave();
            }
        }

    }

    public partial class PartyWindow
    {
        public List<PartyWindowMember> Members = new List<PartyWindowMember>();

        private void _PartyWindow(Canvas gameCanvas)
        {
            mMemberContainer = new ScrollControl(mPartyWindow, "PartyMemberContainer");
            mMemberContainer.EnableScroll(false, true);
        }

        private void _InitPartyMembers()
        {
            for (var i = 0; i < Options.Instance.PartyOpts.MaximumMembers; i++)
            {
                Members.Add(new PartyWindowMember(this, i));
                Members[i].Container = new ImagePanel(mMemberContainer, "PartyMember");
                Members[i].Setup();
                if (i > Globals.Me.Party.Count - 1)
                {
                    Members[i].Hide();
                }
                Members[i].Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            }
        }

        void addPopupButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var iBox = new InputBox(
                Strings.Parties.InviteTitle, Strings.Parties.InvitePopupPrompt, true, InputBox.InputType.TextInput,
                AddMember, null, 0
            );
        }

        private void AddMember(Object sender, EventArgs e)
        {
            var ibox = (InputBox)sender;
            if (ibox.TextValue.Trim().Length >= 3) //Don't bother sending a packet less than the char limit
            {
                PacketSender.SendPartyInviteName(ibox.TextValue);
            }
        }
    }
}
