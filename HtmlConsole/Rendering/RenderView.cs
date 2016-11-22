using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class RenderView
    {
        public Document Document { get; }
        public Renderer RootRenderer { get; }

        public RenderView(Document document)
        {
            Document = document;
            RootRenderer = document.RootNode.CreateRenderer();
        }
    }
}