using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Eto.Parse;
using Eto.Parse.Grammars;

namespace HtmlConsole.Css
{
    public class StyleParser
    {
        private readonly Grammar _grammar;

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

            _grammar = new EbnfGrammar(EbnfStyle.W3c).Build(grammarText, "stylesheet");
        }

        public void Parse(string str)
        {
            // Lack of trailing newline can mess up the parser
            //if (str.Last() != '\n') str += Environment.NewLine;

            var syntaxTree = GetSyntaxTree(str);
        }

        protected Match GetSyntaxTree(string str)
        {
            var match = _grammar.Match(str);

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

        private void PrintPrettySyntaxTree(Match match, StringBuilder sb, int level)
        {
            sb.AppendLine($"{new string('\t', level)}{match.Name} - {match.Text}");
            foreach (var child in match.Matches)
            {
                PrintPrettySyntaxTree(child, sb, level + 1);
            }
        }
    }
}
