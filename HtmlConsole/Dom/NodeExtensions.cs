using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Dom
{
    public static class NodeExtensions
    {
        public static IEnumerable<INode> GetSiblings(this INode node)
        {
            if (node.Parent == null)
            {
                return new INode[0];
            }

            return node.Parent.Children.Where(p => p != node);
        }
    }
}
