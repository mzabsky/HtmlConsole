using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Match = Eto.Parse.Match;

namespace HtmlConsole.Css
{
    public class ColorStyleValue : StyleValue
    {
        public Color Color { get; }

        public override Type GetStyleValueType() => GetType();

        public ColorStyleValue(Color color)
        {
            Color = color;
        }

        internal static ColorStyleValue TryCreate(Match match)
        {
            Color color;
            if ((color = Color.FromNamedColor(match.Text)) != null)
            {
                return new ColorStyleValue(color);
            }

            if ((color = Color.FromHexString(match.Text)) != null)
            {
                return new ColorStyleValue(color);
            }

            // TODO: Function notation

            return null;
        }
    }
}