using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using System.Linq;

namespace Intersect.Client.Interface.Game
{

    public class QuestOfferWindow
    {

        private Button mAcceptButton;

        private Button mDeclineButton;

        private Button mNextQuest;

        private Button mPreviousQuest;

        private string mQuestOfferText = "";

        //Controls
        private WindowControl mQuestOfferWindow;

        private ScrollControl mQuestPromptArea;

        private RichLabel mQuestPromptLabel;

        private Label mQuestPromptTemplate;

        private Label mQuestTitle;

        private Label mQuestOfferAmount;
        
        private Label mCompletedLabel;

        public QuestOfferWindow(Canvas gameCanvas)
        {
            mQuestOfferWindow = new WindowControl(gameCanvas, Strings.QuestOffer.title, false, "QuestOfferWindow");
            mQuestOfferWindow.DisableResizing();
            mQuestOfferWindow.IsClosable = false;

            //Menu Header
            mQuestTitle = new Label(mQuestOfferWindow, "QuestTitle");
            mCompletedLabel = new Label(mQuestOfferWindow, "CompletedLabel");
            mCompletedLabel.SetText(Strings.QuestOffer.Completed);
            mQuestPromptArea = new ScrollControl(mQuestOfferWindow, "QuestOfferArea");
            mQuestPromptTemplate = new Label(mQuestPromptArea, "QuestOfferTemplate");
            mQuestPromptLabel = new RichLabel(mQuestPromptArea);
            mQuestOfferAmount = new Label(mQuestOfferWindow, "QuestOfferAmount");

            //Accept Button
            mAcceptButton = new Button(mQuestOfferWindow, "AcceptButton");
            mAcceptButton.SetText(Strings.QuestOffer.accept);
            mAcceptButton.Clicked += _acceptButton_Clicked;

            //Decline Button
            mDeclineButton = new Button(mQuestOfferWindow, "DeclineButton");
            mDeclineButton.SetText(Strings.QuestOffer.decline);
            mDeclineButton.Clicked += _declineButton_Clicked;

            mNextQuest = new Button(mQuestOfferWindow, "NextQuestButton");
            mNextQuest.UserData = 1;
            mNextQuest.Clicked += _changeQuest_Clicked;
            mPreviousQuest = new Button(mQuestOfferWindow, "PreviousQuestButton");
            mPreviousQuest.UserData = -1;
            mPreviousQuest.Clicked += _changeQuest_Clicked;

            mQuestOfferWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            Interface.InputBlockingElements.Add(mQuestOfferWindow);

            mNextQuest.Hide();
            mPreviousQuest.Hide();
            if (Globals.QuestOffers.Count > 0)
            {
                mCompletedLabel.IsHidden = !Globals.Me.QuestProgress[Globals.QuestOffers[0]].Completed;
            }
        }

        /// <summary>
        /// Used to abbreviate titles of "tasks", i.e "Rogue Task: Some shit"
        /// </summary>
        /// <returns></returns>
        private static string ChopTitle(string title)
        {
            var split = title.Split(':');
            if (split.Length <= 1)
            {
                return title;
            }

            if (!split[0].ToLower().Contains("task"))
            {
                return title;
            }

            return string.Concat(split.Skip(1)).Trim();
        }

        private void _declineButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.QuestOffers.Count > 0)
            {
                // TODO this needs revamped a bit to handle quest baords
                PacketSender.SendDeclineQuest(Globals.QuestOffers[Globals.QuestOfferIndex], InQuestBoard());
                RemoveQuestOffers();
            }
        }

        private void _acceptButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.QuestOffers.Count > 0)
            {
                PacketSender.SendAcceptQuest(Globals.QuestOffers[Globals.QuestOfferIndex], InQuestBoard());
                RemoveQuestOffers();
            }
        }

        public void _changeQuest_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var button = (Button)sender;
            var dir = (int)button.UserData;

            var nextIndex = Globals.QuestOfferIndex + dir;
            if (nextIndex < 0)
            {
                nextIndex = Globals.QuestOffers.Count - 1; // Wrap to back of list
            }
            else if (nextIndex >= Globals.QuestOffers.Count)
            {
                nextIndex = 0; // Wrap to front
            }

            Globals.QuestOfferIndex = nextIndex;
        }

        private void RemoveQuestOffers()
        {
            if (Globals.QuestBoard != null)
            {
                Globals.QuestOffers.Clear(); // if we're in a board, we want to get rid of ALL offers.
            }
            else
            {
                Globals.QuestOffers.RemoveAt(0); // Otherwise, we only ever display the head of the offers list - so remove that one.
            }
        }

        public void Update(QuestBase quest)
        {
            if (quest == null)
            {
                Hide();
            }
            else
            {
                Show();
                if (InQuestBoard())
                {
                    if (Globals.QuestOffers.Count > 1)
                    {
                        mNextQuest.Show();
                        mPreviousQuest.Show();
                        mQuestOfferAmount.Show();
                        mQuestOfferAmount.SetText(Strings.QuestOffer.questamount.ToString(Globals.QuestOfferIndex + 1, Globals.QuestOffers.Count));
                    } else
                    {
                        mNextQuest.Hide();
                        mPreviousQuest.Hide();
                        mQuestOfferAmount.Hide();
                    }
                    mDeclineButton.SetText(Strings.QuestOffer.backtoboard);
                } else
                {
                    mNextQuest.Hide();
                    mPreviousQuest.Hide();
                    mQuestOfferAmount.Hide();
                    mDeclineButton.SetText(Strings.QuestOffer.decline);
                }

                mQuestTitle.Text = ChopTitle(quest.Name);
                mCompletedLabel.IsHidden = !Globals.Me.QuestProgress[quest.Id].Completed;
                if (mQuestOfferText != quest.StartDescription)
                {
                    mQuestPromptLabel.ClearText();
                    mQuestPromptLabel.Width = mQuestPromptArea.Width - mQuestPromptArea.GetVerticalScrollBar().Width;
                    mQuestPromptLabel.AddText(
                        quest.StartDescription, mQuestPromptTemplate.TextColor,
                        mQuestPromptTemplate.CurAlignments.Count > 0
                            ? mQuestPromptTemplate.CurAlignments[0]
                            : Alignments.Left, mQuestPromptTemplate.Font
                    );

                    mQuestPromptLabel.SizeToChildren(false, true);
                    mQuestOfferText = quest.StartDescription;
                }
            }
        }

        public void Show()
        {
            mQuestOfferWindow.IsHidden = false;
        }

        public void Close()
        {
            mQuestOfferWindow.Close();
        }

        public bool IsVisible()
        {
            return !mQuestOfferWindow.IsHidden;
        }

        public void Hide()
        {
            mQuestOfferWindow.IsHidden = true;
        }

        private bool InQuestBoard()
        {
            return (Globals.QuestBoard != null);
        }
    }

}
