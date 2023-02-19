using Intersect.Client.General.Enhancement;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Entities
{
    public partial class Player : Entity
    {
        public Enhancement Enhancement { get; set; } = new Enhancement();

        public List<Guid> KnownEnhancements { get; set; } = new List<Guid>();

        public void SetKnownEnhancements(Guid[] knownEnhancements)
        {
            knownEnhancements.OrderBy(id => EnhancementDescriptor.GetName(id));
            KnownEnhancements.Clear();
            KnownEnhancements.AddRange(knownEnhancements);
        }
    }
}
