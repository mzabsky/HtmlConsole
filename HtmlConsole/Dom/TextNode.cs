using System.Collections.Generic;
using System.Xml;
using HtmlAgilityPack;
using HtmlConsole.Rendering;

namespace HtmlConsole.Dom
{
    public class TextNode : INode
    {
        public string Text { get; set; }
        public ElementNode Parent { get; set; }
        public IEnumerable<INode> Children { get; } = new INode[0];
        public Document Document { get; set; }
        public IRenderer Renderer { get; set; }

        public TextNode(string text)
        {
            Text = text;
        }

        internal TextNode(HtmlTextNode xmlNode, ElementNode parent)
        {
            Text = xmlNode.InnerText;
            Parent = parent;
        }

        public bool Equals(INode other)
        {
            if (other == null) return false;

            if (GetType() != other.GetType()) return false;

            var textNode = (TextNode)other;
            return Text == textNode.Text;
        }
        
        public IRenderer CreateRenderer()
        {
            return new TextRenderer(this);
        }
    }
}
