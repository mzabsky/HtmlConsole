using System;
using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class SequenceStylePropertyTests
    {
        [TestMethod]
        public void GetAllowedTypes_ThreeByThreeSequence_ContainsAllTypes()
        {
            var property = new SequenceStyleProperty("a", new[]
                {
                    new KeyValuePair<string, Type[]>("a", new []{ typeof(AutoStyleValue), typeof(Display) }),
                    new KeyValuePair<string, Type[]>("b", new []{ typeof(BorderThickness) }),
                    new KeyValuePair<string, Type[]>("c", new []{ typeof(BorderStyle) }),
                });
            
            var actual = property.GetAllowedTypes();
            Assert.AreEqual(4, actual.Length);
            Assert.IsTrue(actual.Contains(typeof(AutoStyleValue)));
            Assert.IsTrue(actual.Contains(typeof(Display)));
            Assert.IsTrue(actual.Contains(typeof(BorderThickness)));
            Assert.IsTrue(actual.Contains(typeof(BorderStyle)));
        }

        [TestMethod]
        public void MapStyleValues_EmptyValueSequenceEmptyTypeSequence_ReturnsEmptyDictionary()
        {
            var property = new SequenceStyleProperty("a", new KeyValuePair < string, Type[] >[0]);
            var actual = property.MapStyleValues(new StyleValue[0]).ToArray();
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void MapStyleValues_EmptyValueSequenceHasTypeSequence_ReturnsEmptyDictionary()
        {
            var property = new SequenceStyleProperty("a", new[]
                {
                    new KeyValuePair<string, Type[]>("a", new []{ typeof(AutoStyleValue), typeof(Display) }),
                    new KeyValuePair<string, Type[]>("b", new []{ typeof(BorderThickness) }),
                    new KeyValuePair<string, Type[]>("c", new []{ typeof(BorderStyle) }),
                });
            
            var actual = property.MapStyleValues(new StyleValue[0]).ToArray();
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void MapStyleValues_ExactMatchTypeSequence_ReturnsCorrectMappings()
        {
            var property = new SequenceStyleProperty("a", new[]
                {
                    new KeyValuePair<string, Type[]>("a", new []{ typeof(AutoStyleValue), typeof(Display) }),
                    new KeyValuePair<string, Type[]>("b", new []{ typeof(BorderThickness) }),
                    new KeyValuePair<string, Type[]>("c", new []{ typeof(BorderStyle) }),
                });

            StyleValue styleValue1, styleValue2, styleValue3;
            var actual = property.MapStyleValues(new []
            {
                styleValue1= new AutoStyleValue(),
                styleValue2 = new EnumStyleValue<BorderThickness>(BorderThickness.Medium),
                styleValue3 = new EnumStyleValue<BorderStyle>(BorderStyle.Dashed),
            }).ToDictionary();

            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(styleValue1, actual["a"]);
            Assert.AreEqual(styleValue2, actual["b"]);
            Assert.AreEqual(styleValue3, actual["c"]);
        }

        [TestMethod]
        public void MapStyleValues_MissingPropertyInSequence_SkipsThatProperty()
        {
            var property = new SequenceStyleProperty("a", new[]
                {
                    new KeyValuePair<string, Type[]>("a", new []{ typeof(AutoStyleValue), typeof(Display) }),
                    new KeyValuePair<string, Type[]>("b", new []{ typeof(BorderThickness) }),
                    new KeyValuePair<string, Type[]>("c", new []{ typeof(BorderStyle) }),
                });

            StyleValue styleValue1, styleValue3;
            var actual = property.MapStyleValues(new []
            {
                styleValue1= new AutoStyleValue(),
                styleValue3 = new EnumStyleValue<BorderStyle>(BorderStyle.Dashed),
            }).ToDictionary();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(styleValue1, actual["a"]);
            Assert.AreEqual(styleValue3, actual["c"]);
        }

        [TestMethod]
        public void MapStyleValues_MissingPropertyAtEndOfSequence_SkipsThatProperty()
        {
            var property = new SequenceStyleProperty("a", new[]
                {
                    new KeyValuePair<string, Type[]>("a", new []{ typeof(AutoStyleValue), typeof(Display) }),
                    new KeyValuePair<string, Type[]>("b", new []{ typeof(BorderThickness) }),
                    new KeyValuePair<string, Type[]>("c", new []{ typeof(BorderStyle) }),
                });

            StyleValue styleValue1, styleValue2;
            var actual = property.MapStyleValues(new []
            {
                styleValue1= new AutoStyleValue(),
                styleValue2 = new EnumStyleValue<BorderThickness>(BorderThickness.Medium),
            }).ToDictionary();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(styleValue1, actual["a"]);
            Assert.AreEqual(styleValue2, actual["b"]);
        }

        // property type sequence bude obsahovat primo enumy! mapovane typy uz ale enumy nebudou
    }
}
