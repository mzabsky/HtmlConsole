using System;
using HtmlConsole.Dom;
using HtmlConsole.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Rendering
{
    [TestClass]
    public class RenderViewTests
    {
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
            var document = Document.ParseHtml("<xyz><a>aa</a>t<b>bb</b>t<c>cc</c><xyz/>");
            document.AddStylesheet("xyz { color: black} a {color:red} b {color: lime} c {color: blue}");
            document.ComputeStyles();
            var view = new RenderView(document);

            var layer = new VisualLayer(new Size(9, 1));
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
