using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eto.Parse;
using HtmlConsole.Extensions;

namespace HtmlConsole.Css
{
    public class RuleSet
    {
        public Selector Selector { get; set; }
        public Declaration[] Declarations { get; set; } = new Declaration[0];

        internal static RuleSet Create(Match rulesetMatch)
        {
            Debug.Assert(rulesetMatch.Name == "ruleset");

            return new RuleSet
            {
                Selector = Selector.Create(rulesetMatch["selectors"]),
                Declarations = CreateDeclarations(rulesetMatch["declarations"]).Values.ToArray()
            };
        }

        internal static Dictionary<string, Declaration> CreateDeclarations(Match declarationsMatch)
        {
            var allProperties = StyleProperties.All();

            Debug.Assert(declarationsMatch.Name == "declarations");

            var declarations = new Dictionary<string, Declaration>();
            foreach (var declarationMatch in declarationsMatch.Matches.ExceptWhitespace())
            {
                Debug.Assert(declarationMatch.Name == "declaration");

                bool isImportant = declarationMatch.Matches.Any(p => p.Name == "prio");

                var propertyName = declarationMatch["ident"].Text.ToLowerInvariant();
                if (!allProperties.ContainsKey(propertyName))
                {
                    continue;
                }
                
                var property = allProperties[propertyName];
                var styleValueSequence = StyleValue.Create(property, declarationMatch["expression"]);
                foreach (var mappedPropertyValue in property.MapStyleValues(styleValueSequence.ToArray()))
                {
                    declarations[mappedPropertyValue.Key] = new Declaration(propertyName, mappedPropertyValue.Value,
                        isImportant);
                }
            }

            return declarations;
        }
    }
}
