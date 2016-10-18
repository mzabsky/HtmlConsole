using System.Text.RegularExpressions;

namespace HtmlConsole.Css
{
    public enum StyleValueType
    {
        Default,
        Inherit,
        Absolute,
        Auto,
        Percent
    }

    public class StyleValue<T> where T: struct
    {
        public T Value { get; set; }

        public StyleValueType Type { get; set; }

        public StyleValue()
        {
            Value = default(T);
            Type = StyleValueType.Default;
        }

        public StyleValue(T value, StyleValueType type)
        {
            Value = value;
            Type = type;
        }

        public static StyleValue<TValue> Parse<TValue>(string str) where TValue : struct
        {
            if (str == "auto")
            {
                return new StyleValue<TValue>(default(TValue), StyleValueType.Auto);
            }

            if (str == "default")
            {
                return new StyleValue<TValue>(default(TValue), StyleValueType.Auto);
            }

            var match = Regex.Match(str, "^(?<value>[0-9]+(.[0-9]+)?)(?<unit>em|px|cm|%)$");
            if (!match.Success)
            {
                return null;
            }

            double value;
            if (!double.TryParse(match.Groups["value"].ToString(), out value))
            {
                return null;
            }

            if (match.Groups["unit"].ToString() == "%")
            {
                return new StyleValue<TValue>(default(TValue), StyleValueType.Percent);
            }
            return new StyleValue<TValue>(default(TValue), StyleValueType.Absolute);
        }
    }
}
