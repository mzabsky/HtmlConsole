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

        internal Stylesheet(Match match)
        {
            this.RuleSets = match.Matches.Select(p => new RuleSet(p)).ToList();
            //var sequenceMatch = 
        }
    }
}
