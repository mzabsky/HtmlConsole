using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlConsole
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> a, IEnumerable<T2> b)
        {
            return a.Zip<T1, T2, Tuple<T1, T2>>(b, Tuple.Create);
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
    }
}
