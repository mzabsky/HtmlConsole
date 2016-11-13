using System.Linq;
using HtmlConsole.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class SimpleStylePropertyTests
    {
        [TestMethod]
        public void MapStyleValues_EmptyValueSequence_ReturnsEmptyDictionary()
        {
            var property = new SimpleStyleProperty();
            var actual = property.MapStyleValues(new StyleValue[0]).ToArray();
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void MapStyleValues_SingleValue_ReturnsIt()
        {
            var property = new SimpleStyleProperty { PropertyName = "hello" };
            var styleValue = new AutoStyleValue();
            var actual = property.MapStyleValues(new StyleValue[] { styleValue }).ToArray();
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(styleValue, actual.Single(p => p.Key == "hello").Value);
        }

        [TestMethod]
        public void MapStyleValues_MultipleValues_ReturnsOnlyFirst()
        {
            var property = new SimpleStyleProperty { PropertyName = "hello" };
            var styleValue = new AutoStyleValue();
            var styleValue2 = new AutoStyleValue();
            var styleValue3 = new AutoStyleValue();
            var actual = property.MapStyleValues(new StyleValue[] { styleValue, styleValue2, styleValue3 }).ToArray();
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(styleValue, actual.Single(p => p.Key == "hello").Value);
        }
    }
}
