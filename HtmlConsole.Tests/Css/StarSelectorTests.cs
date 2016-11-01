using System.Collections.Generic;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class StarSelectorTests
    {
        [TestMethod]
        public void Match_Always_ReturnsTrue()
        {
            var node = new ElementNode();
            var selector = new StarSelector();
            Assert.AreEqual(true, selector.Match(node));
        }
    }
}
