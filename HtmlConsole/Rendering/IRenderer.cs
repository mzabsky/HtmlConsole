using System.Collections.Generic;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public interface IRenderer
    {
        Position Position { get; }
        Size ClientSize { get; }
        Size MinimumSize { get; }
        Size MaximumSize { get; }

        INode DomNode { get; }
        IRenderer Parent { get; set; }
        List<IRenderer> Children { get; set; }

        bool IsBlock { get; }
        bool IsInline { get; }

        void Layout(LayoutContext context);
        void Paint(VisualLayer target);

        IRenderer Clone();
    }
}