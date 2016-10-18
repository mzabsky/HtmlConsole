using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlConsole.Dom
{
    public interface INode : IEquatable<INode>
    {
        IEnumerable<INode> Children { get; }
    }
}
