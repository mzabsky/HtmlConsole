using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Css
{
    public abstract class StyleProperty
    {
        private Type[] _cachedAllowedTypes;

        public string PropertyName { get; }
        public bool IsInherited { get; }
        public StyleValue InitialValue { get; }

        protected abstract IEnumerable<Type> GetAllowedTypesCore();

        public abstract IEnumerable<KeyValuePair<string, StyleValue>> MapStyleValues(StyleValue[] values);

        protected StyleProperty(string propertyName, StyleValue initialValue, bool isInherited)
        {
            PropertyName = propertyName;
            IsInherited = isInherited;
            InitialValue = initialValue;
        }

        public Type[] GetAllowedTypes()
        {
            if (_cachedAllowedTypes == null)
            {
                _cachedAllowedTypes = GetAllowedTypesCore().ToArray();
            }

            return _cachedAllowedTypes;
        }

        public override string ToString()
        {
            return PropertyName;
        }
    }
}
