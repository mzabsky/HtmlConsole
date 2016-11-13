using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Match = Eto.Parse.Match;

namespace HtmlConsole.Css
{
    public enum LengthUnit
    {
        None,
        Px,
        Cm,
        Ex,
        Em,
        Mm,
        In,
        Pt
    }

    public class LengthStyleValue: StyleValue
    {
        public decimal Length { get; set; }

        public LengthUnit Unit { get; set; }

        public override Type GetStyleValueType() => GetType();

        public static LengthStyleValue TryCreate(Match match)
        {
            /*if (match.Name != "number")
            {
                return null;
            }*/

            var regex = new Regex(@"(?<number>[0-9]+(\.[0-9]+)?)(?<unit>([a-z]{2})?)", RegexOptions.IgnoreCase);
            var regexMatch = regex.Match(match.Text);
            if (!regexMatch.Success)
            {
                return null;
            }

            decimal number;
            if (!decimal.TryParse(regexMatch.Groups["number"].ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out number))
            {
                return null;
            }

            LengthUnit unit;
            if (string.IsNullOrWhiteSpace(regexMatch.Groups["unit"].ToString()))
            {
                unit = LengthUnit.None;

                if (number != 0)
                {
                    // Only zero can be dimensionless
                    return null;
                }
            }
            else
            {
                string unitString = regexMatch.Groups["unit"].ToString();
                if (!Enum.TryParse<LengthUnit>(unitString, true, out unit))
                {
                    return null;
                }
            }
            
            return new LengthStyleValue
            {
                Length = number,
                Unit = unit
            };
        }
    }
}
