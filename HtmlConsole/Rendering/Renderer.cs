using System.CodeDom;
using System.Collections.Generic;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    /// <summary>
    /// A node in the rendering tree, based in the <see cref="RenderView"/>.
    /// </summary>
    public abstract class Renderer : IRenderer
    {
        public Position Position { get; protected set; }
        public Size ClientSize { get; protected set; }
        public Size MinimumSize { get; protected set; }
        public Size MaximumSize { get; protected set; }

        public INode DomNode { get; }
        public IRenderer Parent { get; set; }
        public List<IRenderer> Children { get; set; } = new List<IRenderer>();
        public abstract bool IsBlock { get; }
        public abstract bool IsInline { get; }

        // reference na renderview?
        // aspect ratio elements?
        // Continuations?
        // Anonymous blocks
        // enclosing box (utility only )

        protected Renderer(INode domNode)
        {
            DomNode = domNode;
        }

        public abstract void Paint(VisualLayer target);
        public abstract IRenderer Clone();
        public virtual void Layout(LayoutContext context) // TODO: Abstract
        {
            throw new System.NotImplementedException();
        }
    }
}
