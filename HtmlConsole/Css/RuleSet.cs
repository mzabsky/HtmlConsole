using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using Eto.Parse;
using HtmlConsole.Extensions;

namespace HtmlConsole.Css
{
    public class RuleSet
    {
        public Selector Selector { get; set; }
        public Dictionary<string, Rule> Declarations { get; set; } = new Dictionary<string, Rule>();

        public static RuleSet Create(Match rulesetMatch)
        {
            Debug.Assert(rulesetMatch.Name == "ruleset");

            return new RuleSet
            {
                Selector = Selector.Create(rulesetMatch["selectors"]),
                Declarations = CreateDeclarations(rulesetMatch["declarations"])
            };
        }

        public static Dictionary<string, Rule> CreateDeclarations(Match declarationsMatch)
        {
            var allProperties = StyleProperty.GetAllProperties();

            Debug.Assert(declarationsMatch.Name == "declarations");

            var declarations = new Dictionary<string, Rule>();
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
                    declarations[mappedPropertyValue.Key] = new Rule
                    {
                        PropertyName = propertyName,
                        Value = mappedPropertyValue.Value,
                        IsImportant = isImportant
                    };
                }
            }

            return declarations;
        }
    }
}
