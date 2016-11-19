using System;
using System.Collections.Generic;
using System.Diagnostics;
using HtmlConsole.Extensions;
using Match = Eto.Parse.Match;

namespace HtmlConsole.Css
{
    public abstract class StyleValue
    {
        /// <summary>
        /// Gets a type that represents this style value in <see cref="StyleProperty"/> definitions.
        /// </summary>
        /// <returns>A type that represents this style value in <see cref="StyleProperty"/> definitions.</returns>
        public abstract Type GetStyleValueType();

        internal static IEnumerable<StyleValue> Create(StyleProperty property, Match expressionMatch)
        {
            Debug.Assert(expressionMatch.Name == "expression");
            
            foreach (var termMatch in expressionMatch.Matches.ExceptWhitespace())
            {
                if (termMatch.Name == "operator")
                {
                    if (!string.IsNullOrWhiteSpace(termMatch.Text))
                    {
                        // "," and "/" operators are not supported - those are only really used for font properties
                        yield break;
                    }
                }
                else
                {
                    Debug.Assert(termMatch.Name == "term");

                    if (termMatch.Text.ToLower() == "inherit")
                    {
                        yield return new InheritStyleValue();
                    }
                    else if (termMatch.Text.ToLower() == "initial")
                    {
                        yield return new InitialStyleValue();
                    }
                    else
                    {
                        foreach (var type in property.GetAllowedTypes())
                        {
                            if(type == typeof(AutoStyleValue) && termMatch.Text.ToLower() == "auto")
                            {
                                yield return new AutoStyleValue();
                                continue;
                            }

                            if (type == typeof(LengthStyleValue))
                            {
                                var value = LengthStyleValue.TryCreate(termMatch);
                                if (value != null)
                                {
                                    yield return value;
                                    continue;
                                }
                            }

                            if (type == typeof(PercentageStyleValue))
                            {
                                var value = PercentageStyleValue.TryCreate(termMatch);
                                if (value != null)
                                {
                                    yield return value;
                                    continue;
                                }
                            }
                            if(type.IsEnum)
                            {
                                var value = EnumStyleValue.TryCreate(type, termMatch);
                                if (value != null)
                                {
                                    yield return value;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
