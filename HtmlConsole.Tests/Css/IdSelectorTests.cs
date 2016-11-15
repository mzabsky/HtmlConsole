using HtmlConsole.Css;
using HtmlConsole.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class IdSelectorTests
    {
        [TestMethod]
        public void Match_MatchingId_ReturnsTrue()
        {
            var node = new ElementNode {Element = "a", Id = "ida"};
            var selector = new IdSelector {Id = "ida"};

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(1, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_NotMatchingId_ReturnsFalse()
        {
            var node = new ElementNode {Element = "a", Id = "ida"};
            var selector = new IdSelector {Id = "idb"};

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }
    }
}
