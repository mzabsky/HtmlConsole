using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Eto.Parse;
using Eto.Parse.Grammars;

namespace HtmlConsole.Css
{
    public enum StyleParserMode
    {
        Stylesheet,
        Selector
    }

    public class StyleParser
    {
        private readonly Grammar _stylesheetGrammar;
        private readonly Grammar _selectorGrammar;

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public StyleParser()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "HtmlConsole.Css.cssgrammar.txt";

            string grammarText;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                grammarText = reader.ReadToEnd();
            }

            _stylesheetGrammar = new EbnfGrammar(EbnfStyle.W3c).Build(grammarText, "stylesheet");
            _selectorGrammar = new EbnfGrammar(EbnfStyle.W3c).Build(grammarText, "selectors");
        }

        public void ParseStylesheet(string str)
        {
            // Lack of trailing newline can mess up the parser
            //if (str.Last() != '\n') str += Environment.NewLine;

            var syntaxTree = GetSyntaxTree(str, StyleParserMode.Stylesheet);
        }

        public Selector ParseSelector(string str)
        {
            var syntaxTree = GetSyntaxTree(str, StyleParserMode.Selector);
            return Selector.Create(syntaxTree);
        }

        protected Match GetSyntaxTree(string str, StyleParserMode mode = StyleParserMode.Stylesheet)
        {
            Grammar grammar;
            switch (mode)
            {
                case StyleParserMode.Stylesheet:
                    grammar = _stylesheetGrammar;
                    break;
                case StyleParserMode.Selector:
                    grammar = _selectorGrammar;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            var match = grammar.Match(str);

            if (!match.Success/* || !string.IsNullOrEmpty(match.ErrorMessage)*/)
            {
                throw new Exception(match.ErrorMessage);
            }

            return match;
        }

        public string PrintSyntaxTree(Match match, Func<Match, int, string> printer)
        {
            var sb = new StringBuilder();
            PrintSyntaxTree(match, printer, sb, 0);
            return sb.ToString();
        }

        private void PrintSyntaxTree(Match match, Func<Match, int, string> printer, StringBuilder sb, int level)
        {
            sb.Append(printer(match, level));
            foreach (var child in match.Matches)
            {
                PrintSyntaxTree(child, printer, sb, level + 1);
            }
        }

        public string PrintPrettySyntaxTree(Match match)
        {
            return PrintSyntaxTree(match, (m, l) => $"{new string('\t', l)}{m.Name} - {m.Text}{Environment.NewLine}");
        }
    }
}
