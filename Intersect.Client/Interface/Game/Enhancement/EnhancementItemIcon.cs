using Intersect.Client.Framework.Gwen.Control;

namespace Intersect.Client.Interface.Game.Enhancement
{
    public class EnhancementItemIcon : ItemContainer
    {
        public EnhancementItemIcon(int index, Base container, int hoverPanelX, int hoverPanelY) : base(index, container, hoverPanelX, hoverPanelY)
        {
        }

        public override string Filename => "EnhancementItem";

        public override string ContentName => "EnhancementIcon";
    }
}
