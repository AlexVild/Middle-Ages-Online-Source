using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.GameObjects
{
    public class LabelDescriptor : DatabaseObject<LabelDescriptor>, IFolderable
    {
        [JsonConstructor]
        public LabelDescriptor(Guid id) : base(id)
        {
            Name = "New Label";
        }

        // EF
        public LabelDescriptor()
        {
            Name = "New Label";
        }

        public string DisplayName { get; set; }

        public string Hint { get; set; }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
