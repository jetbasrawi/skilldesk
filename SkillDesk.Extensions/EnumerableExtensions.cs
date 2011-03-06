using System;
using System.Collections.Generic;

namespace SkillDesk.Extensions
{
    // See http://www.developerfusion.com/article/84468/linq-to-log-files/

    public static class EnumerableExtensions
    {
        public static IEnumerable<KeyValuePair<TKey, int>> GroupAndCount<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> mapping)
        {
            Dictionary<TKey, int> dictionary = new Dictionary<TKey, int>();
            foreach (TElement element in source)
            {
                TKey key = mapping(element);

                if (dictionary.ContainsKey(key))
                    dictionary[key]++;
                else
                    dictionary[key] = 1;
            }
            return dictionary;
        }
    }
}