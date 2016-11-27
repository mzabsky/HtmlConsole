using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class ReplacedContentRenderer : BlockRenderer
    {
        public ReplacedContentRenderer(ElementNode domNode) : base(domNode)
        {
        }

        public override IRenderer Clone()
        {
            return new ReplacedContentRenderer((ElementNode)DomNode);
        }
    }
}