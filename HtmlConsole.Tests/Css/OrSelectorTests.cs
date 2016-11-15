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
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity { ClassSpecificity = 3, ElementSpecificity = 0, IdSpecificity = 0 } },
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity { ClassSpecificity = 0, ElementSpecificity = 3, IdSpecificity = 0 } },
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity { ClassSpecificity = 0, ElementSpecificity = 0, IdSpecificity = 3 } }
                }
            };

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(3, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }
        
        [TestMethod]
        public void Match_OneNotMatching_ReturnsTrue()
        {
            var node = new ElementNode();
            var selector = new OrSelector
            {
                Children = new List<Selector>
                {
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity { ClassSpecificity = 0, ElementSpecificity = 3, IdSpecificity = 0 } },
                    new ConstantSelector { IsSuccess = false },
                    new ConstantSelector { IsSuccess = true, Specificity = new Specificity { ClassSpecificity = 3, ElementSpecificity = 0, IdSpecificity = 0 } }
                }
            };

            var selectorMatch = selector.Match(node);
            Assert.AreEqual(true, selectorMatch.IsSuccess);
            Assert.AreEqual(0, selectorMatch.Specificity.IdSpecificity);
            Assert.AreEqual(3, selectorMatch.Specificity.ClassSpecificity);
            Assert.AreEqual(0, selectorMatch.Specificity.ElementSpecificity);
        }

        [TestMethod]
        public void Match_AllNotMatching_ReturnsFalse()
        {
            var node = new ElementNode();
            var selector = new OrSelector
            {
                Children = new List<Selector>
                {
                    new ConstantSelector { IsSuccess = false },
                    new ConstantSelector { IsSuccess = false },
                    new ConstantSelector { IsSuccess = false }
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
