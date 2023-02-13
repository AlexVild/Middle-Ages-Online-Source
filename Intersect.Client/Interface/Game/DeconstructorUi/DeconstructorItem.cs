using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General.Deconstructor;

namespace Intersect.Client.Interface.Game.DeconstructorUi
{
    public class DeconstructorItem : ItemContainer
    {
        public Deconstructor Deconstructor;

        public override string Filename => "DeconstructorItem";

        public override string ContentName => "DeconstructorIcon";

        public int InventoryIdx { get; set; }

        public DeconstructorItem(int index, Base container, Deconstructor deconstructor, int invIdx) : base(index, container)
        {
            InventoryIdx = invIdx;
            Deconstructor = deconstructor;
        }

        protected override void pnl_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            Deconstructor.TryRemoveItem(InventoryIdx);
        }
    }
}
