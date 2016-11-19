using System;
using System.Linq;
using Match = Eto.Parse.Match;

namespace HtmlConsole.Css
{
    public class EnumStyleValue<T> : EnumStyleValue
    {
        public T EnumValue { get; }
        
        public EnumStyleValue(int enumValue)
        {
            EnumValue = (T)(object)enumValue;
        }

        public EnumStyleValue(T enumValue)
        {
            EnumValue = enumValue;
        }

        public override Type GetStyleValueType() => typeof(T);
    }

    public abstract class EnumStyleValue : StyleValue
    {
        public static StyleValue TryCreate(Type enumType, Match termMatch)
        {
            var str = termMatch.Text.ToLower();
            var enumValues = enumType.GetEnumValues().OfType<object>().Select(p => new { String = p.ToString().ToLower(), Numeric = (int)p });

            var matchingEnumValue = enumValues.FirstOrDefault(p => p.String == str);
            if (matchingEnumValue == null)
            {
                return null;
            }

            var styleValueType = typeof(EnumStyleValue<>).MakeGenericType(enumType);
            return (StyleValue)Activator.CreateInstance(styleValueType, matchingEnumValue.Numeric);
        }
    }
}
