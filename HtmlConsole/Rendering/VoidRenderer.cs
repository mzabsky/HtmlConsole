using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class VoidRenderer : Renderer
    {
        public override bool IsBlock => false;
        public override bool IsInline => false;

        public VoidRenderer(ElementNode domNode) : base(domNode)
        {
        }

        public override IRenderer Clone()
        {
            return new VoidRenderer((ElementNode)DomNode);
        }
    }
}