using System.Collections.Generic;
using System.Linq;
using Eto.Parse;
using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public class Stylesheet
    {
        public List<RuleSet> RuleSets { get; set; } = new List<RuleSet>();
        
        internal static Stylesheet Create(Match syntaxTree)
        {
            return new Stylesheet
            {
                RuleSets = syntaxTree.Matches.Select(RuleSet.Create).ToList()
            }; 
        }

        public DeclarationSet GetDeclarationSetForNode(ElementNode node)
        {
            var matchingRulesets =
                from ruleSet in RuleSets
                let selectorMatch = ruleSet.Selector.Match(node)
                where selectorMatch.IsSuccess
                orderby selectorMatch.Specificity ascending
                select ruleSet;
            
            var output = new DeclarationSet();
            foreach (var matchingRuleset in matchingRulesets)
            {
                var declarationSet = new DeclarationSet(matchingRuleset.Declarations);
                output.MergeFrom(declarationSet);
            }

            return output;
        }
    }
}
