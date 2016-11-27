using System;
using System.Text;
using HtmlConsole.Dom;
using HtmlConsole.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Dom
{
    [TestClass]
    public class ElementNodeTests
    {
        private string GetRendererString(IRenderer renderer)
        {
            StringBuilder sb = new StringBuilder();
            GetRendererString(renderer, sb, 0);
            return sb.ToString();
        }

        private void GetRendererString(IRenderer renderer, StringBuilder sb, int level)
        {
            /*switch (renderer.DomNode)
            {
                case ElementNode elementNode2:
                    break;
            }*/

            string domNodeString;
            if (renderer.DomNode is ElementNode elementNode)
            {
                domNodeString = $" - {elementNode.Element}";
            }
            else if (renderer.DomNode is TextNode textNode)
            {
                domNodeString = $" - {textNode.Text}";
            }
            else
            {
                domNodeString = "";
            }

            sb.AppendLine($"{new string(' ', level*4)}{renderer.GetType().Name}{domNodeString}");
            foreach (var child in renderer.Children)
            {
                GetRendererString(child, sb, level + 1);
            }
        }

        private void TestCreateRenderer(string html, string expectedRendererTree)
        {
            TestCreateRenderer(html, null, expectedRendererTree);
        }

        private void TestCreateRenderer(string html, string stylesheet, string expectedRendererTree)
        {
            var document = Document.ParseHtml(html);
            if(stylesheet != null) document.AddStylesheet(stylesheet);
            document.ComputeStyles();
            var renderer = document.RootNode.CreateRenderer();
            Assert.AreEqual(expectedRendererTree, GetRendererString(renderer).Trim());
        }

        // TODO: Document crashes when ParseHtml empty string
        /*[TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateRenderer_StylesNotYetCalculated_ThrowsException()
        {
            var document = Document.ParseHtml("");
            document.RootNode.CreateRenderer();
        }*/

        [TestMethod]
        public void CreateRenderer_UnspecifiedDisplay_CreatesInlineRenderer()
        {
            TestCreateRenderer("<xyz/>", @"InlineRenderer - xyz");
        }

        [TestMethod]
        public void CreateRenderer_DisplayInline_CreatesInlineRenderer()
        {            
            TestCreateRenderer("<xyz/>", "xyz {display: inline}", @"InlineRenderer - xyz");
        }

        [TestMethod]
        public void CreateRenderer_DisplayBlock_CreatesBlockRenderer()
        {
            TestCreateRenderer("<xyz/>", "xyz {display: block}", @"BlockRenderer - xyz");
        }

        [TestMethod]
        public void CreateRenderer_DisplayNone_CreatesVoidRenderer()
        {
            TestCreateRenderer("<xyz/>", "xyz {display: none}", @"VoidRenderer - xyz");
        }

        [TestMethod]
        public void CreateRenderer_SimpleNestedElements_CreatesNestedRenderers()
        {
            TestCreateRenderer("<xyz><x/><y/><z/></xyz>", @"InlineRenderer - xyz
    InlineRenderer - x
    InlineRenderer - y
    InlineRenderer - z");
        }

        [TestMethod]
        public void CreateRenderer_BlockWithNestedInlines_CreatesNestedRenderers()
        {
            TestCreateRenderer("<xyz><x/><y/><z/></xyz>", "xyz {display: block}", @"BlockRenderer - xyz
    InlineRenderer - x
    InlineRenderer - y
    InlineRenderer - z");
        }

        [TestMethod]
        public void CreateRenderer_BlockWithNestedInlinesAndABlock_WrapsNestedInlines()
        {
            TestCreateRenderer("<xyz><x/><y/><z/></xyz>", "xyz, y {display: block}", @"BlockRenderer - xyz
    AnonymousBlockRenderer
        InlineRenderer - x
    BlockRenderer - y
    AnonymousBlockRenderer
        InlineRenderer - z");
        }

        [TestMethod]
        public void CreateRenderer_InlineWithNestedInlinesAndABlock_SplitsAndGetsWrapped()
        {
            TestCreateRenderer("<xyz><x/><y/><z/></xyz>", "y {display: block}", @"AnonymousBlockRenderer
    AnonymousBlockRenderer
        InlineRenderer - xyz
            InlineRenderer - x
    BlockRenderer - y
    AnonymousBlockRenderer
        InlineRenderer - xyz
            InlineRenderer - z");
        }

        [TestMethod]
        public void CreateRenderer_InlineWithNestedAdjacentInlinesAndABlock_SplitsAndGetsWrappedAdjacentInlinesMerge()
        {
            TestCreateRenderer("<xyz><v/><w/><x/><y/><z/></xyz>", "y {display: block}", @"AnonymousBlockRenderer
    AnonymousBlockRenderer
        InlineRenderer - xyz
            InlineRenderer - v
            InlineRenderer - w
            InlineRenderer - x
    BlockRenderer - y
    AnonymousBlockRenderer
        InlineRenderer - xyz
            InlineRenderer - z");
        }
    }
}
