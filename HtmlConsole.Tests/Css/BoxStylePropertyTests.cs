using System;
using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class BoxStylePropertyTests
    {
        [TestMethod]
        public void MapStyleValues_EmptyValueSequence_ReturnsEmptyDictionary()
        {
            var property = new BoxStyleProperty("a", "a-{0}", new Type[0]);
            var actual = property.MapStyleValues(new StyleValue[0]).ToArray();
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void MapStyleValues_SingleValue_ReturnsSameAllDirections()
        {
            var property = new BoxStyleProperty("hello", "hel-{0}-lo", new Type[0]);
            var styleValue = new AutoStyleValue();
            var actual = property.MapStyleValues(new StyleValue[] { styleValue }).ToDictionary();
            Assert.AreEqual(4, actual.Count);
            Assert.AreEqual(styleValue, actual["hel-top-lo"]);
            Assert.AreEqual(styleValue, actual["hel-right-lo"]);
            Assert.AreEqual(styleValue, actual["hel-bottom-lo"]);
            Assert.AreEqual(styleValue, actual["hel-left-lo"]);
        }

        [TestMethod]
        public void MapStyleValues_twoValues_ReturnsPairedDirections()
        {
            var property = new BoxStyleProperty("hello", "hel-{0}-lo", new Type[0]);
            var styleValueVertical = new AutoStyleValue();
            var styleValueHorizontal = new AutoStyleValue();
            var actual = property.MapStyleValues(new StyleValue[] { styleValueVertical, styleValueHorizontal }).ToDictionary();
            Assert.AreEqual(4, actual.Count);
            Assert.AreEqual(styleValueVertical, actual["hel-top-lo"]);
            Assert.AreEqual(styleValueHorizontal, actual["hel-right-lo"]);
            Assert.AreEqual(styleValueVertical, actual["hel-bottom-lo"]);
            Assert.AreEqual(styleValueHorizontal, actual["hel-left-lo"]);
        }

        [TestMethod]
        public void MapStyleValues_ThreeValues_FillsInLeft()
        {
            var property = new BoxStyleProperty("hello", "hel-{0}-lo", new Type[0]);
            var styleValue1 = new AutoStyleValue();
            var styleValue2 = new AutoStyleValue();
            var styleValue3 = new AutoStyleValue();
            var actual = property.MapStyleValues(new StyleValue[] { styleValue1, styleValue2, styleValue3 }).ToDictionary();
            Assert.AreEqual(4, actual.Count);
            Assert.AreEqual(styleValue1, actual["hel-top-lo"]);
            Assert.AreEqual(styleValue2, actual["hel-right-lo"]);
            Assert.AreEqual(styleValue3, actual["hel-bottom-lo"]);
            Assert.AreEqual(styleValue2, actual["hel-left-lo"]);
        }

        [TestMethod]
        public void MapStyleValues_ThreeValues_MapsInCorrectDirections()
        {
            var property = new BoxStyleProperty("hello", "hel-{0}-lo", new Type[0]);
            var styleValue1 = new AutoStyleValue();
            var styleValue2 = new AutoStyleValue();
            var styleValue3 = new AutoStyleValue();
            var styleValue4 = new AutoStyleValue();
            var actual = property.MapStyleValues(new StyleValue[] { styleValue1, styleValue2, styleValue3, styleValue4 }).ToDictionary();
            Assert.AreEqual(4, actual.Count);
            Assert.AreEqual(styleValue1, actual["hel-top-lo"]);
            Assert.AreEqual(styleValue2, actual["hel-right-lo"]);
            Assert.AreEqual(styleValue3, actual["hel-bottom-lo"]);
            Assert.AreEqual(styleValue4, actual["hel-left-lo"]);
        }

        [TestMethod]
        public void MapStyleValues_FiveValues_DiscardsFifth()
        {
            var property = new BoxStyleProperty("hello", "hel-{0}-lo", new Type[0]);
            var styleValue1 = new AutoStyleValue();
            var styleValue2 = new AutoStyleValue();
            var styleValue3 = new AutoStyleValue();
            var styleValue4 = new AutoStyleValue();
            var styleValue5 = new AutoStyleValue();
            var actual = property.MapStyleValues(new StyleValue[] { styleValue1, styleValue2, styleValue3, styleValue4, styleValue5 }).ToDictionary();
            Assert.AreEqual(4, actual.Count);
            Assert.AreEqual(styleValue1, actual["hel-top-lo"]);
            Assert.AreEqual(styleValue2, actual["hel-right-lo"]);
            Assert.AreEqual(styleValue3, actual["hel-bottom-lo"]);
            Assert.AreEqual(styleValue4, actual["hel-left-lo"]);
        }
    }
}
