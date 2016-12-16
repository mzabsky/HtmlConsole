using System.Collections.Generic;
using System.Diagnostics;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using HtmlConsole.Extensions;
using HtmlConsole.Rendering;

namespace HtmlConsole
{
    public class Terminal
    {
        private readonly List<Stylesheet> _stylesheets = new List<Stylesheet>();

        public IReadOnlyList<Stylesheet> Stylesheets => _stylesheets.AsReadOnly();

        public StyleParser StyleParser { get; } = new StyleParser();
        public IConsoleWriter Writer { get; }

        public Terminal() : this(new SystemConsoleWriter())
        {
        }

        public Terminal(IConsoleWriter writer)
        {
            Writer = writer;
        }

        public void AddStylesheet(Stylesheet stylesheet)
        {
            _stylesheets.Add(stylesheet);
        }

        public void AddStylesheet(string stylesheet)
        {
            _stylesheets.Add(StyleParser.ParseStylesheet(stylesheet));
        }

        public void RemoveStylesheet(Stylesheet stylesheet)
        {
            _stylesheets.Remove(stylesheet);
        }

        public void WriteBlock(string html)
        {
            var document = Document.ParseHtml(html, StyleParser);
            document.AddStylesheets(_stylesheets);
            document.ComputeStyles();

            var view = new RenderView(document);
            var layer = new VisualLayer(new Size(Writer.GetConsoleWidth(), 1));
            view.Paint(layer);

            Writer.WriteBlock(layer);
        }
    }
}
