using System;
using System.Collections.Generic;

namespace HtmlConsole.Css
{
    public enum BorderStyle
    {
        Double,
        Solid,
        None
    }

    public enum Display
    {
        Inline,
        Block,
        None
    }

    public class ParsedStyle
    {
        public ConsoleColor? BackgroundColor { get; set; }
        public ConsoleColor? Color { get; set; }

        public ConsoleColor? BorderTopColor { get; set; }
        public BorderStyle? BorderTopStyle { get; set; }
        /*public NumericStyleValue BorderTopWidth { get; set; }

        public ConsoleColor? BorderRightColor { get; set; }
        public BorderStyle? BorderRightStyle { get; set; }
        public NumericStyleValue BorderRightWidth { get; set; }

        public ConsoleColor? BorderBottomColor { get; set; }
        public BorderStyle? BorderBottomStyle { get; set; }
        public NumericStyleValue BorderBottomWidth { get; set; }

        public ConsoleColor? BorderLeftColor { get; set; }
        public BorderStyle? BorderLeftStyle { get; set; }
        public NumericStyleValue BorderLeftWidth { get; set; }

        public Display Display { get; set; }

        public NumericStyleValue MarginTop { get; set; }
        public NumericStyleValue MarginRight { get; set; }
        public NumericStyleValue MarginBottom { get; set; }
        public NumericStyleValue MarginLeft { get; set; }
        
        public NumericStyleValue PaddingTop { get; set; }
        public NumericStyleValue PaddingRight { get; set; }
        public NumericStyleValue PaddingBottom { get; set; }
        public NumericStyleValue PaddingLeft { get; set; }
        
        public NumericStyleValue Width { get; set; }
        public NumericStyleValue Height { get; set; }*/

        public static ParsedStyle Parse(Dictionary<string, Style> styles)
        {
            var parsedStyle = new ParsedStyle();

            foreach (var style in styles.Values)
            {
                switch (style.Property.ToLower())
                {
                    case "background":
                    case "background-color":
                        parsedStyle.BackgroundColor = StringToConsoleColor(style.Value);
                        break;
                    case "color":
                        parsedStyle.Color = StringToConsoleColor(style.Value);
                        break;
                    /*case "border":
                        {
                            var borderTuple = ParseBorderNotation(style.Value);
                            parsedStyle.BorderTopColor = parsedStyle.BorderRightColor = parsedStyle.BorderBottomColor = parsedStyle.BorderLeftColor = borderTuple.Item1;
                            parsedStyle.BorderTopStyle = parsedStyle.BorderRightStyle = parsedStyle.BorderBottomStyle = parsedStyle.BorderLeftStyle = borderTuple.Item2;
                            parsedStyle.BorderTopWidth = parsedStyle.BorderRightWidth = parsedStyle.BorderBottomWidth = parsedStyle.BorderLeftWidth = new NumericStyleValue(1, NumericStyleValueType.Absolute);
                        }
                        break;
                    case "border-top":
                        {
                            var borderTuple = ParseBorderNotation(style.Value);
                            parsedStyle.BorderTopColor = borderTuple.Item1;
                            parsedStyle.BorderTopStyle = borderTuple.Item2;
                            parsedStyle.BorderTopWidth = new NumericStyleValue(1, NumericStyleValueType.Absolute);
                        }
                        break;
                    case "border-right":
                        {
                            var borderTuple = ParseBorderNotation(style.Value);
                            parsedStyle.BorderRightColor = borderTuple.Item1;
                            parsedStyle.BorderRightStyle = borderTuple.Item2;
                            parsedStyle.BorderRightWidth = new NumericStyleValue(1, NumericStyleValueType.Absolute);
                        }
                        break;
                    case "border-bottom":
                        {
                            var borderTuple = ParseBorderNotation(style.Value);
                            parsedStyle.BorderBottomColor = borderTuple.Item1;
                            parsedStyle.BorderBottomStyle = borderTuple.Item2;
                            parsedStyle.BorderBottomWidth = new NumericStyleValue(1, NumericStyleValueType.Absolute);
                        }
                        break;
                    case "border-left":
                        {
                            var borderTuple = ParseBorderNotation(style.Value);
                            parsedStyle.BorderLeftColor = borderTuple.Item1;
                            parsedStyle.BorderLeftStyle = borderTuple.Item2;
                            parsedStyle.BorderLeftWidth = new NumericStyleValue(1, NumericStyleValueType.Absolute);
                        }
                        break;
                    case "display":*/

                }
            }

            return parsedStyle;
        }

        internal static Tuple<ConsoleColor?, BorderStyle?> ParseBorderNotation(string borderNotation)
        {
            ConsoleColor? color = null;
            BorderStyle? style = null;

            var split = borderNotation.Split(' ');
            if (split.Length > 0)
            {
                color = StringToConsoleColor(split[0]);
            }

            if (split.Length > 1)
            {
                style = StringToBorderStyle(split[1]);
            }
            else
            {
                style = BorderStyle.Solid;
            }

            return Tuple.Create(color, style);
        }

        internal static ConsoleColor? StringToConsoleColor(string styleColor)
        {
            switch (styleColor)
            {
                case "black": return ConsoleColor.Black;
                case "blue": return ConsoleColor.Blue;
                case "cyan": return ConsoleColor.Cyan;
                case "dark blue": return ConsoleColor.DarkBlue;
                case "dark cyan": return ConsoleColor.DarkCyan;
                case "dark gray": return ConsoleColor.DarkGray;
                case "dark green": return ConsoleColor.DarkGreen;
                case "dark magenta": return ConsoleColor.DarkMagenta;
                case "dark red": return ConsoleColor.DarkRed;
                case "dark yellow": return ConsoleColor.DarkYellow;
                case "gray": return ConsoleColor.Gray;
                case "green": return ConsoleColor.Green;
                case "white": return ConsoleColor.White;
                case "yello": return ConsoleColor.Yellow;
                case "magenta": return ConsoleColor.Magenta;
                case "red": return ConsoleColor.Red;
                default: return null;
            }
        }

        internal static BorderStyle? StringToBorderStyle(string styleColor)
        {
            switch (styleColor)
            {
                case "solid": return BorderStyle.Solid;
                case "double": return BorderStyle.Double;
                case "none": return BorderStyle.None;
                default: return null;
            }
        }

        internal static Display? StringToDisplay(string styleColor)
        {
            switch (styleColor)
            {
                case "block": return Display.Block;
                case "inline": return Display.Inline;
                case "none": return Display.None;
                default: return null;
            }
        }
    }
}
