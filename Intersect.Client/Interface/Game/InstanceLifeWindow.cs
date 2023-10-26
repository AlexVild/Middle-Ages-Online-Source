using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game
{
    public class InstanceLifeWindow : Base
    {
        //Controls
        private Canvas mGameCanvas;

        private ImagePanel mWindow;
        
        private ImagePanel mLife1;
        
        private ImagePanel mLife2;
        
        private ImagePanel mLife3;

        private Label mLabel;

        /// <summary>
        /// Indicates whether the control is hidden.
        /// </summary>
        public bool IsHidden
        {
            get { return mWindow.IsHidden; }
            set { mWindow.IsHidden = value; }
        }

        public InstanceLifeWindow(Canvas gameCanvas)
        {
            mGameCanvas = gameCanvas;
            mWindow = new ImagePanel(gameCanvas, "InstanceLifeWindow");
            mLife1 = new ImagePanel(mWindow, "Life1");
            mLife2 = new ImagePanel(mWindow, "Life2");
            mLife3 = new ImagePanel(mWindow, "Life3");
            mLabel = new Label(mWindow, "LivesLabel");
            mLabel.Text = Strings.LifeCounterWindow.Label;

            mWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        /// <summary>
        /// Update this control..
        /// </summary>
        public void Update()
        {
            if (Globals.Me == null || (!Globals.Me.InDungeon && mWindow.IsVisible))
            {
                mWindow.Hide();
            }
            else if (Globals.Me.InDungeon && mWindow.IsHidden)
            {
                mWindow.Show();
            }
            // Only update when we're visible to the user.
            if (!mWindow.IsHidden && Globals.Me != null)
            {
                mLife1.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "life_depleted.png");
                mLife2.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "life_depleted.png");
                mLife3.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "life_depleted.png");

                // The 254 shenanigans is cause I used a Byte and a negative value overflows lmao whatever
                if (Globals.Me.DungeonLives >= 0 && Globals.Me.DungeonLives < 254)
                {
                    mLife1.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "life.png");
                }
                if (Globals.Me.DungeonLives >= 1 && Globals.Me.DungeonLives < 254)
                {
                    mLife2.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "life.png");
                }
                if (Globals.Me.DungeonLives >= 2 && Globals.Me.DungeonLives < 254)
                {
                    mLife3.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "life.png");
                }
            }
        }

        /// <summary>
        /// Hides the control.
        /// </summary>
        public void Hide()
        {
            mWindow.Hide();
        }

        /// <summary>
        /// Shows the control.
        /// </summary>
        public void Show()
        {
            mWindow.Show();
        }
    }
}
