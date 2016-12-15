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

            propertyList.Add(BackgroundColor = 
                new SimpleStyleProperty(
                    "background-color", 
                    typeof(ColorStyleValue), 
                    new ColorStyleValue(Color.Transparent)
                )
            );
            
            propertyList.Add(BorderTop = 
                new SequenceStyleProperty(
                    "border-top", 
                    new[]
                    {
                        new KeyValuePair<string, Type[]>("border-top-width", new[] { typeof(LengthStyleValue), typeof(BorderThickness) }),
                        new KeyValuePair<string, Type[]>("border-top-color", new[] { typeof(ColorStyleValue) }),
                        new KeyValuePair<string, Type[]>("border-top-style", new[] { typeof(BorderStyle) })
                    }
                )
            );

            propertyList.Add(Display = 
                new SimpleStyleProperty(
                    "display", 
                    typeof(Display), 
                    new EnumStyleValue<Display>(Css.Display.Inline)
                )
            );

            propertyList.Add(Margin = 
                new BoxStyleProperty(
                    "margin", 
                    "margin-{0}", 
                    typeof(LengthStyleValue)
                )
            );

            propertyList.Add(MarginTop = new SimpleStyleProperty("margin-top", typeof(LengthStyleValue), new LengthStyleValue(0, LengthUnit.None)));

            propertyList.Add(Width = 
                new SimpleStyleProperty(
                    "width", 
                    new[] { typeof(LengthStyleValue), typeof(PercentageStyleValue), typeof(AutoStyleValue) }, 
                    new AutoStyleValue()
                )
            );

            PropertyIndex = propertyList.ToDictionary(p => p.PropertyName, p => p);
        }
        
        public static IDictionary<string, StyleProperty> All() => PropertyIndex;

        public static StyleProperty Get(string propertyName) => PropertyIndex.GetValueOrDefault(propertyName);
    }
}
