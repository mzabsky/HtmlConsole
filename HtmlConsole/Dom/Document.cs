using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlConsole.Css;

namespace HtmlConsole.Dom
{
    public class Document
    {
        public StyleParser StyleParser { get; set; }
        public INode RootNode { get; set; }

        public static Document ParseHtml(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var rootNode = ElementNode.ParseNode(htmlDocument.DocumentNode);

            return new Document
            {
                RootNode = rootNode,
                StyleParser = new StyleParser()
            };
        }

        public void EvaluateStylesheet(Stylesheet stylesheet)
        {
            EvaluateStylesheet(stylesheet, new List<ElementNode>());
        }

        private void EvaluateStylesheet(Stylesheet stylesheet, List<ElementNode> path)
        {
            // TODO parse HTML attribute "style"
            var currentPath = path.Concat(new[] { RootNode });

            /*foreach (var ruleSet in stylesheet.RuleSets)
            {
                var selector = 
            }*/
        }

        public IEnumerable<ElementNode> Find(Selector selector)
        {
            return RootNode.GetAllNodes().OfType<ElementNode>().Where(selector.Match);
        }
    }
}
