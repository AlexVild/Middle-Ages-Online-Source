using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;

namespace Intersect.Client.Interface.Game
{
    public abstract class GamePanel
    {
        public bool IsHidden => Background.IsHidden;

        protected Base GameCanvas;
        public ImagePanel Background { get; set; }

        protected abstract string FileName { get; }

        public GamePanel(Base gameCanvas)
        {
            GameCanvas = gameCanvas;
            Background = new ImagePanel(GameCanvas, FileName);

            PreInitialization();
            Background.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            PostInitialization();
            Background.Hide();
        }

        protected abstract void PreInitialization();

        protected abstract void PostInitialization();

        public virtual void Show()
        {
            Background.Show();
        }

        public virtual void Hide()
        {
            Background.Hide();
        }

        public void Update()
        {
            if (Background.IsHidden)
            {
                UpdateHidden();
            }
            else
            {
                UpdateShown();
            }
        }

        public virtual void UpdateHidden()
        {
            return;
        }

        public abstract void UpdateShown();

        public bool IsVisible()
        {
            return !Background.IsHidden;
        }
    }
}
