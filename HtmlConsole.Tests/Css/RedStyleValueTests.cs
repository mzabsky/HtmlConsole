using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class RedStyleValueTests
    {
        // The parser is immutable
        private readonly TestingStyleParser _parser = new TestingStyleParser();

        [TestMethod]
        public void TryCreate_HexString_CreatesCorrectStyleValue()
        {
            var styleValue = ColorStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("#00ff00", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(0, styleValue.Color.Red);
            Assert.AreEqual(1, styleValue.Color.Green);
            Assert.AreEqual(0, styleValue.Color.Blue);
            Assert.AreEqual(1, styleValue.Color.Alpha);
        }
        [TestMethod]
        public void TryCreate_NamedColor_CreatesCorrectStyleValue()
        {
            var styleValue = ColorStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("lime", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(0, styleValue.Color.Red);
            Assert.AreEqual(1, styleValue.Color.Green);
            Assert.AreEqual(0, styleValue.Color.Blue);
            Assert.AreEqual(1, styleValue.Color.Alpha);
        }
        
        [TestMethod]
        public void TryCreate_InvalidString_ReturnsNull()
        {
            var styleValue = ColorStyleValue.TryCreate(
                _parser.TestingGetSyntaxTree("sdfadf", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.IsNull(styleValue);
        }
    }
}
