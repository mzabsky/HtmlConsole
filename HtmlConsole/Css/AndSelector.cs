using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class AndSelector : Selector
    {
        public List<Selector> Children { get; }

        public AndSelector(IEnumerable<Selector> children)
        {
            Children = children.ToList();
        }

        public override SelectorMatch Match(ElementNode node)
        {
            return Children.Aggregate(
                new SelectorMatch(true, new Specificity()),
                (agg, selector) =>
                {
                    var match = selector.Match(node);
                    var isSuccess = match.IsSuccess && agg.IsSuccess;
                    return new SelectorMatch(isSuccess, isSuccess ? match.Specificity + agg.Specificity : new Specificity());
                });
        }

        public override string ToString() => $"[AND {string.Join(" AND ", Children.Select(p => p.ToString()))}]";
    }
}