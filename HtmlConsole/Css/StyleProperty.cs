using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Css
{
    public abstract class StyleProperty
    {
        private Type[] _cachedAllowedTypes;

        public string PropertyName { get; set; }
        public bool IsInherited { get; set; } = false;
        
        protected abstract IEnumerable<Type> GetAllowedTypesCore();

        public abstract IEnumerable<KeyValuePair<string, StyleValue>> MapStyleValues(StyleValue[] values);

        public Type[] GetAllowedTypes()
        {
            if (_cachedAllowedTypes == null)
            {
                _cachedAllowedTypes = GetAllowedTypesCore().ToArray();
            }

            return _cachedAllowedTypes;
        }
    }
}
