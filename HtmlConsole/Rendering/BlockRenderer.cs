using System.Linq;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class BlockRenderer : ElementRenderer
    {
        public override bool IsBlock => true;
        public override bool IsInline => false;

        public BlockRenderer(ElementNode domNode) : base(domNode)
        {
        }

        public override void Paint(VisualLayer target)
        {
            foreach (var child in Children)
            {
                child.Paint(target);
            }
        }

        public override void Layout(LayoutContext context)
        {
            Position = context.Position;
            
            if (Children.FirstOrDefault()?.IsInline ?? false)
            {
                // If the first child is inline, all the children are supposed to be inline
                // TODO: Line breaking

                Position = context.Position;

                var currentWidth = 0;
                var maximumHeight = 0;

                // Assuming all children are inlines here
                foreach (var child in Children)
                {
                    child.Layout(new LayoutContext
                    {
                        Position = context.Position + new Position(currentWidth, 0),
                        Size = context.Size
                    });

                    currentWidth += child.ClientSize.Width;

                    if (child.ClientSize.Height > maximumHeight)
                    {
                        maximumHeight = child.ClientSize.Height;
                    }
                }

                ClientSize = new Size(context.Size.Width, maximumHeight);
            }
            else
            {
                var currentHeight = 0;
                var maximumHeight = 0;

                // Otherwise all the children are assumed to be blocks
                foreach (var child in Children)
                {
                    child.Layout(new LayoutContext
                    {
                        Position = context.Position + new Position(0, currentHeight),
                        Size = context.Size
                    });

                    currentHeight += child.ClientSize.Height;

                    /*if (child.ClientSize.Height > maximumHeight)
                    {
                        maximumHeight = child.ClientSize.Height;
                    }*/
                }

                ClientSize = new Size(context.Size.Width, currentHeight);
            }
        }

        public override IRenderer Clone()
        {
            return new BlockRenderer((ElementNode)DomNode);
        }
    }
}