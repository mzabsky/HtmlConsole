using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class ElementSelector : Selector
    {
        public string ElementName { get; set; }

        public override bool Match(TagNode node)
        {
            return node.Tag == ElementName;
        }
    }
}
