using System;
using System.IO;
using System.Linq;
using System.Reflection;
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

            _grammar = new EbnfGrammar(EbnfStyle.W3c | EbnfStyle.WhitespaceSeparator).Build(grammarText, "stylesheet");
        }

        public void Parse(string str)
        {
            // Lack of trailing newline can mess up the parser
            if (str.Last() != '\n') str += Environment.NewLine;

            var match = _grammar.Match(str);

            if (!match.Success || !string.IsNullOrEmpty(match.ErrorMessage))
            {
                throw new Exception(match.ErrorMessage);
            }
        }
    }
}
