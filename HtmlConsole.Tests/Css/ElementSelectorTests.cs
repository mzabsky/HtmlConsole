using HtmlConsole.Css;
using HtmlConsole.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class ElementSelectorTests
    {
        [TestMethod]
        public void Match_MatchingElement_ReturnsTrue()
        {
            var node = new ElementNode {Element = "a"};
            var selector = new ElementSelector {ElementName = "a"};
            Assert.AreEqual(true, selector.Match(node));
        }

        [TestMethod]
        public void Match_NotMatchingElement_ReturnsFalse()
        {
            var node = new ElementNode {Element = "a"};
            var selector = new ElementSelector {ElementName = "b"};
            Assert.AreEqual(false, selector.Match(node));
        }
    }
}
