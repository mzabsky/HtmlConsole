using System.Collections.Generic;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using HtmlConsole.Tests.Testing.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class IsChildOfSelectorTests
    {
        [TestMethod]
        public void Match_MatchingParent_ReturnsTrue()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode {Element = "div", Children = new List<INode> { child } };
            node.FixParents();

            var selector = new IsChildOfSelector { SubSelector = new ElementSelector {ElementName = "div"}};

            var selectorMatch = selector.Match(child);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(1, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_NotMatchingParent_ReturnsFalse()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { child } };
            node.FixParents();

            var selector = new IsChildOfSelector { SubSelector = new ElementSelector { ElementName = "span" } };

            var selectorMatch = selector.Match(child);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_MatchingAncestorNotParent_ReturnsFalse()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { new ElementNode { Element = "span", Children = new List<INode> { child } } } };
            node.FixParents();

            var selector = new IsChildOfSelector { SubSelector = new ElementSelector { ElementName = "div" } };

            var selectorMatch = selector.Match(child);
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

            var selector = new IsChildOfSelector { SubSelector = new ElementSelector { ElementName = "a" } };

            var selectorMatch = selector.Match(child);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }
    }
}
