using HtmlConsole.Dom;
using HtmlConsole.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Dom
{
    [TestClass]
    public class ElementNodeTests
    {
        [TestMethod]
        public void CreateRenderer_UnspecifiedDisplay_CreatesInlineRenderer()
        {
            var document = Document.ParseHtml("<xyz/>");
            document.ComputeStyles();
            var renderer = document.RootNode.CreateRenderer();
            Assert.AreEqual(typeof(InlineRenderer), renderer.GetType());
        }

        [TestMethod]
        public void CreateRenderer_DisplayInline_CreatesInlineRenderer()
        {            
            var document = Document.ParseHtml("<xyz/>");
            document.AddStylesheet("xyz { display:inline; }");
            document.ComputeStyles();
            var renderer = document.RootNode.CreateRenderer();
            Assert.AreEqual(typeof(InlineRenderer), renderer.GetType());
        }

        [TestMethod]
        public void CreateRenderer_DisplayBlock_CreatesBlockRenderer()
        {            
            var document = Document.ParseHtml("<xyz/>");
            document.AddStylesheet("xyz { display:block; }");
            document.ComputeStyles();
            var renderer = document.RootNode.CreateRenderer();
            Assert.AreEqual(typeof(BlockRenderer), renderer.GetType());
        }

        [TestMethod]
        public void CreateRenderer_DisplayNone_CreatesVoidRenderer()
        {            
            var document = Document.ParseHtml("<xyz/>");
            document.AddStylesheet("xyz { display:none; }");
            document.ComputeStyles();
            var renderer = document.RootNode.CreateRenderer();
            Assert.AreEqual(typeof(VoidRenderer), renderer.GetType());
        }

        [TestMethod]
        public void CreateRenderer_NestedElements_CreatesNestedRenderers()
        {            
            var document = Document.ParseHtml("<xyz><x/><y/><z/></xyz>");
            document.ComputeStyles();
            var renderer = document.RootNode.CreateRenderer();
            Assert.AreEqual(3, renderer.Children.Count);
        }
    }
}
