using System;
using System.Collections.Generic;

namespace HtmlConsole.Css
{
    public class BoxStyleProperty : StyleProperty
    {
        public Type[] AllowedTypes { get; set; }
        public string NamePattern { get; set; }

        protected override IEnumerable<Type> GetAllowedTypesCore()
        {
            return AllowedTypes;
        }

        public override IEnumerable<KeyValuePair<string, StyleValue>> MapStyleValues(StyleValue[] values)
        {
            switch (values.Length)
            {
                case 0:
                    return new Dictionary<string, StyleValue>();
                case 1:
                    return new Dictionary<string, StyleValue>
                    {
                        { string.Format(NamePattern, "top"), values[0] },
                        { string.Format(NamePattern, "right"), values[0] },
                        { string.Format(NamePattern, "bottom"), values[0] },
                        { string.Format(NamePattern, "left"), values[0] },
                    };
                case 2:
                    return new Dictionary<string, StyleValue>
                    {
                        { string.Format(NamePattern, "top"), values[0] },
                        { string.Format(NamePattern, "right"), values[1] },
                        { string.Format(NamePattern, "bottom"), values[0] },
                        { string.Format(NamePattern, "left"), values[1] },
                    };
                case 3:
                    return new Dictionary<string, StyleValue>
                    {
                        { string.Format(NamePattern, "top"), values[0] },
                        { string.Format(NamePattern, "right"), values[1] },
                        { string.Format(NamePattern, "bottom"), values[2] },
                        { string.Format(NamePattern, "left"), values[1] },
                    };
                default:
                    return new Dictionary<string, StyleValue>
                    {
                        { string.Format(NamePattern, "top"), values[0] },
                        { string.Format(NamePattern, "right"), values[1] },
                        { string.Format(NamePattern, "bottom"), values[2] },
                        { string.Format(NamePattern, "left"), values[3] },
                    };
            }
        }
    }
}