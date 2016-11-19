using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using Eto.Parse;
using Eto.Parse.Grammars;

namespace HtmlConsole.Css
{
    public enum StyleParserMode
    {
        Stylesheet,
        Selector,
        StyleValue,
        Declarations
    }

    public class StyleParser
    {
        private readonly Grammar _stylesheetGrammar;
        private readonly Grammar _selectorGrammar;
        private readonly Grammar _styleValueGrammar;
        private readonly Grammar _declarationsGrammar;

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
            _styleValueGrammar = new EbnfGrammar(EbnfStyle.W3c).Build(grammarText, "expression");
            _declarationsGrammar = new EbnfGrammar(EbnfStyle.W3c).Build(grammarText, "declarations");
        }

        public Stylesheet ParseStylesheet(string str)
        {
            // Lack of trailing newline can mess up the parser
            //if (str.Last() != '\n') str += Environment.NewLine;

            var syntaxTree = GetSyntaxTree(str, StyleParserMode.Stylesheet);
            return Stylesheet.Create(syntaxTree);
        }

        public Selector ParseSelector(string str)
        {
            var syntaxTree = GetSyntaxTree(str, StyleParserMode.Selector);
            return Selector.Create(syntaxTree);
        }

        public Selector ParseStyleValue(string str)
        {
            var syntaxTree = GetSyntaxTree(str, StyleParserMode.StyleValue);
            return Selector.Create(syntaxTree);
        }

        public Dictionary<string, Declaration> ParseDeclarations(string str)
        {
            // The grammar can't handle empty string, but those can come from the style attribute -> they need to be handled extra
            // TODO: Make a special entry point in the grammar instead that does declarations | S*
            if (string.IsNullOrWhiteSpace(str))
            {
                return new Dictionary<string, Declaration>();
            }

            var syntaxTree = GetSyntaxTree(str, StyleParserMode.Declarations);
            return RuleSet.CreateDeclarations(syntaxTree);
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
                case StyleParserMode.StyleValue:
                    grammar = _styleValueGrammar;
                    break;
                case StyleParserMode.Declarations:
                    grammar = _declarationsGrammar;
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
