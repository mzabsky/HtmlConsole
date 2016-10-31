using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Dom
{
    public static class NodeExtensions
    {
        public static IEnumerable<INode> GetSiblings(this INode node)
        {
            if (node == null) throw new ArgumentNullException();

            if (node.Parent == null)
            {
                return new INode[0];
            }

            return node.Parent.Children.Where(p => p != node);
        }

        public static IEnumerable<INode> GetAncestors(this INode node)
        {
            if(node == null) throw new ArgumentNullException();

            var currentNode = node.Parent;
            while (currentNode != null)
            {
                yield return currentNode;

                currentNode = currentNode.Parent;
            }
            ;
        }

        public static IEnumerable<INode> GetAllNodes(this INode node)
        {
            if (node == null) throw new ArgumentNullException();

            yield return node;

            foreach (var child in node.Children)
            {
                foreach (var childNode in child.GetAllNodes())
                {
                    yield return childNode;
                }
            }
        }
    }
}
