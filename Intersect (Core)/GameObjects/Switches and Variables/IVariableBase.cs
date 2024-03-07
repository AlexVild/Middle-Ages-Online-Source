using Intersect.Enums;
using Intersect.Models;

namespace Intersect.GameObjects.Switches_and_Variables
{
    public interface IVariableBase : IDatabaseObject, IFolderable
    {
        VariableDataTypes Type { get; set; }

        string TextId { get; set; }
    }
}
