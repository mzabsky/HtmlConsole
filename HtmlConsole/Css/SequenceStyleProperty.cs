using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Css
{
    public class SequenceStyleProperty : StyleProperty
    {
        // This is not a dictionary because a dictionary is not ordered
        public KeyValuePair<string, Type[]>[] PropertySequence { get; }

        protected override IEnumerable<Type> GetAllowedTypesCore()
        {
            return PropertySequence.SelectMany(p => p.Value);
        }

        public SequenceStyleProperty(string propertyName, KeyValuePair<string, Type[]>[] propertySequence, bool isInherited = false, StyleValue initialValue = null) : base(propertyName, initialValue, isInherited)
        {
            PropertySequence = propertySequence;
        }

        public override IEnumerable<KeyValuePair<string, StyleValue>> MapStyleValues(StyleValue[] values)
        {
            var currentPropertyIndex = 0;
            foreach (var value in values)
            {
                // Find first item in the sequence that matches type of the current item
                while (!PropertySequence[currentPropertyIndex].Value.Contains(value.GetStyleValueType()))
                {
                    currentPropertyIndex++;
                    if (currentPropertyIndex >= PropertySequence.Length)
                    {
                        // Extra values in the sequence don't matter, discard them
                        yield break;
                    }
                }

                yield return new KeyValuePair<string, StyleValue>(PropertySequence[currentPropertyIndex].Key, value);
            }
        }
    }
}