using System.Collections.Generic;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class AndSelectorTests
    {
        [TestMethod]
        public void Match_AllMatching_ReturnsTrue()
        {
            var node = new ElementNode();
            var selector = new AndSelector
            {
                Children = new List<Selector>
                {
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity(1, 2, 3) },
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity(1, 0, 0) },
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity(0, 1, 1) }
                }
            };

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(2, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(3, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(4, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_OneNotMatching_ReturnsFalse()
        {
            var node = new ElementNode();
            var selector = new AndSelector
            {
                Children = new List<Selector>
                {
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity(1, 2, 3) },
                    new ConstantSelector { IsSuccess = false },
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity(1, 2, 3) }
                }
            };
            var selectorMatch = selector.Match(node);
            Assert.AreEqual(false, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }
    }
}
