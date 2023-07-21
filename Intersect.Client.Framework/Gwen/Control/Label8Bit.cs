using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Intersect.Client.Framework.Gwen.Control
{
    public enum Label8BitColors
    {
        Default,
        Hovered,
        Clicked,
        Disabled,
    }

    public static class Label8BitSettings
    {
        public static readonly string FontName = "november";

        public static readonly Dictionary<Label8BitColors, Color> Colors = new Dictionary<Label8BitColors, Color>()
        {
            { Label8BitColors.Default, new Color(255, 50, 19, 0) },
            { Label8BitColors.Hovered, new Color(255, 111, 63, 0) },
            { Label8BitColors.Clicked, new Color(255, 0, 0, 0) },
            { Label8BitColors.Disabled, new Color(255, 60, 60, 60) },
        };

        public static readonly string TooltipBg = "tooltip.png";
        
        public static readonly int TooltipFontSize = 10;

        public static readonly Color TooltipFontColor = Color.White;
        
    }

    public class Label8Bit : Label
    {
        public int DefaultFontSize { get; set; }
        public string DefaultFontName { get; set; }
        public Color DefaultTextColor { get; set; }
        public Color DefaultDisabledTextColor { get; set; }
        public Color DefaultHoveredTextColor { get; set; }
        public Color DefaultClickedTextColor { get; set; }
        public string TooltipText { get; set; }
        public bool Interactive { get; set; }

        public Label8Bit(Base parent, int fontSize, string name = "", bool interactive = false, string tooltip = "") : base(parent, name)
        {
            DefaultFontSize = fontSize;
            DefaultFontName = Label8BitSettings.FontName;

            DefaultTextColor = Label8BitSettings.Colors[Label8BitColors.Default];
            DefaultDisabledTextColor = Label8BitSettings.Colors[Label8BitColors.Disabled];
            DefaultHoveredTextColor = Label8BitSettings.Colors[Label8BitColors.Hovered];
            DefaultClickedTextColor = Label8BitSettings.Colors[Label8BitColors.Clicked];

            Interactive = interactive;

            if (Interactive)
            {
                SetToolTipText(tooltip);
            }
        }

        public override void LoadJson(JToken obj)
        {
            base.LoadJson(obj);

            SetTooltipGraphicsMAO();
            FontSize = DefaultFontSize;
            FontName = DefaultFontName;

            TextColor = DefaultTextColor;
            mDisabledTextColor = DefaultDisabledTextColor;

            if (Interactive)
            {
                mHoverTextColor = DefaultHoveredTextColor;
                mClickedTextColor = DefaultClickedTextColor;
            }
        }

        public void PlaceBelow(Base element)
        {
            if (element == default)
            {
                return;
            }

            X = element.X;
            Y = element.Bottom + 4;
            ProcessAlignments();
        }
    }
}
