using System;
using System.Text.RegularExpressions;

using Intersect.Client.Framework.GenericClasses;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Framework.Gwen
{

    /// <summary>
    ///     Misc utility functions.
    /// </summary>
    public static class Util
    {

        public static int Round(float x)
        {
            return (int) Math.Round(x, MidpointRounding.AwayFromZero);
        }

        /*
        public static int Trunc(float x)
        {
            return (int)Math.Truncate(x);
        }
        */
        public static int Ceil(float x)
        {
            return (int) Math.Ceiling(x);
        }

        public static Rectangle FloatRect(float x, float y, float w, float h)
        {
            return new Rectangle((int) x, (int) y, (int) w, (int) h);
        }

        public static int Clamp(int x, int min, int max)
        {
            if (x < min)
            {
                return min;
            }

            if (x > max)
            {
                return max;
            }

            return x;
        }

        public static float Clamp(float x, float min, float max)
        {
            if (x < min)
            {
                return min;
            }

            if (x > max)
            {
                return max;
            }

            return x;
        }

        public static Rectangle ClampRectToRect(Rectangle inside, Rectangle outside, bool clampSize = false)
        {
            if (inside.X < outside.X)
            {
                inside.X = outside.X;
            }

            if (inside.Y < outside.Y)
            {
                inside.Y = outside.Y;
            }

            if (inside.Right > outside.Right)
            {
                if (clampSize)
                {
                    inside.Width = outside.Width;
                }
                else
                {
                    inside.X = outside.Right - inside.Width;
                }
            }

            if (inside.Bottom > outside.Bottom)
            {
                if (clampSize)
                {
                    inside.Height = outside.Height;
                }
                else
                {
                    inside.Y = outside.Bottom - inside.Height;
                }
            }

            return inside;
        }

        public static Hsv ToHsv(this Color color)
        {
            var hsv = new Hsv();
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            float delta = max - min;

            // Value (V) is the maximum RGB component scaled to [0, 1]
            hsv.V = max / 255f;

            // Saturation (S)
            hsv.S = max == 0 ? 0 : delta / (float)max;

            // Hue (H) calculation
            if (delta == 0)
            {
                hsv.H = 0; // undefined, default to 0 for grayscale colors
            }
            else
            {
                if (max == color.R)
                {
                    hsv.H = 60f * (((color.G - color.B) / delta) % 6);
                }
                else if (max == color.G)
                {
                    hsv.H = 60f * (((color.B - color.R) / delta) + 2);
                }
                else if (max == color.B)
                {
                    hsv.H = 60f * (((color.R - color.G) / delta) + 4);
                }

                if (hsv.H < 0)
                {
                    hsv.H += 360f; // Ensure hue is positive
                }
            }

            return hsv;
        }

        public static Color HsvToColor(float h, float s, float v)
        {
            var hi = Convert.ToInt32(Math.Floor(h / 60)) % 6;
            var f = h / 60 - (float) Math.Floor(h / 60);

            v = v * 255;
            var va = Convert.ToInt32(v);
            var p = Convert.ToInt32(v * (1 - s));
            var q = Convert.ToInt32(v * (1 - f * s));
            var t = Convert.ToInt32(v * (1 - (1 - f) * s));

            if (hi == 0)
            {
                return Color.FromArgb(255, va, t, p);
            }

            if (hi == 1)
            {
                return Color.FromArgb(255, q, va, p);
            }

            if (hi == 2)
            {
                return Color.FromArgb(255, p, va, t);
            }

            if (hi == 3)
            {
                return Color.FromArgb(255, p, q, va);
            }

            if (hi == 4)
            {
                return Color.FromArgb(255, t, p, va);
            }

            return Color.FromArgb(255, va, p, q);
        }

        // can't create extension operators
        public static Color Subtract(this Color color, Color other)
        {
            return Color.FromArgb(color.A - other.A, color.R - other.R, color.G - other.G, color.B - other.B);
        }

        public static Color Add(this Color color, Color other)
        {
            return Color.FromArgb(color.A + other.A, color.R + other.R, color.G + other.G, color.B + other.B);
        }

        public static Color Multiply(this Color color, float amount)
        {
            return Color.FromArgb(
                color.A, (int) (color.R * amount), (int) (color.G * amount), (int) (color.B * amount)
            );
        }

        public static Rectangle Add(this Rectangle r, Rectangle other)
        {
            return new Rectangle(r.X + other.X, r.Y + other.Y, r.Width + other.Width, r.Height + other.Height);
        }

        /// <summary>
        ///     Splits a string but keeps the separators intact (at the end of split parts).
        /// </summary>
        /// <param name="text">String to split.</param>
        /// <param name="separators">Separator characters.</param>
        /// <returns>Split strings.</returns>
        public static string[] SplitAndKeep(string text, string separators)
        {
            return Regex.Split(text, @"(?=[" + separators + "])");
        }

    }

}
