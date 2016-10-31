﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using HtmlAgilityPack;
using HtmlConsole.Css;

namespace HtmlConsole.Dom
{
    public class TagNode : INode
    {
        public string Tag { get; set; }
        public string Id { get; set; }
        public string[] Classes { get; set; } = new string[0];
        // TODO: Stylesheet
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        public INode Parent { get; set; }
        public IEnumerable<INode> Children { get; set; } = new INode[0];

        public Dictionary<string, StyleValue> Styles { get; set; } = new Dictionary<string, StyleValue>();

        public TagNode()
        {
        }

        public TagNode(HtmlNode htmlNode, INode parent)
        {
            Tag = htmlNode.Name.ToLowerInvariant();
            Id = htmlNode.Attributes?["id"]?.Value.ToLowerInvariant();

            var classesString = htmlNode.Attributes?["class"]?.Value.ToLowerInvariant();
            Classes = classesString?.Split(' ') ?? new string[0];

            Attributes = htmlNode.Attributes?.ToDictionary(p => p.Name.ToLower(), p => p.Value) 
                ?? new Dictionary<string, string>();

            Parent = parent;
            Children = htmlNode.ChildNodes.Select(p => ParseNode(p, this)).ToList();
        }

        public static INode ParseNode(HtmlNode xmlNode, INode parent = null)
        {
            var text = xmlNode as HtmlTextNode;
            if (text != null)
            {
                return new TextNode(text, parent);
            }
            else
            {
                return new TagNode(xmlNode, parent);
            }
        }

        public bool Equals(INode other)
        {
            if (other == null) return false;

            if (GetType() != other.GetType()) return false;

            var tagNode = (TagNode) other;

            return
                Tag == tagNode.Tag &&
                Id == tagNode.Id &&
                Classes.SequenceEqual(tagNode.Classes) &&
                Attributes.SequenceEqual(tagNode.Attributes) &&
                Children.SequenceEqual(tagNode.Children);
        }
    }
}
