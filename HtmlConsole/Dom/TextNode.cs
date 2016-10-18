﻿using System.Collections.Generic;
using System.Xml;
using HtmlAgilityPack;

namespace HtmlConsole.Dom
{
    public class TextNode : INode
    {
        public string Text { get; set; }
        public IEnumerable<INode> Children { get; } = new INode[0];

        public TextNode(string text)
        {
            Text = text;
        }

        public TextNode(HtmlTextNode xmlNode)
        {
            Text = xmlNode.InnerText;
        }

        public bool Equals(INode other)
        {
            if (other == null) return false;

            if (GetType() != other.GetType()) return false;

            var textNode = (TextNode)other;
            return Text == textNode.Text;
        }
    }
}