using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eto.Parse;
using HtmlConsole.Dom;
using HtmlConsole.Extensions;

namespace HtmlConsole.Css
{
    // TODO: Make this a struct?
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

            var orSelectorChildren = new List<Selector>();
            foreach (var selectorMatch in selectorsMatch.Matches.ExceptWhitespace())
            {
                Debug.Assert(selectorMatch.Name == "selector");

                Selector currentSelector = null;
                char currentCombinator = ' ';
                foreach (var simpleSelectorMatch in selectorMatch.Matches.ExceptWhitespace())
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
                                    combinatorSelector = new IsChildOfSelector(currentSelector);
                                    break;
                                case ' ':
                                    combinatorSelector = new IsDescendantOfSelector(currentSelector);
                                    break;
                                case '+':
                                    throw new NotImplementedException("+ selector is not implemented");
                                case '~':
                                    throw new NotImplementedException("~ selector is not implemented");
                                default:
                                    throw new InvalidOperationException($"Invalid combinator '{currentCombinator}'.");
                            }
                            
                            currentSelector = new AndSelector
                            (
                                new List<Selector>
                                {
                                    combinatorSelector,
                                    simpleSelector
                                }
                            );
                        }

                        // Reset the combinator after each simple selector
                        currentCombinator = ' ';
                    }
                }

                orSelectorChildren.Add(currentSelector);
            }

            return new OrSelector(orSelectorChildren);
        }

        private static Selector CreateSimpleSelector(Match simpleSelectorMatch)
        {
            Debug.Assert(simpleSelectorMatch.Name == "simple_selector");

            var children = new List<Selector>();
            foreach (var fragmentMatch in simpleSelectorMatch.Matches.ExceptWhitespace())
            {
                children.Add(CreateSimpleSelectorFragment(fragmentMatch));
            }
            return new AndSelector(children);
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
                        return new ElementSelector(fragmentMatch.Text);
                    }
                case "hash":
                    return new IdSelector(fragmentMatch.Matches["ident"].Text);
                case "class":
                    return new ClassSelector(fragmentMatch.Matches["ident"].Text);
                case "attrib":
                    throw new NotImplementedException("Attrib selectors are not supported");
                case "pseudo":
                    throw new NotImplementedException("Pseudo selectors are not supported");
                default:
                    throw new InvalidOperationException($"Invalid fragment type '{fragmentMatch.Name}'.");
            }
        }

        public abstract SelectorMatch Match(ElementNode node);

        // There will be some "match" memthod
    }
}
