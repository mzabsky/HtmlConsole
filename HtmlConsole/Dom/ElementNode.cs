using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlConsole.Css;
using HtmlConsole.Rendering;

namespace HtmlConsole.Dom
{
    public class ElementNode : INode
    {
        public string Element { get; set; }
        public string Id { get; set; }
        public string[] Classes { get; set; } = new string[0];

        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        public ElementNode Parent { get; set; }
        public IEnumerable<INode> Children { get; set; } = new INode[0];
        public Document Document { get; set; }

        public DeclarationSet Styles { get; set; }

        public ElementNode()
        {
        }

        public ElementNode(HtmlNode htmlNode, ElementNode parent)
        {
            Element = htmlNode.Name.ToLowerInvariant();
            Id = htmlNode.Attributes?["id"]?.Value.ToLowerInvariant();

            var classesString = htmlNode.Attributes?["class"]?.Value.ToLowerInvariant();
            Classes = classesString?.Split(' ') ?? new string[0];

            Attributes = htmlNode.Attributes?.ToDictionary(p => p.Name.ToLower(), p => p.Value) 
                ?? new Dictionary<string, string>();

            Parent = parent;
            Children = htmlNode.ChildNodes.Select(p => ParseNode(p, this)).ToList();
        }

        public static INode ParseNode(HtmlNode xmlNode, ElementNode parent = null)
        {
            var text = xmlNode as HtmlTextNode;
            if (text != null)
            {
                return new TextNode(text, parent);
            }
            else
            {
                return new ElementNode(xmlNode, parent);
            }
        }

        Renderer INode.CreateRenderer(Renderer parent)
        {
            if(Styles == null) throw new InvalidOperationException("Styles are not computed yet (call ComputeStyles on the Document first).");

            Renderer renderer;
            switch (GetStyleValue<EnumStyleValue<Display>>("display")?.EnumValue)
            {
                // TODO: Replaced content renderer
                case Display.Block:
                    renderer = new BlockRenderer(this, parent);
                    break;
                case Display.None:
                    renderer = new VoidRenderer(this, parent);
                    break;
                case Display.Inline:
                default: // Inline is the default
                    renderer = new InlineRenderer(this, parent);
                    break;
            }

            foreach (var child in Children)
            {
                renderer.Children.Add(child.CreateRenderer(renderer));
            }

            return renderer;
        }

        public StyleValue GetStyleValue(string name)
        {
            // TODO: Evaluate InheritStyleValue and InitialStyleValue here?

            return Styles?[name]?.Value;
        }

        public T GetStyleValue<T>(string name) where T: StyleValue
        {
            return GetStyleValue(name) as T;
        }

        public bool Equals(INode other)
        {
            if (other == null) return false;

            if (GetType() != other.GetType()) return false;

            var tagNode = (ElementNode) other;

            return
                Element == tagNode.Element &&
                Id == tagNode.Id &&
                Classes.SequenceEqual(tagNode.Classes) &&
                Attributes.SequenceEqual(tagNode.Attributes) &&
                Children.SequenceEqual(tagNode.Children);
        }
    }
}
