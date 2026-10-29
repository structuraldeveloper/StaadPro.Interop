using System;
using System.Windows.Media;

namespace StaadPro.Interop.Services.Table
{
    internal static class TableColorConversion
    {
        internal static Color Parse(string colorText)
        {
            if (colorText == null) throw new ArgumentNullException(nameof(colorText));
            try { return (Color)ColorConverter.ConvertFromString(colorText); }
            catch (FormatException) { return Colors.Black; }
        }

        internal static Color FromHsl(float hue, float saturation, float lightness)
        {
            Finite(hue, nameof(hue)); Finite(saturation, nameof(saturation)); Finite(lightness, nameof(lightness));
            hue = Math.Max(0f, Math.Min(360f, hue));
            saturation = Math.Max(0f, Math.Min(1f, saturation));
            lightness = Math.Max(0f, Math.Min(1f, lightness));
            float chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            float secondary = chroma * (1 - Math.Abs((hue / 60) % 2 - 1));
            float offset = lightness - chroma / 2;
            float red = 0, green = 0, blue = 0;
            if (hue < 60) { red = chroma; green = secondary; }
            else if (hue < 120) { red = secondary; green = chroma; }
            else if (hue < 180) { green = chroma; blue = secondary; }
            else if (hue < 240) { green = secondary; blue = chroma; }
            else if (hue < 300) { red = secondary; blue = chroma; }
            else { red = chroma; blue = secondary; }
            return Color.FromRgb((byte)Math.Round((red + offset) * 255), (byte)Math.Round((green + offset) * 255), (byte)Math.Round((blue + offset) * 255));
        }

        private static void Finite(float value, string name)
        {
            if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(name, "Supply a finite HSL component; finite out-of-range values are clamped.");
        }
    }
}
