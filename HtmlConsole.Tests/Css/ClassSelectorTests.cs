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

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(1, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_NotMatchingId_ReturnsFalse()
        {
            var node = new ElementNode {Element = "a", Classes = new[] { "ca", "cb" }};
            var selector = new ClassSelector {Class = "cc"};

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }
    }
}
