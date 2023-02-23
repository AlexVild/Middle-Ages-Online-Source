using Intersect.Client.Entities;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Label = Intersect.Client.Framework.Gwen.Control.Label;

namespace Intersect.Client.Interface.Game.Character
{
    public abstract class CharacterWindowPanel
    {
        protected CharacterWindow mParent { get; set; }

        protected Player Me => Globals.Me;

        protected ImagePanel mParentContainer { get; set; }
        protected ImagePanel mBackground { get; set; }
        public CharacterPanelType Type { get; }

        public bool IsHidden => mBackground.IsHidden;

        public abstract void Update();

        public virtual void Hide()
        {
            mBackground.Hide();
        }

        public virtual void Show()
        {
            mBackground.Show();
        }

        protected Label GenerateLabel(string name, string initText = "")
        {
            return new Label(mBackground, name)
            {
                Text = string.IsNullOrEmpty(initText) ? string.Empty : initText
            };
        }
    }
}
