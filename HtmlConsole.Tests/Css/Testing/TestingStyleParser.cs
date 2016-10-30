using Eto.Parse;
using HtmlConsole.Css;

namespace HtmlConsole.Tests.Css.Testing
{
    public class TestingStyleParser : StyleParser
    {
        public Match TestingGetSyntaxTree(string str)
        {
            return GetSyntaxTree(str);
        }
    }
}
