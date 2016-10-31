using System;
using System.Collections.Generic;
using Eto.Parse;
using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public abstract class Selector
    {
        internal static Selector Create(Match match)
        {
            throw new NotImplementedException();
        }

        public abstract bool Match(TagNode node);

        // There will be some "match" memthod
    }
}
