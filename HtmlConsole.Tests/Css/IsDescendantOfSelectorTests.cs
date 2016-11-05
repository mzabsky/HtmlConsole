﻿using System.Collections.Generic;
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

            var selector = new IsDescendantOfSelector { SubSelector = new ElementSelector {ElementName = "div"}};
            Assert.AreEqual(true, selector.Match(descendant));
        }

        [TestMethod]
        public void Match_NotMatchingDescendant_ReturnsFalse()
        {
            var descendant = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { new ElementNode { Element = "span", Children = new List<INode> { descendant } } } };
            node.FixParents();

            var selector = new IsDescendantOfSelector { SubSelector = new ElementSelector { ElementName = "table" } };
            Assert.AreEqual(false, selector.Match(descendant));
        }

        [TestMethod]
        public void Match_MatchingSelfNotParent_ReturnsFalse()
        {
            var child = new ElementNode { Element = "a" };
            var node = new ElementNode { Element = "div", Children = new List<INode> { child } };
            node.FixParents();

            var selector = new IsDescendantOfSelector { SubSelector = new ElementSelector { ElementName = "a" } };
            Assert.AreEqual(false, selector.Match(child));
        }
    }
}