using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.QuestList;
using System;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game
{
    class QuestBoardWindow
    {
        private Button mCancelButton;

        private List<Button> mQuestListButtons = new List<Button>();

        //Controls
        private WindowControl mQuestBoardWindow;

        public int X => mQuestBoardWindow.X;

        public int Y => mQuestBoardWindow.Y;

        private QuestBoardBase mQuestBoard;

        private List<QuestListBase> mQuestLists = new List<QuestListBase>();

        public QuestBoardWindow(Canvas gameCanvas)
        {
            mQuestBoardWindow = new WindowControl(gameCanvas, Globals.QuestBoard.Name, false, "QuestBoardWindow");

            mCancelButton = new Button(mQuestBoardWindow, "CancelButton");
            mCancelButton.Text = Strings.QuestBoard.cancel;

            mQuestBoardWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            Interface.InputBlockingElements.Add(mQuestBoardWindow);
            ClearQuestLists();

            mCancelButton.Clicked += Cancel_Clicked;
        } 

        public void Setup(QuestBoardBase questBoard)
        {
            mQuestBoard = questBoard;

            ClearQuestLists();
            PopulateQuestLists();
        }

        public void Update()
        {
            mQuestBoardWindow.DisableResizing();
        }

        public void ClearQuestLists()
        {
            mQuestLists.Clear();
            for (var i = 0; i < mQuestListButtons.Count; i++)
            {
                mQuestListButtons[i].Dispose();
            }
            mQuestListButtons.Clear();
        }

        public void PopulateQuestLists()
        {
            var baseX = 48;
            var baseY = 50; // Initial positions of the first button
            var upperMargin = 16; // How much space to have between each button

            for (var i = 0; i < mQuestBoard.QuestLists.Count; i++)
            {
                var questListId = mQuestBoard.QuestLists[i];
                mQuestLists.Add(QuestListBase.Get(questListId));
                // Keep button in a list so we have some reference of which to dispose of it
                mQuestListButtons.Add(SetupSelectionButton(mQuestBoardWindow, i, mQuestLists[i]));
                var button = mQuestListButtons[i];
                // Load in button styling
                button.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                
                // If we don't meet the requirements for accessing the list
                if (!Globals.QuestBoardRequirements[questListId])
                {
                    button.Disable();
                }

                // Position of button
                var dynamicY = baseY + (button.Height * i) + (upperMargin * (Math.Sign(i) * i)); // Sign() allows us not to calc margin on first iteration
                button.SetPosition(baseX, dynamicY);
            }
        }

        public Button SetupSelectionButton(WindowControl parent, int index, QuestListBase questList)
        {
            var button = new Button(mQuestBoardWindow, $"QuestBoardSelection");
            button.Text = questList.Name;
            return button;
        }

        public bool IsVisible()
        {
            return !mQuestBoardWindow.IsHidden;
        }

        public void Close()
        {
            mQuestBoardWindow.Close();
        }

        public void Hide()
        {
            mQuestBoardWindow.IsHidden = true;
        }

        private void Cancel_Clicked(Base sender, ClickedEventArgs arguments)
        { 
            Close();
        }
    }
}

