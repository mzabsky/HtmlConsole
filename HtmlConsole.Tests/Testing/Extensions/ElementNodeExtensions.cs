using HtmlConsole.Dom;

namespace HtmlConsole.Tests.Testing.Extensions
{
    public static class ElementNodeExtensions
    {
        /// <summary>
        /// Fixes pointers to parents for all subnodes of an node. For testing purposes only, correctly constructed DOM should already have parents filled.
        /// </summary>
        /// <param name="node">The node.</param>
        public static void FixParents(this ElementNode node)
        {
            foreach (var child in node.Children)
            {
                child.Parent = node;

                var elementNode = child as ElementNode;
                if (elementNode != null)
                {
                    FixParents(elementNode);
                }
            }
        }
    }
}
