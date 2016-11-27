using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlConsole.Css;
using HtmlConsole.Extensions;
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

        internal ElementNode(HtmlNode htmlNode, ElementNode parent)
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

        internal static INode ParseNode(HtmlNode xmlNode, ElementNode parent = null)
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

        IRenderer INode.CreateRenderer()
        {
            if(Styles == null) throw new InvalidOperationException("Styles are not computed yet (call ComputeStyles on the Document first).");

            var childRenderers = Children.Select(p => p.CreateRenderer()).ToList();
            var hasBlockChildren = childRenderers.Any(p => p.IsBlock);

            Renderer selfRenderer;
            switch (GetStyleValue<EnumStyleValue<Display>>(StyleProperties.Display)?.EnumValue)
            {
                // TODO: Replaced content renderer
                case Display.Block:
                    selfRenderer = new BlockRenderer(this);
                    break;
                case Display.None:
                    selfRenderer = new VoidRenderer(this);
                    break;
                case Display.Inline:
                default: // Inline is the default
                    selfRenderer = new InlineRenderer(this);
                    break;
            }

            IRenderer actualRenderer;
            if (selfRenderer.IsInline && hasBlockChildren)
            {
                // Inline renderers can't have block children -> wrap the whole thing in a block renderer and split the inline block so that it doesn't contain any block children
                actualRenderer = new AnonymousBlockRenderer();
            }
            else
            {
                actualRenderer = selfRenderer;
            }
            
            List<IRenderer> actualChildRenderers = new List<IRenderer>();
            var childRendererSegments = childRenderers.Segmentize(p => p.IsInline);
            foreach (var segment in childRendererSegments)
            {
                if (childRendererSegments.Count > 1 && segment.Any(p => p.IsInline))
                {
                    // A continuous segment of inline children on the same levels of non-inline children
                    // Wrapping in an anonymous block is required
                    
                    var anonymousBlock = new AnonymousBlockRenderer();

                    // If the current box is an inline, it needs to be split around its block children
                    if (selfRenderer.IsInline)
                    {
                        var inlineCopy = selfRenderer.Clone();
                        foreach (var childInline in segment)
                        {
                            inlineCopy.Children.Add(childInline);
                        }
                        anonymousBlock.Children.Add(inlineCopy);
                    }
                    else
                    {
                        foreach (var childInline in segment)
                        {
                            anonymousBlock.Children.Add(childInline);
                        }
                    }

                    actualChildRenderers.Add(anonymousBlock);
                }
                else
                {
                    // This is either a block segment or a segment of all the parent renderer's inline children
                    // -> wrapping is not required
                    actualChildRenderers.AddRange(segment);   
                }
            }

            actualRenderer.Children = actualChildRenderers;

            return actualRenderer;
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

        public T GetStyleValue<T>(StyleProperty property) where T: StyleValue
        {
            return GetStyleValue<T>(property.PropertyName);
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
