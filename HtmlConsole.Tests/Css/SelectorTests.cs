using HtmlConsole.Css;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class SelectorTests
    {
        // The parser is immutable
        private readonly TestingStyleParser _parser = new TestingStyleParser();

        private void TestCreateSelector(string input, string output)
        {
            var actual = Selector.Create(_parser.TestingGetSyntaxTree(input, StyleParserMode.Selector));
            Assert.AreEqual(output, actual.ToString());
        }

        [TestMethod]
        public void Create_SimpleElementSelector_BuildsCorrectSelector()
        {
            TestCreateSelector(
                "a",
                "[OR [AND [a]]]"
            );
        }

        [TestMethod]
        public void Create_SelectorWithCommas_BuildsCorrectSelector()
        {
            TestCreateSelector(
                "a, b, c, d",
                "[OR [AND [a]] OR [AND [b]] OR [AND [c]] OR [AND [d]]]"
            );
        }

        [TestMethod]
        public void Create_SelectorWithComplexFragment_BuildsCorrectSelector()
        {
            TestCreateSelector(
                "a.b#c",
                "[OR [AND [a] AND [.b] AND [#c]]]"
            );
        }

        [TestMethod]
        public void Create_SelectorWithCombinators_BuildsCorrectSelector()
        {
            TestCreateSelector(
                "a b > c d",
                "[OR [AND [ [AND [>[AND [ [AND [a]]] AND [AND [b]]]] AND [AND [c]]]] AND [AND [d]]]]"
            );
        }
    }
}
