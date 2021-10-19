using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game
{
    class QuestBoardWindow
    {
        //Controls
        private WindowControl mQuestBoardWindow;

        public QuestBoardWindow(Canvas gameCanvas)
        {
            mQuestBoardWindow = new WindowControl(gameCanvas, Strings.QuestOffer.title, false, "QuestOfferWindow");

            mQuestBoardWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            Interface.InputBlockingElements.Add(mQuestBoardWindow);
        }

        public void Update()
        {

        }

        public bool IsVisible()
        {
            return mQuestBoardWindow.IsVisible;
        }

        public void Close()
        {
            mQuestBoardWindow.Close();
        }
    }
}
