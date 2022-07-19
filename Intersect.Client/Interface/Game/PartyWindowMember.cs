using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using System;

namespace Intersect.Client.Interface.Game
{
    public class PartyWindowMember
    {
        public ImagePanel Container;
        private PartyWindow mPartyWindow;
        private int mMemberIdx;

        private Label mNameLabel;

        private ImagePanel mHpBar;
        private ImagePanel mHpBarContainer;
        private Label mHpLabel;
        private Label mHpValue;

        private ImagePanel mMpBar;
        private ImagePanel mMpBarContainer;
        private Label mMpLabel;
        private Label mMpValue;

        public Button KickButton;
        private Label mLeaderText;

        public PartyWindowMember(PartyWindow partyWindow, int index)
        {
            mPartyWindow = partyWindow;
            mMemberIdx = index;
        }

        /// <summary>
        /// To be called AFTER we have given the member its <see cref="Container"/>
        /// </summary>
        public void Setup()
        {
            mLeaderText = new Label(Container, "LeaderText");
            mLeaderText.Text = Strings.Parties.leader;
            mLeaderText.Hide();

            //Labels
            mNameLabel = new Label(Container, "MemberName");

            //Health bars
            mHpBarContainer = new ImagePanel(Container, "HealthBarContainer");
            mHpLabel = new Label(Container, "HealthLabel");
            mHpLabel.Text = Strings.Parties.vital0;
            mHpValue = new Label(Container, "HealthValue");
            mHpBar = new ImagePanel(mHpBarContainer, "HealthBar");

            //Mana bars
            mMpBarContainer = new ImagePanel(Container, "ManaBarContainer");
            mMpLabel = new Label(Container, "ManaLabel");
            mMpLabel.Text = Strings.Parties.vital1;
            mMpValue = new Label(Container, "ManaValue");
            mMpBar = new ImagePanel(mMpBarContainer, "ManaBar");

            KickButton = new Button(Container, "KickButton");
            KickButton.Clicked += kick_Clicked;
            KickButton.Text = Strings.Parties.kicklbl;

            if (Globals.Me.Party.Count > mMemberIdx)
            {
                //Only show the kick buttons if its you or you are the party leader
                if (Globals.Me.Party[0].Id == Globals.Me.Id)
                {
                    KickButton.Show();
                }
                var memberName = UiHelper.TruncateString(Globals.Me.Party[mMemberIdx].Name, mNameLabel.Font, 120);

                mNameLabel.Text = Strings.Parties.name.ToString(
                    memberName, Globals.Me.Party[mMemberIdx].Level
                );
                KickButton
                    .SetToolTipText(
                        Strings.Parties.kick.ToString(Globals.Entities[Globals.Me.Party[mMemberIdx].Id].Name)
                    );
            }
        }

        public void Update()
        {
            if (Container.IsHidden)
            {
                return;
            }

            mNameLabel.Text = Strings.Parties.name.ToString(
                Globals.Me.Party[mMemberIdx].Name, Globals.Me.Party[mMemberIdx].Level
            );

            if (mMemberIdx == 0)
            {
                mLeaderText.Show();
            }

            if (mHpBar.Texture != null)
            {
                var partyHpWidthRatio = 1f;
                if (Globals.Me.Party[mMemberIdx].MaxVital[(int)Vitals.Health] > 0)
                {
                    var vitalHp = Globals.Me.Party[mMemberIdx].Vital[(int)Vitals.Health];
                    var vitalMaxHp = Globals.Me.Party[mMemberIdx].MaxVital[(int)Vitals.Health];
                    var ratioHp = (float)vitalHp / (float)vitalMaxHp;
                    partyHpWidthRatio = Math.Min(1, Math.Max(0, ratioHp));
                }

                mHpBar.SetTextureRect(0, 0, mHpBar.Texture.GetWidth(), mHpBar.Texture.GetHeight());

                var width = Intersect.Utilities.MathHelper.RoundNearestMultiple((int) (partyHpWidthRatio * mHpBar.Width), 4);
                mHpBar.SetSize(width, mHpBar.Height);
            }

            mHpValue.Text = Strings.Parties.vital0val.ToString(
                Globals.Me.Party[mMemberIdx].Vital[(int)Vitals.Health],
                Globals.Me.Party[mMemberIdx].MaxVital[(int)Vitals.Health]
            );

            if (mMpBar.Texture != null)
            {
                var partyMpWidthRatio = 1f;
                if (Globals.Me.Party[mMemberIdx].MaxVital[(int)Vitals.Mana] > 0)
                {
                    var vitalMp = Globals.Me.Party[mMemberIdx].Vital[(int)Vitals.Mana];
                    var vitalMaxMp = Globals.Me.Party[mMemberIdx].MaxVital[(int)Vitals.Mana];
                    var ratioMp = (float)vitalMp / (float)vitalMaxMp;
                    partyMpWidthRatio = Math.Min(1, Math.Max(0, ratioMp));
                }

                mMpBar.SetTextureRect(0, 0, mMpBar.Texture.GetWidth(),mMpBar.Texture.GetHeight());

                var width = Intersect.Utilities.MathHelper.RoundNearestMultiple((int)(partyMpWidthRatio * mMpBar.Width), 4);
                mMpBar.SetSize(width, mMpBar.Height);
            }

            mMpValue.Text = Strings.Parties.vital1val.ToString(
                Globals.Me.Party[mMemberIdx].Vital[(int)Vitals.Mana],
                Globals.Me.Party[mMemberIdx].MaxVital[(int)Vitals.Mana]
            );

            //Only show the kick buttons if its you or you are the party leader
            if (Globals.Me.Party[0].Id == Globals.Me.Id && mMemberIdx > 0)
            {
                KickButton.Show();
                KickButton.SetToolTipText(Strings.Parties.kick.ToString(Globals.Me.Party[mMemberIdx].Name));
            }
            else
            {
                KickButton.Hide();
            }
        }

        public void Show()
        {
            Container.Show();
        }

        public void Hide()
        {
            Container.Hide();
        }

        void kick_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendPartyKick(Globals.Me.Party[mMemberIdx].Id);
            return;
        }
    }
}
