using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HtmlConsole.Css
{
    public class Stylesheet
    {
        public Dictionary<string, Dictionary<string, Style>> Blocks { get; }

        private Stylesheet(Dictionary<string, Dictionary<string, Style>> blocks)
        {
            Blocks = blocks;
        }

        public static Stylesheet Parse(string str)
        {
            Dictionary<string, Dictionary<string, Style>> blocks = new Dictionary<string, Dictionary<string, Style>>();
            foreach (var blockStr in str.Split('}'))
            {
                var splitBlock = blockStr.Split('{');

                if (splitBlock.Length != 2)
                {
                    continue;
                }

                var selector = splitBlock[0].Trim();
                var declarations = ParseBlock(splitBlock[1]).ToList();

                Dictionary<string, Style> existingDeclarations;
                if (blocks.TryGetValue(selector, out existingDeclarations))
                {
                    foreach (var declaration in declarations)
                    {
                        existingDeclarations[declaration.Property] = declaration;
                    }
                }
                else
                {
                    blocks[selector] = declarations.ToDictionary(p => p.Property, p => p);
                }
            }
            
            return new Stylesheet(blocks);
        }

        public static IEnumerable<Style> ParseBlock(string str)
        {
            var matches = Regex.Matches(str, @"(?<property>[a-z-]+)\s*:\s*(?<value>[^;]+)\s*;", RegexOptions.IgnoreCase);

            return matches.OfType<Match>().Select(
                match => new Style(
                    match.Groups["property"].ToString(), 
                    match.Groups["value"].ToString()));
        }

        public IEnumerable<Tuple<Dictionary<string, Style>, Specificity>> CalculateSpecificityForBlocks(IEnumerable<PathSegment> path)
        {
            var pathList = path.ToList();

            foreach (var block in Blocks)
            {
                var element = pathList.First().Element;  
                if (block.Key == element)
                {
                    yield return
                        Tuple.Create(
                            block.Value,
                            new Specificity {IdSpecificity = 0, ClassSpecificity = 0, ElementSpecificity = 1});
                }
                else if (block.Key == $"#{pathList.First().Id}")
                {
                    yield return
                        Tuple.Create(
                            block.Value,
                            new Specificity { IdSpecificity = 1, ClassSpecificity = 0, ElementSpecificity = 0 });
                }
                else if (pathList.First().Classes.Any(p => block.Key == $".{p}"))
                {
                    yield return
                        Tuple.Create(
                            block.Value,
                            new Specificity { IdSpecificity = 0, ClassSpecificity = 1, ElementSpecificity = 0 });
                }
            }
        }

        public Dictionary<string, Style> GetMatchingDeclarations(IEnumerable<PathSegment> path)
        {
            var matchingBlocks = CalculateSpecificityForBlocks(path)
                .OrderBy(p => p.Item2, Comparer<Specificity>.Default)
                .Select(p => p.Item1)
                .ToList();

            var resultingBlock = new Dictionary<string, Style>();
            foreach (var matchingBlock in matchingBlocks)
            {
                resultingBlock.MergeAnotherBlockOver(matchingBlock);
            }

            return resultingBlock;
        }

        public static Stylesheet GetDefaultStylesheet()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GenerationShip.Console.ConsoleStack.Resources.DefaultStylesheet.css"))
            using (StreamReader reader = new StreamReader(stream))
            {
                return Parse(reader.ReadToEnd());
            }
        }
    }
}
