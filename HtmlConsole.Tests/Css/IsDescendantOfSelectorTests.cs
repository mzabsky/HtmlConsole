using System.Collections.Generic;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using HtmlConsole.Tests.Testing.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class IsDescendantOfSelectorTests
    {
        [TestMethod]
        public void Match_MatchingDescendant_ReturnsTrue()
        {
            var descendant = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { new ElementNode { Element = "span", Children = new List<INode> { descendant } } } };
            node.FixParents();

            var selector = new IsDescendantOfSelector(new ElementSelector("div"));

            var selectorMatch = selector.Match(descendant);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(1, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_NotMatchingDescendant_ReturnsFalse()
        {
            var descendant = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { new ElementNode { Element = "span", Children = new List<INode> { descendant } } } };
            node.FixParents();

            var selector = new IsDescendantOfSelector(new ElementSelector("table"));

            var selectorMatch = selector.Match(descendant);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_MatchingSelfNotParent_ReturnsFalse()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { child } };
            node.FixParents();

            var selector = new IsDescendantOfSelector(new ElementSelector("a"));

            var selectorMatch = selector.Match(child);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }
    }
}
