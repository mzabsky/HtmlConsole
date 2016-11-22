using System.Collections.Generic;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    /// <summary>
    /// A node in the rendering tree, based in the <see cref="RenderView"/>.
    /// </summary>
    public abstract class Renderer : IRenderer
    {
        public INode DomNode { get; }
        public IRenderer Parent { get; set; }
        public List<IRenderer> Children { get; set; } = new List<IRenderer>();

        // reference na renderview?
        // aspect ratio elements?
        // Continuations?
        // Anonymous blocks
        // enclosing box (utility only )

        protected Renderer(INode domNode, IRenderer parent = null)
        {
            Parent = parent;
            DomNode = domNode;
        }
    }
}
