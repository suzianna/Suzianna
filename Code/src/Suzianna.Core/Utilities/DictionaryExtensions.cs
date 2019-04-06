using System.Collections.Generic;

namespace Suzianna.Core.Utilities
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> AddOrUpdate<TKey,TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
            else
                dictionary[key] = value;

            return dictionary;
        }
        public static Dictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> pair)
        {
            return AddOrUpdate(dictionary, pair.Key, pair.Value);
        }

    }
}
