using System.Collections.Generic;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class OrSelectorTests
    {
        [TestMethod]
        public void Match_AllMatching_ReturnsTrue()
        {
            var node = new ElementNode();
            var selector = new OrSelector
            {
                Children = new List<Selector>
                {
                    new ConstantSelector { Value = true },
                    new ConstantSelector { Value = true },
                    new ConstantSelector { Value = true }
                }
            };
            Assert.AreEqual(true, selector.Match(node));
        }
        
        [TestMethod]
        public void Match_OneNotMatching_ReturnsTrue()
        {
            var node = new ElementNode();
            var selector = new OrSelector
            {
                Children = new List<Selector>
                {
                    new ConstantSelector { Value = true },
                    new ConstantSelector { Value = false },
                    new ConstantSelector { Value = true }
                }
            };
            Assert.AreEqual(true, selector.Match(node));
        }

        [TestMethod]
        public void Match_AllNotMatching_ReturnsFalse()
        {
            var node = new ElementNode();
            var selector = new OrSelector
            {
                Children = new List<Selector>
                {
                    new ConstantSelector { Value = false },
                    new ConstantSelector { Value = false },
                    new ConstantSelector { Value = false }
                }
            };
            Assert.AreEqual(false, selector.Match(node));
        }
    }
}
