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
            mQuestBoardWindow = new WindowControl(gameCanvas, Strings.QuestBoard.title, false, "QuestBoardWindow");

            mQuestBoardWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            Interface.InputBlockingElements.Add(mQuestBoardWindow);
        }

        public void Update(QuestBoardBase questBoard)
        {
            mQuestBoardWindow.DisableResizing();

            mQuestBoard = questBoard;

            ClearQuestLists();
            PopulateQuestlists();
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

        public void PopulateQuestlists()
        {
            mQuestLists.Clear();
            int yPadding = 16;
            for (var i = 0; i < mQuestBoard.QuestLists.Count; i++)
            {
                var questListId = mQuestBoard.QuestLists[i];
                mQuestLists.Add(QuestListBase.Get(questListId));
                mQuestListButtons.Add(new Button(mQuestBoardWindow, $"QuestListButton_{i}"));
                

                if (!Globals.QuestBoardRequirements[questListId])
                {
                    mQuestListButtons[i].Disable();
                }
                
                if (i > 0)
                {
                    mQuestListButtons[i].Margin = new Margin(yPadding, 0, 0, 0);
                }

                mQuestListButtons[i].Text = mQuestLists[i].Name;

                yPadding += 16;
            }
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
    }
}
