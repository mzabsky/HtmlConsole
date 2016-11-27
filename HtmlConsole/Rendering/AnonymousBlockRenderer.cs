using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class AnonymousBlockRenderer : ElementRenderer
    {
        public override bool IsBlock => true;
        public override bool IsInline => false;

        public AnonymousBlockRenderer() : base(null)
        {
        }

        public override IRenderer Clone()
        {
            return new AnonymousBlockRenderer();
        }
    }
}