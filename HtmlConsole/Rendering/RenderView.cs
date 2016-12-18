using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class RenderView
    {
        public Document Document { get; }
        public IRenderer RootRenderer { get; }

        public RenderView(Document document)
        {
            Document = document;
            RootRenderer = document.RootNode.CreateRenderer();
        }

        public void Layout(Size viewportSize)
        {
            RootRenderer.Layout(new LayoutContext
            {
                Position = new Position(0, 0),
                Size = viewportSize
            });
        }

        public void Paint(VisualLayer target)
        {
            RootRenderer?.Paint(target);
        }
    }
}