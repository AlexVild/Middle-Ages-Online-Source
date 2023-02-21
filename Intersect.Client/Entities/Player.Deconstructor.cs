using Intersect.Client.General.Deconstructor;

namespace Intersect.Client.Entities
{
    public partial class Player : Entity
    {
        public Deconstructor Deconstructor { get; set; } = new Deconstructor();

        public bool RefreshInventoryItems { get; set; }

        public int Fuel { get; set; }
    }
}
