using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlConsole.Dom
{
    public interface INode : IEquatable<INode>
    {
        INode Parent { get; set; }
        IEnumerable<INode> Children { get; }
    }
}
