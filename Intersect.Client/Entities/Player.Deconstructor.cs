using Intersect.Client.General.Deconstructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Entities
{
    public partial class Player : Entity
    {
        public Deconstructor Deconstructor { get; set; } = new Deconstructor();
    }
}
