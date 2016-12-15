using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Css
{
    public class SimpleStyleProperty : StyleProperty
    {
        public Type[] AllowedTypes { get; set; }

        public SimpleStyleProperty(string propertyName, Type allowedType, StyleValue initialValue = null, bool isInherited = false) : base(propertyName, initialValue, isInherited)
        {
            AllowedTypes = new [] {allowedType};
        }

        public SimpleStyleProperty(string propertyName, Type[] allowedTypes, StyleValue initialValue = null, bool isInherited = false) : base(propertyName, initialValue, isInherited)
        {
            AllowedTypes = allowedTypes;
        }

        protected override IEnumerable<Type> GetAllowedTypesCore()
        {
            return AllowedTypes;
        }

        public override IEnumerable<KeyValuePair<string, StyleValue>> MapStyleValues(StyleValue[] values)
        {
            if (values.Length == 0)
            {
                return new List<KeyValuePair<string, StyleValue>>();
            }

            // The default mapping is to 
            return new Dictionary<string, StyleValue>
            {
                { PropertyName, values.First() }
            };
        }
    }
}