using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;
using HtmlConsole.Css;
using HtmlConsole.Dom;

namespace HtmlConsole
{
    //public class BlockWriter
    //{
    //    private readonly StringWriter writer = new StringWriter();
    //    private int currentLineLength = 0;

    //    public char LastCharacter { get; private set; } = '\0';
    //    public int BlockWidth { get; }
    //    public bool ForceWrapping { get; }

    //    public BlockWriter(int blockWidth, bool forceWrapping = true)
    //    {
    //        BlockWidth = blockWidth;
    //        ForceWrapping = ForceWrapping;
    //    }

    //    public void Write(string str)
    //    {
    //        int currentLineLegth = 0;
    //        foreach (var c in str)
    //        {
    //            if (c == '\r' || c == '\n')
    //            {
    //                currentLineLegth = 0;
    //            }

    //            if (ForceWrapping && currentLineLegth > BlockWidth)
    //            {
    //                writer.Write(Environment.NewLine);
    //                currentLineLegth = 0;
    //            }

    //            writer.Write(c);
    //            LastCharacter = c;
    //            currentLineLegth++;
    //        }
    //    }

    //    public bool LastCharIsWhiteSpace() => char.IsWhiteSpace(LastCharacter);

    //    public string GetString() => writer.GetStringBuilder().ToString();
    //}

    //public class PathSegment
    //{
    //    public string Element { get; set; }
    //    public List<string> Classes { get; set; }
    //    public string Id { get; set; }
    //    public int Index { get; set; }

    //    public override string ToString()
    //    {
    //        return $"{Element}#{Id}{string.Join("", Classes.Select(p => $".{p}"))}[{Index}]";
    //    }
    //}

    //public class HtmlConsolePresentation : IConsolePresentation
    //{
    //    private class TableStackEntry
    //    {
    //        public List<TableRow> Rows { get; } = new List<TableRow>();
    //    }

    //    private class TableRow
    //    {
    //        public XmlElement RowElement { get; set; }
    //        public List<XmlElement> CellElements { get; } = new List<XmlElement>();
    //    }
        
    //    private Stylesheet stylesheet;
    //    private IConsoleWriter writer;

    //    private char lastCharacter = '\0';
    //    private readonly Stack<TableStackEntry> tableStack = new Stack<TableStackEntry>();
    //    private readonly Stack<BlockWriter> blockWriterStack = new Stack<BlockWriter>();
    //    private readonly Stack<PathSegment> pathSegmentStack = new Stack<PathSegment>(); 

    //    private BlockWriter CurrentBlockWriter => blockWriterStack.Peek();

    //    public HtmlConsolePresentation(Stylesheet stylesheet, IConsoleWriter writer)
    //    {
    //        this.stylesheet = stylesheet;
    //        this.writer = writer;
    //    }

    //    public virtual string ReadLine()
    //    {
    //        return System.Console.ReadLine();
    //    }

    //    public void WriteLine(string line)
    //    {
    //        Write($"{line}<br/>");
    //    }

    //    public void Write(string str)
    //    {
    //        HtmlDocument doc = new HtmlDocument();
    //        doc.LoadHtml($"<message>{str}</message>");

    //        // The top level block is not wrapped (the console software will take care of that).
    //        //blockWriterStack.Push(new BlockWriter(GetConsoleWidth(), false));

    //        // Parse
    //        var root = TagNode.ParseNode(doc.DocumentNode);

    //        // Calculate styles

    //        // Measure

    //        // Arrange

    //        //ParseElement(doc.DocumentElement);

    //        /*Assert.AreEqual(0, pathSegmentStack.Count);
    //        Assert.AreEqual(1, blockWriterStack.Count);*/

    //        //WriteToOutputCore(CurrentBlockWriter.GetString());

    //        blockWriterStack.Pop();
    //    }

    //    /*private void ParseElement(XmlElement element)
    //    {
    //        var localName = element.LocalName.ToLowerInvariant();

    //        var classAttribute = element.Attributes
    //            .OfType<XmlAttribute>()
    //            .Where(p => p.LocalName == "class")
    //            .Select(p => p.InnerText.ToLower())
    //            .SingleOrDefault(p => Regex.IsMatch(p, "^-?[_a-z]+[_a-z0-9-]*$")); // http://stackoverflow.com/questions/448981/which-characters-are-valid-in-css-class-names-selectors

    //        var idAttribute = element.Attributes
    //            .OfType<XmlAttribute>()
    //            .Where(p => p.LocalName == "id")
    //            .Select(p => p.InnerText.ToLower())
    //            .SingleOrDefault(p => Regex.IsMatch(p, "^[^ #.]+$"));

    //        pathSegmentStack.Push(new PathSegment
    //        {
    //            Element = localName,
    //            Index = element.ParentNode?.ChildNodes.OfType<XmlNode>().TakeWhile(p => p != element).Count() ?? 0,
    //            Id = idAttribute?.ToLower(),
    //            Classes = (classAttribute ?? "").ToLower().Split(' ').ToList()
    //        });

    //        var matchingStyles = stylesheet.GetMatchingDeclarations(pathSegmentStack);

    //        var styleAttribute = element.Attributes
    //            .OfType<XmlAttribute>()
    //            .Where(p => p.LocalName == "style")
    //            .Select(p => p.InnerText)
    //            .SingleOrDefault();

    //        if (styleAttribute != null)
    //        {
    //            var inlineStyles = Stylesheet.ParseBlock(styleAttribute);
    //            matchingStyles.MergeAnotherBlockOver(inlineStyles.ToDictionary(p => p.Property, p => p));
    //        }

    //        Display display;
    //        if (matchingStyles.ContainsKey("display") && matchingStyles["display"].Value == "block")
    //        {
    //            display = Display.Block;

    //            int blockWidth = CurrentBlockWriter.BlockWidth;

    //            blockWriterStack.Push(new BlockWriter(CurrentBlockWriter.BlockWidth));
    //        }
    //        else
    //        {
    //            display = Display.Inline;
    //        }

    //        var previousBackgroundColor = GetBackgroundColor();
    //        if (matchingStyles.ContainsKey("background-color"))
    //        {
    //            //this.SetBackgroundColor(this.StyleColorToConsoleColor(matchingStyles["background-color"].Value));
    //        }

    //        var previousForegroundColor = GetForegroundColor();
    //        if (matchingStyles.ContainsKey("color"))
    //        {
    //            //this.SetForegroundColor(this.StyleColorToConsoleColor(matchingStyles["color"].Value));
    //        }

    //        if (localName == "br")
    //        {
    //            CurrentBlockWriter.Write(Environment.NewLine);
    //        }

    //        if (localName == "table")
    //        {
    //            var tableStackEntry = new TableStackEntry();
    //            tableStack.Push(tableStackEntry);
    //        }

    //        if (localName == "pre")
    //        {
    //            CurrentBlockWriter.Write(element.InnerXml);
    //        }
    //        else
    //        {
    //            foreach (var childNode in element.ChildNodes)
    //            {
    //                if (childNode is XmlText)
    //                {
    //                    var innerText = (childNode as XmlText).InnerText;

    //                    // Collapse white space
    //                    innerText = Regex.Replace(innerText, @"\s+", " ");

    //                    if (blockWriterStack.Peek().LastCharIsWhiteSpace())
    //                    {
    //                        innerText = innerText.TrimStart();
    //                    }

    //                    CurrentBlockWriter.Write(innerText);
    //                }
    //                else if (childNode is XmlWhitespace)
    //                {
    //                    if (!CurrentBlockWriter.LastCharIsWhiteSpace())
    //                    {
    //                        CurrentBlockWriter.Write(" ");
    //                    }
    //                }
    //                else if (childNode is XmlElement)
    //                {
    //                    var xmlElement = childNode as XmlElement;
    //                    var childLocalName = xmlElement.LocalName.ToLowerInvariant();

    //                    if (childLocalName == "tr")
    //                    {
    //                        if (tableStack.Any())
    //                        {
    //                            tableStack.Peek().Rows.Add(new TableRow {RowElement = xmlElement});
    //                        }

    //                        //this.ParseElement(xmlElement);
    //                    }
    //                    else if (childLocalName == "td" || childLocalName == "th")
    //                    {
    //                        if (tableStack.Any() && tableStack.Peek().Rows.Any())
    //                        {
    //                            tableStack.Peek().Rows.Last().CellElements.Add(xmlElement);
    //                        }

    //                        //this.ParseElement(xmlElement);
    //                    }
    //                    else
    //                    {
    //                        ParseElement(xmlElement);
    //                    }

    //                }
    //            }
    //        }

    //        if(localName == "table")
    //        {
    //            var tableStackEntry = tableStack.Pop();
    //            RenderTable(tableStackEntry);
    //        }

    //        else if (localName == "h1")
    //        {
    //            CurrentBlockWriter.Write($"{Environment.NewLine}====================");
    //        }
    //        else if (localName == "h2")
    //        {
    //            CurrentBlockWriter.Write($"{Environment.NewLine}--------------------");
    //        }

    //        if (display == Display.Block)
    //        {
    //            var blockWriter = blockWriterStack.Pop();

    //            CurrentBlockWriter.Write(Environment.NewLine);
    //            CurrentBlockWriter.Write(blockWriter.GetString());
    //            CurrentBlockWriter.Write(Environment.NewLine);
    //        }


    //        if (GetBackgroundColor() != previousBackgroundColor)
    //        {
    //            SetBackgroundColor(previousBackgroundColor);
    //        }

    //        if (GetForegroundColor() != previousForegroundColor)
    //        {
    //            SetForegroundColor(previousForegroundColor);
    //        }

    //        pathSegmentStack.Pop();
    //    }

    //    private void RenderTable(TableStackEntry tableStackEntry)
    //    {



    //        throw new NotImplementedException();
    //    }

        
    //    protected virtual void WriteToOutputCore(string str)
    //    {
    //        System.Console.Write(str);
    //    }

    //    protected virtual ConsoleColor GetForegroundColor()
    //    {
    //        return System.Console.ForegroundColor;
    //    }

    //    protected virtual ConsoleColor GetBackgroundColor()
    //    {
    //        return System.Console.BackgroundColor;
    //    }

    //    protected virtual void SetForegroundColor(ConsoleColor color)
    //    {
    //        System.Console.ForegroundColor = color;
    //    }

    //    protected virtual void SetBackgroundColor(ConsoleColor color)
    //    {
    //        System.Console.BackgroundColor = color;
    //    }

    //    protected virtual int GetConsoleWidth()
    //    {
    //        try
    //        {
    //            return System.Console.BufferWidth;
    //        }
    //        catch (IOException)
    //        {
    //            // Exception is throw when this is run outside of a console (such as within a unit test runner)
    //            return 60;
    //        }
    //    }*/
    //}
}
