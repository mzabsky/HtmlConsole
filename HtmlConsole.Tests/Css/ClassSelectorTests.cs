using HtmlConsole.Css;
using HtmlConsole.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class ClassSelectorTests
    {
        [TestMethod]
        public void Match_MatchingClass_ReturnsTrue()
        {
            var node = new ElementNode {Element = "a", Classes = new []{"ca", "cb"}};
            var selector = new ClassSelector {Class = "ca"};
            Assert.AreEqual(true, selector.Match(node));
        }

        [TestMethod]
        public void Match_NotMatchingId_ReturnsFalse()
        {
            var node = new ElementNode {Element = "a", Classes = new[] { "ca", "cb" }};
            var selector = new ClassSelector {Class = "cc"};
            Assert.AreEqual(false, selector.Match(node));
        }
    }
}
