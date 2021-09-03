using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Intersect.Config
{

    public class PaperdollOptions
    {

        [JsonIgnore] public List<string>[] Directions;

        public List<string> Down = new List<string>()
        {
            "Player",
            "Eyes",
            "Shirt",
            "Extra",
            "Boots",
            "Armor",
            "Beard",
            "Hair",
            "Accessory",
            "Helmet",
            "Shield",
            "Weapon",
            "Prayer"
        };

        public List<string> Left = new List<string>()
        {
            "Weapon",
            "Player",
            "Shirt",
            "Eyes",
            "Extra",
            "Boots",
            "Helmet",
            "Accessory",
            "Armor",
            "Beard",
            "Hair",
            "Shield",
            "Prayer"
        };

        public List<string> Right = new List<string>()
        {
            "Shield",
            "Player",
            "Shirt",
            "Eyes",
            "Extra",
            "Boots",
            "Helmet",
            "Accessory",
            "Armor",
            "Beard",
            "Hair",
            "Weapon",
            "Prayer"
        };

        public List<string> Up = new List<string>()
        {
            "Weapon",
            "Shield",
            "Extra",
            "Beard",
            "Player",
            "Shirt",
            "Eyes",
            "Boots",
            "Accessory",
            "Armor",
            "Hair",
            "Helmet",
            "Prayer"
        };

        public PaperdollOptions()
        {
            Directions = new List<string>[]
            {
                Up,
                Down,
                Left,
                Right
            };
        }

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            Up.Clear();
            Down.Clear();
            Left.Clear();
            Right.Clear();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Up = new List<string>(Up.Distinct());
            Down = new List<string>(Down.Distinct());
            Left = new List<string>(Left.Distinct());
            Right = new List<string>(Right.Distinct());
            Directions = new List<string>[]
            {
                Up,
                Down,
                Left,
                Right
            };

            Validate();
        }

        public void Validate()
        {
            var equipment = new EquipmentOptions();
            var player = new PlayerOptions();

            foreach (var direction in Directions)
            {
                var hasPlayer = false;
                foreach (var item in direction)
                {
                    if (item == "Player")
                    {
                        hasPlayer = true;
                    }

                    if (item != "Player" && !equipment.Slots.Contains(item) && !player.DecorSlots.Contains(item))
                    {
                        throw new Exception($"Config Error: Paperdoll item {item} does not exist in equipment slots!");
                    }
                }

                if (!hasPlayer)
                {
                    throw new Exception($"Config Error: Paperdoll direction {direction} does not have Player listed!");
                }
            }
        }

    }

}
