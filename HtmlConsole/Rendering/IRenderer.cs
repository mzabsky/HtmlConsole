using System.Collections.Generic;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public interface IRenderer
    {
        INode DomNode { get; }
        IRenderer Parent { get; set; }
        List<IRenderer> Children { get; set; }
    }
}