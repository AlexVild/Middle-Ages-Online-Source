using Intersect.Client.General.Enhancement;

namespace Intersect.Client.Entities
{
    public partial class Player : Entity
    {
        public Enhancement Enhancement { get; set; } = new Enhancement();
    }
}
