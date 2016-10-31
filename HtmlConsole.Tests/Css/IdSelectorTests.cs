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
            Assert.AreEqual(true, selector.Match(node));
        }

        [TestMethod]
        public void Match_NotMatchingId_ReturnsFalse()
        {
            var node = new ElementNode {Element = "a", Id = "ida"};
            var selector = new IdSelector {Id = "idb"};
            Assert.AreEqual(false, selector.Match(node));
        }
    }
}
