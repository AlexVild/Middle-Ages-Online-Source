using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Config
{
    public static class TileLayers
    {
        public const string MAP_LAYER_GROUND = "Ground";
        public const string MAP_LAYER_MASK_1 = "Mask 1";
        public const string MAP_LAYER_MASK_2 = "Mask 2";
        public const string MAP_LAYER_FRINGE_1 = "Fringe 1";
        public const string MAP_LAYER_FRINGE_2 = "Fringe 2";

        public static List<string> Names = new List<string>
        {
            MAP_LAYER_GROUND,
            MAP_LAYER_MASK_1,
            MAP_LAYER_MASK_2,
            MAP_LAYER_FRINGE_1,
            MAP_LAYER_FRINGE_2,
        };
    }

    public class LayerOptions
    {
        public const string Attributes = nameof(Attributes);
        public const string Npcs = nameof(Npcs);
        public const string Lights = nameof(Lights);
        public const string Events = nameof(Events);

        [JsonProperty]
        public List<string> LowerLayers { get; private set; } = new List<string>() { TileLayers.MAP_LAYER_GROUND, TileLayers.MAP_LAYER_MASK_1, TileLayers.MAP_LAYER_MASK_2 };

        [JsonProperty]
        public List<string> MiddleLayers { get; private set; } = new List<string>() { TileLayers.MAP_LAYER_FRINGE_1 };

        [JsonProperty]
        public List<string> UpperLayers { get; private set; } = new List<string>() { TileLayers.MAP_LAYER_FRINGE_2 };

        [JsonIgnore]
        public List<string> All { get; private set; } = new List<string>();

        [JsonProperty]
        public bool DestroyOrphanedLayers { get; private set; } = false;

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            All.Clear();
            LowerLayers.Clear();
            MiddleLayers.Clear();
            UpperLayers.Clear();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Validate();
        }

        public void Validate()
        {
            LowerLayers = new List<string>(LowerLayers.Distinct());
            MiddleLayers = new List<string>(MiddleLayers.Distinct());
            UpperLayers = new List<string>(UpperLayers.Distinct());

            var reservedLayers = new string[] { Attributes, Npcs, Lights, Events };
            All.Clear();
            All.AddRange(LowerLayers);
            All.AddRange(MiddleLayers);
            All.AddRange(UpperLayers);

            if (All.Count() == 0)
            {
                //Must have at least 1 map layer!
                throw new Exception("Config Error: You must have at least 1 map layer configured! Please update your server config.");
            }

            foreach (var reserved in reservedLayers)
            {
                if (All.Contains(reserved))
                {
                    throw new Exception($"Config Error: Layer '{reserved}' is reserved for editor use. Please choose different naming for map layers in your server config.");
                }
            }

            if (All.Count != All.Distinct().Count())
            {
                //Duplicate layers!
                throw new Exception("Config Error: Duplicate map layers detected! Map layers must be unique in name. Please update your server config.");
            }
        }
    }
}
