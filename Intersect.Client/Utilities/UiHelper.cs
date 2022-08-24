using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects.Timers;
using System;
using System.Linq;
using System.Text;

namespace Intersect.Client.Utilities
{
    public static class UiHelper
    {
        public static string TruncateString(string str, GameFont font, int maxNameWidth)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = str.Trim();

            var nameWidth = Graphics.Renderer.MeasureText(str, font, 1).X;
            // because we want to fit the name in between the bolts of the map texture
            if (nameWidth >= maxNameWidth)
            {
                var sb = new StringBuilder();
                if (str.Contains("-"))
                {
                    var split = str.Split('-');
                    if (split.Length > 0)
                    {
                        var prefix = split[0];
                        var prefixWords = prefix.Split(' ');
                        if (prefixWords.Length > 0)
                        {
                            sb.Append($"{prefixWords[0][0]}. {string.Concat(prefixWords.Skip(1)).Trim()} - {string.Concat(split.Skip(1)).Trim()}");
                        }
                    }

                    var tmpWidth = Graphics.Renderer.MeasureText(sb.ToString(), font, 1).X;
                    if (tmpWidth >= maxNameWidth)
                    {
                        int maxChars = Graphics.Renderer.GetMaxCharsWithinWidth(str, font, 1, maxNameWidth);
                        // -3 cause we're adding 3 chars with the ellipses
                        str = $"{sb.ToString().Substring(0, maxChars - 3)}...";
                    }
                    else
                    {
                        str = sb.ToString();
                    }
                }
                else
                {
                    int maxChars = Graphics.Renderer.GetMaxCharsWithinWidth(str, font, 1, maxNameWidth);
                    // -3 cause we're adding 3 chars with the ellipses
                    sb.Append($"{str.Substring(0, maxChars - 3)}...");
                    str = sb.ToString();
                }
            }

            return str;
        }

        public static Pointf GetViewMousePos()
        {
            var mousePos = Globals.InputManager.GetMousePosition();
            mousePos.X += Graphics.CurrentView.X;
            mousePos.Y += Graphics.CurrentView.Y;

            return mousePos;
        }
    }
}
