using System;
using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Extensions;

namespace HtmlConsole.Css
{
    public static class StyleProperties
    {
        private static readonly Dictionary<string, StyleProperty> PropertyIndex;

        public static StyleProperty BackgroundColor;

        public static StyleProperty BorderTop;

        public static StyleProperty Display;

        public static StyleProperty Margin;
        public static StyleProperty MarginTop;

        public static StyleProperty Width;

        static StyleProperties()
        {
            var propertyList = new List<StyleProperty>();

            propertyList.Add(BackgroundColor = new SimpleStyleProperty
            {
                PropertyName = "background-color",
                AllowedTypes = new[] { typeof(ColorStyleValue) },
            });
            
            propertyList.Add(BorderTop = new SequenceStyleProperty
            {
                PropertyName = "border-top",
                PropertySequence = new[]
                {
                    new KeyValuePair<string, Type[]>("border-top-width", new[] { typeof(LengthStyleValue), typeof(BorderThickness) }),
                    new KeyValuePair<string, Type[]>("border-top-color", new[] { typeof(ColorStyleValue) }),
                    new KeyValuePair<string, Type[]>("border-top-style", new[] { typeof(BorderStyle) })
                }
            });

            propertyList.Add(Display = new SimpleStyleProperty
            {
                PropertyName = "display",
                AllowedTypes = new[] { typeof(Display) },
            });

            propertyList.Add(Margin = new BoxStyleProperty
            {
                PropertyName = "margin",
                NamePattern = "margin-{0}",
                AllowedTypes = new[] { typeof(LengthStyleValue) }
            });

            propertyList.Add(Margin = new SimpleStyleProperty
            {
                PropertyName = "margin-top",
                AllowedTypes = new[] { typeof(LengthStyleValue) },
            });

            propertyList.Add(Width = new SimpleStyleProperty
            {
                PropertyName = "width",
                AllowedTypes = new[] { typeof(LengthStyleValue), typeof(PercentageStyleValue), typeof(AutoStyleValue) },
            });

            PropertyIndex = propertyList.ToDictionary(p => p.PropertyName, p => p);
        }
        
        public static IDictionary<string, StyleProperty> All() => PropertyIndex;

        public static StyleProperty Get(string propertyName) => PropertyIndex.GetValueOrDefault(propertyName);
    }
}
