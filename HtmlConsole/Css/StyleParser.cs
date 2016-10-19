using System.IO;
using System.Reflection;
using Eto.Parse;
using Eto.Parse.Grammars;

namespace HtmlConsole.Css
{
    public class StyleParser
    {
        private Grammar _grammar;

        public StyleParser()
        {
            var a = Assembly.GetExecutingAssembly().GetManifestResourceNames();

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
            var match = _grammar.Match("");
        }
    }
}
