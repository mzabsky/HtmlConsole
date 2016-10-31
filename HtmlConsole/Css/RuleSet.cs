using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Parse;

namespace HtmlConsole.Css
{
    public class RuleSet
    {
        public Selector Selector { get; set; }
        public Dictionary<string, StyleValue> Declarations { get; set; } = new Dictionary<string, StyleValue>();

        public RuleSet()
        {
        }

        internal RuleSet(Match p)
        {
            throw new NotImplementedException();
        }
    }
}
