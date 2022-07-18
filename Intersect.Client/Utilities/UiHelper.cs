using Intersect.Client.Core;
using Intersect.Client.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Utilities
{
    public static class UiHelper
    {
        public static string GetMapName(string mapName, GameFont font, int maxNameWidth)
        {
            mapName = mapName.Trim().ToUpper();

            var nameWidth = Graphics.Renderer.MeasureText(mapName, font, 1).X;
            // because we want to fit the name in between the bolts of the map texture
            if (nameWidth >= maxNameWidth)
            {
                var sb = new StringBuilder();
                if (mapName.Contains("-"))
                {
                    var split = mapName.Split('-');
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
                    if (tmpWidth >= maxNameWidth && sb.ToString().Length >= 25)
                    {
                        mapName = $"{sb.ToString().Substring(0, 25)}...";
                    }
                    else
                    {
                        mapName = sb.ToString();
                    }
                }
                else
                {
                    sb.Append($"{mapName.Substring(0, 37)}...");
                    mapName = sb.ToString();
                }
            }

            return mapName;
        }
    }
}
