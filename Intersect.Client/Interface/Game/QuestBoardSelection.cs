using System;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.GameObjects.QuestList;
using Intersect.Client.Core;

namespace Intersect.Client.Interface.Game
{
    class QuestBoardSelection
    {
        public Button Button;

        private WindowControl Container;

        private QuestListBase mQuestList;

        private int mIndex;

        public QuestBoardSelection(WindowControl container, int index)
        {
            Container = container;
            mIndex = index;
        }

        public void Setup(QuestListBase questList)
        {
            mQuestList = questList;

            Button = new Button(Container, $"QuestBoardSelection");
            Button.Text = mQuestList.Name;
        }
    }
}

