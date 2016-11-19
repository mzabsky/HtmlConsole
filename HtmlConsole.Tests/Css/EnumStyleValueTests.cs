using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class EnumStyleValueTests
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
        public void TryCreate_MemberString_CreatesCorrectStyleValue()
        {
            var styleValue = (EnumStyleValue<TestEnum>)EnumStyleValue.TryCreate(
                typeof(TestEnum),
                _parser.TestingGetSyntaxTree("valuEB", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.AreEqual(TestEnum.ValueB, styleValue.EnumValue);
        }

        [TestMethod]
        public void TryCreate_NonMemberString_ReturnNull()
        {
            var styleValue = (EnumStyleValue<TestEnum>)EnumStyleValue.TryCreate(
                typeof(TestEnum),
                _parser.TestingGetSyntaxTree("ValueD", StyleParserMode.StyleValue).Matches.First()
            );

            Assert.IsNull(styleValue);
        }
    }
}
