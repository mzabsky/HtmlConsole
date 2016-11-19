using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class PercentageStyleValueTests
    {
        // The parser is immutable
        private readonly TestingStyleParser _parser = new TestingStyleParser();

        [TestMethod]
        public void TryCreate_IntegerValue_CreatesCorrectStyleValue()
        {
            var styleValue = PercentageStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("1%", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(1, styleValue.Percentage);
        }

        [TestMethod]
        public void TryCreate_DecimalValue_CreatesCorrectStyleValue()
        {
            var styleValue = PercentageStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("999.99%", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(999.99m, styleValue.Percentage);
        }

        [TestMethod]
        public void TryCreate_InvalidString_ReturnsNull()
        {
            var styleValue = PercentageStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("sdfadf", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.IsNull(styleValue);
        }
    }
}
