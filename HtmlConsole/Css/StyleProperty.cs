using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Css
{
    public abstract class StyleProperty
    {
        private static Dictionary<string, StyleProperty> _allProperties;

        private Type[] _cachedAllowedTypes;

        public string PropertyName { get; set; }
        public bool IsInherited { get; set; } = false;

        private static IEnumerable<StyleProperty> CreateProperties()
        {
            // Inherit and Initial are automatically enabled for all properties

            yield return new SimpleStyleProperty
            {
                PropertyName = "display",
                AllowedTypes = new [] { typeof(Display) },
            };

            yield return new BoxStyleProperty
            {
                PropertyName = "margin",
                NamePattern = "margin-{0}",
                AllowedTypes = new[] { typeof(LengthStyleValue) }
            };

            yield return new SimpleStyleProperty
            {
                PropertyName = "margin-top",
                AllowedTypes = new[] { typeof(LengthStyleValue) },
            };

            yield return new SequenceStyleProperty
            {
                PropertyName = "border-top",
                PropertySequence = new[]
                {
                    new KeyValuePair<string, Type[]>("border-top-width", new[] { typeof(LengthStyleValue), typeof(BorderThickness) }),
                    new KeyValuePair<string, Type[]>("border-top-color", new[] { typeof(ColorStyleValue) }),
                    new KeyValuePair<string, Type[]>("border-top-style", new[] { typeof(BorderStyle) })
                }
            };

            /*
            yield return new StyleProperty
            {
                PropertyName = "border",
                AllowedTypes = new[] { typeof(EnumStyleValue<Display>) }
            };*/
        }

        public static Dictionary<string, StyleProperty> GetAllProperties()
        {
            if (_allProperties == null)
            {
                _allProperties = CreateProperties().ToDictionary(p => p.PropertyName, p => p);
            }

            return _allProperties;
        }

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
