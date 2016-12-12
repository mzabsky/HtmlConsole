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
    }
}
