using System.Collections.Generic;

namespace HtmlConsole.Css
{
    public static class StyleBlockExtensions
    {
        public static void MergeAnotherBlockOver(this Dictionary<string, Style> block,
            Dictionary<string, Style> another)
        {
            foreach (var style in another.Values)
            {
                block[style.Property] = style;
            }
        }
    }
}
