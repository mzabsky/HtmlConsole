using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> a, IEnumerable<T2> b)
        {
            return a.Zip<T1, T2, Tuple<T1, T2>>(b, Tuple.Create);
        }

        public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> a)
        {
            return a.ToDictionary(p => p.Key, p => p.Value);
        }

        public static IEnumerable<T3> ExtendingZip<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, T3> operation, Func<T2, T1> fillInFirst = null, Func<T1, T2> fillInSecond = null)
        {
            using (var iter1 = first.GetEnumerator())
            {
                using (var iter2 = second.GetEnumerator())
                {
                    while (iter1.MoveNext())
                    {
                        if (iter2.MoveNext())
                        {
                            yield return operation(iter1.Current, iter2.Current);
                        }
                        else if(fillInSecond != null)
                        {
                            yield return operation(iter1.Current, fillInSecond(iter1.Current));
                        }
                    }

                    if (fillInFirst != null)
                    {
                        while (iter2.MoveNext())
                        {
                            yield return operation(fillInFirst(iter2.Current), iter2.Current);
                        }
                    }
                    
                }
            }
        }

        public static List<List<T>> Segmentize<T>(this IEnumerable<T> collection,
            Predicate<T> segmentationPredicate)
        {
            List<List<T>> result = new List<List<T>>();
            bool isFirst = true;
            bool lastConditionValue = true; // Doesn't matter, the first item gets a new segment anyways;
            foreach (var item in collection)
            {
                var currentConditionValue = segmentationPredicate(item);
                if (isFirst || currentConditionValue != lastConditionValue)
                {
                    result.Add(new List<T>());
                }
                
                result.Last().Add(item);

                lastConditionValue = currentConditionValue;
                isFirst = false;
            }

            return result;
        }
    }
}
