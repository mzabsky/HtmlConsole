using System;
using HtmlConsole.Dom;
using HtmlConsole.Rendering;
using HtmlConsole.Tests.Testing.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Rendering
{
    [TestClass]
    public class RenderViewTests
    {
        private string GetRendererLayoutString(RenderView view)
        {
            return view.RootRenderer.GetRendererString(p => $"{p.Position} {p.ClientSize}");
        }

        private void TestLayout(string html, string css, string expected)
        {
            var document = Document.ParseHtml(html);
            document.AddStylesheet(css);
            document.ComputeStyles();
            var view = new RenderView(document);
            view.Layout(new Size(10, 10));

            Assert.AreEqual(expected, GetRendererLayoutString(view));
        }

        [TestMethod]
        public void Construct_SimpleDocument_CreatesAppropriateView()
        {
            var document = Document.ParseHtml("<xyz/>");
            document.ComputeStyles();
            var view = new RenderView(document);

            Assert.AreEqual(document, view.Document);
            Assert.AreEqual(typeof(InlineRenderer), view.RootRenderer.GetType());
        }
        
        [TestMethod]
        public void Layout_SequenceOfInlines_LayoutsOneAfterOther()
        {
            TestLayout("<xyz><a>aa</a>t<b>bb</b>t<c>cc</c></xyz>", "", 
@"InlineRenderer - [0, 0] [8, 1]
    InlineRenderer - [0, 0] [2, 1]
        TextRenderer - [0, 0] [2, 1]
    TextRenderer - [2, 0] [1, 1]
    InlineRenderer - [3, 0] [2, 1]
        TextRenderer - [3, 0] [2, 1]
    TextRenderer - [5, 0] [1, 1]
    InlineRenderer - [6, 0] [2, 1]
        TextRenderer - [6, 0] [2, 1]
");
        }

        [TestMethod]
        public void Layout_SequenceOfBlocks_LayoutsOneAfterOther()
        {
            TestLayout("<xyz><a>aa</a><b><ba>ba</ba><bb>bb</bb></b><c>cc</c></xyz>", "xyz, a, b, ba, bb, c {display: block}",
@"BlockRenderer - [0, 0] [10, 4]
    BlockRenderer - [0, 0] [10, 1]
        TextRenderer - [0, 0] [2, 1]
    BlockRenderer - [0, 1] [10, 2]
        BlockRenderer - [0, 1] [10, 1]
            TextRenderer - [0, 1] [2, 1]
        BlockRenderer - [0, 2] [10, 1]
            TextRenderer - [0, 2] [2, 1]
    BlockRenderer - [0, 3] [10, 1]
        TextRenderer - [0, 3] [2, 1]
");
        }

        [TestMethod]
        public void Paint_InlineWithText_RendersJustText()
        {
            var document = Document.ParseHtml("<xyz>Hello world!<xyz/>");
            document.ComputeStyles();
            var view = new RenderView(document);

            var layer = new VisualLayer(new Size(12, 1));
            view.Paint(layer);
            Assert.AreEqual("Hello world!" + Environment.NewLine, layer.GetText());
        }

        [TestMethod]
        public void Paint_Color_SetsCorrectColors()
        {
            var document = Document.ParseHtml("<xyz><a>aa</a>t<b>bb</b>t<c>cc</c></xyz>");
            document.AddStylesheet("xyz { color: black} a {color:red} b {color: lime} c {color: blue}");
            document.ComputeStyles();
            var view = new RenderView(document);

            var layer = new VisualLayer(new Size(9, 1));
            view.Layout(new Size(0, 0));
            view.Paint(layer);
            
            Assert.AreEqual("#FF0000", layer.GetColor(new Position(0, 0)).ToString());
            Assert.AreEqual("#FF0000", layer.GetColor(new Position(1, 0)).ToString());
            Assert.AreEqual("#000000", layer.GetColor(new Position(2, 0)).ToString());
            Assert.AreEqual("#00FF00", layer.GetColor(new Position(3, 0)).ToString());
            Assert.AreEqual("#00FF00", layer.GetColor(new Position(4, 0)).ToString());
            Assert.AreEqual("#000000", layer.GetColor(new Position(5, 0)).ToString());
            Assert.AreEqual("#0000FF", layer.GetColor(new Position(6, 0)).ToString());
            Assert.AreEqual("#0000FF", layer.GetColor(new Position(7, 0)).ToString());
        }
    }
}
