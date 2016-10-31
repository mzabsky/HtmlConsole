using System;
using System.Collections.Generic;

namespace HtmlConsole.Dom
{
    public interface INode : IEquatable<INode>
    {
        TagNode Parent { get; set; }
        IEnumerable<INode> Children { get; }
    }
}
