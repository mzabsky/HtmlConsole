﻿using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlConsole.Css;
using HtmlConsole.Rendering;

namespace HtmlConsole.Dom
{
    public class Document
    {
        public StyleParser StyleParser { get; set; }
        public INode RootNode { get; set; }

        public List<Stylesheet> Stylesheets { get; set; } = new List<Stylesheet>();

        public static Document ParseHtml(string html, StyleParser styleParser = null)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var firstChild = htmlDocument.DocumentNode.FirstChild;
            var rootNode = firstChild != null ? ElementNode.ParseNode(firstChild) : null;

            var document = new Document
            {
                RootNode = rootNode,
                StyleParser = styleParser ?? new StyleParser()
            };

            if (rootNode != null)
            {
                foreach (var node in rootNode.GetAllNodes())
                {
                    node.Document = document;
                }
            }

            return document;
        }

        public void AddStylesheet(string css)
        {
            var stylesheet = StyleParser.ParseStylesheet(css);
            Stylesheets.Add(stylesheet);
        }

        public void AddStylesheet(Stylesheet stylesheet)
        {
            Stylesheets.Add(stylesheet);
        }

        public void AddStylesheets(IEnumerable<Stylesheet> stylesheets)
        {
            foreach (var stylesheet in stylesheets)
            {
                Stylesheets.Add(stylesheet);
            }
        }

        public void ComputeStyles()
        {
            foreach (var node in GetAllNodes().OfType<ElementNode>())
            {
                node.Styles = new DeclarationSet();
                foreach (var stylesheet in Stylesheets)
                {
                    node.Styles.MergeFrom(stylesheet.GetDeclarationSetForNode(node));
                }
            }
        }

        public IEnumerable<ElementNode> Find(Selector selector)
        {
            return RootNode.GetAllNodes().OfType<ElementNode>().Where(p => selector.Match(p).IsSuccess);
        }

        public IEnumerable<ElementNode> Find(string selectorString)
        {
            var selector = StyleParser.ParseSelector(selectorString);
            return Find(selector);
        }

        public IEnumerable<INode> GetAllNodes()
        {
            return RootNode.GetAllNodes();
        }
    }
}
