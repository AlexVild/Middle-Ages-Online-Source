using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game
{
    public abstract class GameWindow
    {
        public bool IsHidden => Background.IsHidden;

        protected Base GameCanvas;
        public WindowControl Background { get; set; }

        protected abstract string FileName { get; }
        protected abstract string Title { get; }

        public GameWindow(Base gameCanvas)
        {
            GameCanvas = gameCanvas;
            Background = new WindowControl(GameCanvas, Title, false, FileName, onClose: Close);

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

        protected virtual void Close()
        {
            Background.Hide();
        }

        public bool IsVisible()
        {
            return !Background.IsHidden;
        }

        public void ForceClose()
        {
            Close();
        }
    }
}
