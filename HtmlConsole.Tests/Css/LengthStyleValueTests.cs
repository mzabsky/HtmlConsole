using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class LengthStyleValueTests
    {
        // The parser is immutable
        private readonly TestingStyleParser _parser = new TestingStyleParser();

        public enum TestEnum
        {
            ValueA,
            ValueB,
            ValueC
        }

        [TestMethod]
        public void TryCreate_PxValue_CreatesCorrectStyleValue()
        {
            var styleValue = LengthStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("1px", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(1, styleValue.Length);
            Assert.AreEqual(LengthUnit.Px, styleValue.Unit);
        }

        [TestMethod]
        public void TryCreate_EmValue_CreatesCorrectStyleValue()
        {
            var styleValue = LengthStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("999em", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(999, styleValue.Length);
            Assert.AreEqual(LengthUnit.Em, styleValue.Unit);
        }

        [TestMethod]
        public void TryCreate_DecimalEmValue_CreatesCorrectStyleValue()
        {
            var styleValue = LengthStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("999.99em", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(999.99m, styleValue.Length);
            Assert.AreEqual(LengthUnit.Em, styleValue.Unit);
        }

        [TestMethod]
        public void TryCreate_Zero_CreatesCorrectStyleValue()
        {
            var styleValue = LengthStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("0", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(0, styleValue.Length);
            Assert.AreEqual(LengthUnit.None, styleValue.Unit);
        }

        [TestMethod]
        public void TryCreate_NoneZeroWithoutDimension_ReturnsNull()
        {
            var styleValue = LengthStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("5", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.IsNull(styleValue);
        }

        [TestMethod]
        public void TryCreate_InvalidString_ReturnsNull()
        {
            var styleValue = LengthStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("sdfadf", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.IsNull(styleValue);
        }
    }
}
