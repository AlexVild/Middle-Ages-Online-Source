using Intersect.Models;
using Newtonsoft.Json;
using System;

namespace Intersect.GameObjects
{
    public class TerritoryDescriptor : DatabaseObject<TerritoryDescriptor>, IFolderable
    {
        public TerritoryDescriptor() : this(default)
        {
        }

        [JsonConstructor]
        public TerritoryDescriptor(Guid id) : base(id)
        {
            Name = "New Territory";
        }

        public string DisplayName { get; set; } = "";

        public string Folder { get; set; } = "";

        public long CaptureMs { get; set; }
        
        public int PointsPerTick { get; set; }
        
        public int PointsPerCapture { get; set; }
        
        public int PointsPerDefend { get; set; }
        
        public int PointsPerAttack { get; set; }

        public string Icon { get; set; }
    }
}
