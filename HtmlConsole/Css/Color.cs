﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HtmlConsole.Css
{
    public class Color
    {
        public static Color Transparent = new Color(1, 1, 1, 0);
        public static Color White = new Color(1, 1, 1, 1);
        public static Color Black = new Color(0, 0, 0, 1);

        // http://www.w3schools.com/colors/colors_names.asp
        private static readonly Dictionary<string, string> NamedColorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "AliceBlue", "#F0F8FF" },
            { "AntiqueWhite", "#FAEBD7" },
            { "Aqua", "#00FFFF" },
            { "Aquamarine", "#7FFFD4" },
            { "Azure", "#F0FFFF" },
            { "Beige", "#F5F5DC" },
            { "Bisque", "#FFE4C4" },
            { "Black", "#000000" },
            { "BlanchedAlmond", "#FFEBCD" },
            { "Blue", "#0000FF" },
            { "BlueViolet", "#8A2BE2" },
            { "Brown", "#A52A2A" },
            { "BurlyWood", "#DEB887" },
            { "CadetBlue", "#5F9EA0" },
            { "Chartreuse", "#7FFF00" },
            { "Chocolate", "#D2691E" },
            { "Coral", "#FF7F50" },
            { "CornflowerBlue", "#6495ED" },
            { "Cornsilk", "#FFF8DC" },
            { "Crimson", "#DC143C" },
            { "Cyan", "#00FFFF" },
            { "DarkBlue", "#00008B" },
            { "DarkCyan", "#008B8B" },
            { "DarkGoldenRod", "#B8860B" },
            { "DarkGray", "#A9A9A9" },
            { "DarkGrey", "#A9A9A9" },
            { "DarkGreen", "#006400" },
            { "DarkKhaki", "#BDB76B" },
            { "DarkMagenta", "#8B008B" },
            { "DarkOliveGreen", "#556B2F" },
            { "DarkOrange", "#FF8C00" },
            { "DarkOrchid", "#9932CC" },
            { "DarkRed", "#8B0000" },
            { "DarkSalmon", "#E9967A" },
            { "DarkSeaGreen", "#8FBC8F" },
            { "DarkSlateBlue", "#483D8B" },
            { "DarkSlateGray", "#2F4F4F" },
            { "DarkSlateGrey", "#2F4F4F" },
            { "DarkTurquoise", "#00CED1" },
            { "DarkViolet", "#9400D3" },
            { "DeepPink", "#FF1493" },
            { "DeepSkyBlue", "#00BFFF" },
            { "DimGray", "#696969" },
            { "DimGrey", "#696969" },
            { "DodgerBlue", "#1E90FF" },
            { "FireBrick", "#B22222" },
            { "FloralWhite", "#FFFAF0" },
            { "ForestGreen", "#228B22" },
            { "Fuchsia", "#FF00FF" },
            { "Gainsboro", "#DCDCDC" },
            { "GhostWhite", "#F8F8FF" },
            { "Gold", "#FFD700" },
            { "GoldenRod", "#DAA520" },
            { "Gray", "#808080" },
            { "Grey", "#808080" },
            { "Green", "#008000" },
            { "GreenYellow", "#ADFF2F" },
            { "HoneyDew", "#F0FFF0" },
            { "HotPink", "#FF69B4" },
            { "IndianRed ", "#CD5C5C" },
            { "Indigo ", "#4B0082" },
            { "Ivory", "#FFFFF0" },
            { "Khaki", "#F0E68C" },
            { "Lavender", "#E6E6FA" },
            { "LavenderBlush", "#FFF0F5" },
            { "LawnGreen", "#7CFC00" },
            { "LemonChiffon", "#FFFACD" },
            { "LightBlue", "#ADD8E6" },
            { "LightCoral", "#F08080" },
            { "LightCyan", "#E0FFFF" },
            { "LightGoldenRodYellow", "#FAFAD2" },
            { "LightGray", "#D3D3D3" },
            { "LightGrey", "#D3D3D3" },
            { "LightGreen", "#90EE90" },
            { "LightPink", "#FFB6C1" },
            { "LightSalmon", "#FFA07A" },
            { "LightSeaGreen", "#20B2AA" },
            { "LightSkyBlue", "#87CEFA" },
            { "LightSlateGray", "#778899" },
            { "LightSlateGrey", "#778899" },
            { "LightSteelBlue", "#B0C4DE" },
            { "LightYellow", "#FFFFE0" },
            { "Lime", "#00FF00" },
            { "LimeGreen", "#32CD32" },
            { "Linen", "#FAF0E6" },
            { "Magenta", "#FF00FF" },
            { "Maroon", "#800000" },
            { "MediumAquaMarine", "#66CDAA" },
            { "MediumBlue", "#0000CD" },
            { "MediumOrchid", "#BA55D3" },
            { "MediumPurple", "#9370DB" },
            { "MediumSeaGreen", "#3CB371" },
            { "MediumSlateBlue", "#7B68EE" },
            { "MediumSpringGreen", "#00FA9A" },
            { "MediumTurquoise", "#48D1CC" },
            { "MediumVioletRed", "#C71585" },
            { "MidnightBlue", "#191970" },
            { "MintCream", "#F5FFFA" },
            { "MistyRose", "#FFE4E1" },
            { "Moccasin", "#FFE4B5" },
            { "NavajoWhite", "#FFDEAD" },
            { "Navy", "#000080" },
            { "OldLace", "#FDF5E6" },
            { "Olive", "#808000" },
            { "OliveDrab", "#6B8E23" },
            { "Orange", "#FFA500" },
            { "OrangeRed", "#FF4500" },
            { "Orchid", "#DA70D6" },
            { "PaleGoldenRod", "#EEE8AA" },
            { "PaleGreen", "#98FB98" },
            { "PaleTurquoise", "#AFEEEE" },
            { "PaleVioletRed", "#DB7093" },
            { "PapayaWhip", "#FFEFD5" },
            { "PeachPuff", "#FFDAB9" },
            { "Peru", "#CD853F" },
            { "Pink", "#FFC0CB" },
            { "Plum", "#DDA0DD" },
            { "PowderBlue", "#B0E0E6" },
            { "Purple", "#800080" },
            { "RebeccaPurple", "#663399" },
            { "Red", "#FF0000" },
            { "RosyBrown", "#BC8F8F" },
            { "RoyalBlue", "#4169E1" },
            { "SaddleBrown", "#8B4513" },
            { "Salmon", "#FA8072" },
            { "SandyBrown", "#F4A460" },
            { "SeaGreen", "#2E8B57" },
            { "SeaShell", "#FFF5EE" },
            { "Sienna", "#A0522D" },
            { "Silver", "#C0C0C0" },
            { "SkyBlue", "#87CEEB" },
            { "SlateBlue", "#6A5ACD" },
            { "SlateGray", "#708090" },
            { "SlateGrey", "#708090" },
            { "Snow", "#FFFAFA" },
            { "SpringGreen", "#00FF7F" },
            { "SteelBlue", "#4682B4" },
            { "Tan", "#D2B48C" },
            { "Teal", "#008080" },
            { "Thistle", "#D8BFD8" },
            { "Tomato", "#FF6347" },
            { "Turquoise", "#40E0D0" },
            { "Violet", "#EE82EE" },
            { "Wheat", "#F5DEB3" },
            { "White", "#FFFFFF" },
            { "WhiteSmoke", "#F5F5F5" },
            { "Yellow", "#FFFF00" },
            { "YellowGreen", "#9ACD32" }
        }; 

        public decimal Red { get; }
        public decimal Green { get; }
        public decimal Blue { get; }
        public decimal Alpha { get; } = 1.0m;

        public Color()
        {
        }

        public Color(decimal r, decimal g, decimal b, decimal a = 1.0m)
        {
            if(r > 1 || r < 0) throw new ArgumentException("r");
            if (g > 1 || g < 0) throw new ArgumentException("g");
            if (b > 1 || b < 0) throw new ArgumentException("b");
            if (a > 1 || a < 0) throw new ArgumentException("a");

            Red = r;
            Green = g;
            Blue = b;
            Alpha = a;
        }

        public static Color FromBytes(byte r, byte g, byte b, byte a = 255)
        {
            return new Color(
                (decimal)r / 255,
                (decimal)g / 255,
                (decimal)b / 255,
                (decimal)a / 255
            );
        }

        public static Color FromHexString(string hexString)
        {
            var regex = new Regex(@"^#(?<hex>[0-9a-f]{3}|[0-9a-f]{6})$", RegexOptions.IgnoreCase);
            var regexMatch = regex.Match(hexString);
            if (!regexMatch.Success)
            {
                return null;
            }

            var hex = regexMatch.Groups["hex"].ToString();
            if (hex.Length == 3)
            {
                return FromBytes(
                    byte.Parse(new string(hex[0], 2), NumberStyles.HexNumber),
                    byte.Parse(new string(hex[1], 2), NumberStyles.HexNumber),
                    byte.Parse(new string(hex[2], 2), NumberStyles.HexNumber)
                );
            }
            
            return FromBytes(
                byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber)
            );
        }

        public static Color FromNamedColor(string name)
        {
            string hexString;
            if (NamedColorMap.TryGetValue(name, out hexString))
            {
                return FromHexString(hexString);
            }

            return null;
        }

        public override string ToString()
        {
            return $"#{(int) (Red * 255):X2}{(int) (Green * 255):X2}{(int) (Blue * 255):X2}{(Alpha != 1 ? ((int) (Alpha * 255)).ToString("X2") : "")}";
        }
    }
}
