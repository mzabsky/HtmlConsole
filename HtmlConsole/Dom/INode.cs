using System;
using System.Collections.Generic;

namespace HtmlConsole.Dom
{
    public interface INode : IEquatable<INode>
    {
        ElementNode Parent { get; set; }
        IEnumerable<INode> Children { get; }
        Document Document { get; set; }
    }
}
