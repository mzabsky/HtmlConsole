using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eto.Parse;
using HtmlConsole.Dom;

namespace HtmlConsole.Css
{
    public abstract class Selector
    {
        /// <summary>
        /// Creates a selector object tree based on a syntax (sub)tree.
        /// </summary>
        /// <param name="selectorsMatch">Syntax (sub)tree, pointing at the "selectors" node.</param>
        /// <returns>A selector representing the same selector as the sytnax tree.</returns>
        public static Selector Create(Match selectorsMatch)
        {
            Debug.Assert(selectorsMatch.Name == "selectors");

            var orSelector = new OrSelector();
            foreach (var selectorMatch in selectorsMatch.Matches.Where(p => p.Name != "S"))
            {
                Debug.Assert(selectorMatch.Name == "selector");

                Selector currentSelector = null;
                char currentCombinator = ' ';
                foreach (var simpleSelectorMatch in selectorMatch.Matches.Where(p => p.Name != "S"))
                {
                    if (simpleSelectorMatch.Name == "combinator")
                    {
                        currentCombinator = simpleSelectorMatch.Text.Single();
                    }
                    else
                    {
                        Debug.Assert(simpleSelectorMatch.Name == "simple_selector");

                        var simpleSelector = CreateSimpleSelector(simpleSelectorMatch);

                        if (currentSelector == null)
                        {
                            currentSelector = simpleSelector;
                        }
                        else
                        {
                            CombinatorSelectorBase combinatorSelector;
                            switch (currentCombinator)
                            {
                                case '>':
                                    combinatorSelector = new IsChildOfSelector();
                                    break;
                                case ' ':
                                    combinatorSelector = new IsDescendantOfSelector();
                                    break;
                                case '+':
                                    throw new NotImplementedException("+ selector is not implemented");
                                case '~':
                                    throw new NotImplementedException("~ selector is not implemented");
                                default:
                                    throw new InvalidOperationException($"Invalid combinator '{currentCombinator}'.");
                            }

                            combinatorSelector.SubSelector = currentSelector;

                            currentSelector = new AndSelector
                            {
                                Children = new List<Selector>
                                {
                                    combinatorSelector,
                                    simpleSelector
                                }
                            };
                        }

                        // Reset the combinator after each simple selector
                        currentCombinator = ' ';
                    }
                }

                orSelector.Children.Add(currentSelector);
            }

            return orSelector;
        }

        private static Selector CreateSimpleSelector(Match simpleSelectorMatch)
        {
            Debug.Assert(simpleSelectorMatch.Name == "simple_selector");

            var andSelector = new AndSelector();
            foreach (var fragmentMatch in simpleSelectorMatch.Matches.Where(p => p.Name != "S"))
            {
                andSelector.Children.Add(CreateSimpleSelectorFragment(fragmentMatch));
            }
            return andSelector;
        }

        private static Selector CreateSimpleSelectorFragment(Match fragmentMatch)
        {
            switch (fragmentMatch.Name)
            {
                case "element_name":
                    if (fragmentMatch.Text == "*")
                    {
                        return new StarSelector();
                    }
                    else
                    {
                        return new ElementSelector {ElementName = fragmentMatch.Text};
                    }
                case "hash":
                    return new IdSelector {Id = fragmentMatch.Matches["ident"].Text};
                case "class":
                    return new ClassSelector {Class = fragmentMatch.Matches["ident"].Text};
                case "attrib":
                    throw new NotImplementedException("Attrib selectors are not supported");
                case "pseudo":
                    throw new NotImplementedException("Pseudo selectors are not supported");
                default:
                    throw new InvalidOperationException($"Invalid fragment type '{fragmentMatch.Name}'.");
            }
        }

        public abstract bool Match(ElementNode node);

        // There will be some "match" memthod
    }
}
