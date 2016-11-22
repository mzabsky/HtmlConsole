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

    public class ElementRenderer : Renderer
    {
        public ElementRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }

    public class InlineRenderer : ElementRenderer
    {
        public InlineRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }

    public class BlockRenderer : ElementRenderer
    {
        public BlockRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }

    public class TextRenderer : Renderer
    {
        public TextRenderer(TextNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }

    public class ReplacedContentRenderer : Renderer
    {
        public ReplacedContentRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }

    public class VoidRenderer : Renderer
    {
        public VoidRenderer(ElementNode domNode, IRenderer parent = null) : base(domNode, parent)
        {
        }
    }

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
