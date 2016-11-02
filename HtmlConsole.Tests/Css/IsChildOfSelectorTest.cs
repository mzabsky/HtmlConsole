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
            Assert.AreEqual(true, selector.Match(child));
        }

        [TestMethod]
        public void Match_NotMatchingParent_ReturnsFalse()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { child } };
            node.FixParents();

            var selector = new IsChildOfSelector { SubSelector = new ElementSelector { ElementName = "span" } };
            Assert.AreEqual(false, selector.Match(child));
        }

        [TestMethod]
        public void Match_MatchingAncestorNotParent_ReturnsFalse()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { new ElementNode { Element = "span", Children = new List<INode> { child } } } };
            node.FixParents();

            var selector = new IsChildOfSelector { SubSelector = new ElementSelector { ElementName = "div" } };
            Assert.AreEqual(false, selector.Match(child));
        }

        [TestMethod]
        public void Match_MatchingSelfNotParent_ReturnsFalse()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { child } };
            node.FixParents();

            var selector = new IsChildOfSelector { SubSelector = new ElementSelector { ElementName = "a" } };
            Assert.AreEqual(false, selector.Match(child));
        }
    }
}
