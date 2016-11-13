using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Eto.Parse;

namespace HtmlConsole.Css
{
    public class Stylesheet
    {
        public List<RuleSet> RuleSets { get; set; } = new List<RuleSet>();
        
        public static Stylesheet Create(Match syntaxTree)
        {
            return new Stylesheet
            {
                RuleSets = syntaxTree.Matches.Select(RuleSet.Create).ToList()
            }; 
        }
    }
}
