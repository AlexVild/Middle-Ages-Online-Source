using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.GameObjects
{
    public enum LabelPosition
    {
        Header = 0,
        Footer
    }

    public class LabelDescriptor : DatabaseObject<LabelDescriptor>, IFolderable
    {
        public LabelDescriptor() : this(default)
        {
        }

        [JsonConstructor]
        public LabelDescriptor(Guid id) : base(id)
        {
            Name = "New Label";
            Position = LabelPosition.Header;
            MatchNameColor = true;
        }

        public string DisplayName { get; set; }

        public string Hint { get; set; }

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public LabelPosition Position { get; set; }

        public bool MatchNameColor { get; set; }

        /// <summary>
        /// The database compatible version of <see cref="Color"/>
        /// </summary>
        [Column("Color")]
        [JsonIgnore]
        public string JsonColor
        {
            get => JsonConvert.SerializeObject(Color);
            set => Color = !string.IsNullOrWhiteSpace(value) ? JsonConvert.DeserializeObject<Color>(value) : Color.White;
        }

        /// <summary>
        /// Defines the ARGB color settings for this Npc.
        /// </summary>
        [NotMapped]
        public Color Color { get; set; } = new Color(255, 255, 255, 255);

        public bool ShowOnlyUnlocked { get; set; } = false;
    }
}
