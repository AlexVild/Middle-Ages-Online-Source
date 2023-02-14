using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General.Deconstructor;

namespace Intersect.Client.Interface.Game.DeconstructorUi
{
    public class FuelItem : ItemContainer
    {
        public Deconstructor Deconstructor;

        public override string Filename => "FuelItem";

        public override string ContentName => "FuelIcon";

        public int InventoryIdx { get; set; }

        public FuelItem(int index, Base container, Deconstructor deconstructor, int invIdx, int hoverPanelX, int hoverPanelY) : base(index, container, hoverPanelX, hoverPanelY)
        {
            InventoryIdx = invIdx;
            Deconstructor = deconstructor;
        }

        protected override void pnl_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            Deconstructor.TryRemoveFuel(InventoryIdx);
        }
    }
}
