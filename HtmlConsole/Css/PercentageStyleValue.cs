using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Match = Eto.Parse.Match;

namespace HtmlConsole.Css
{
    public class PercentageStyleValue : StyleValue
    {
        public decimal Percentage { get; }

        public override Type GetStyleValueType() => GetType();

        public PercentageStyleValue(decimal percentage)
        {
            Percentage = percentage;
        }

        internal static PercentageStyleValue TryCreate(Match match)
        {
            var regex = new Regex(@"(?<number>[0-9]+(\.[0-9]+)?)%", RegexOptions.IgnoreCase);
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

            return new PercentageStyleValue(number);
        }
    }
}
