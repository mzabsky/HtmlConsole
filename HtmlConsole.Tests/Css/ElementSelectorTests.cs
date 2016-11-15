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

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(1, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_NotMatchingElement_ReturnsFalse()
        {
            var node = new ElementNode {Element = "a"};
            var selector = new ElementSelector {ElementName = "b"};

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }
    }
}
