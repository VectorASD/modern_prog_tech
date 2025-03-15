using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp {
    [Serializable]
    public class KeyError : Exception {
        public KeyError() : base() { }
        public KeyError(string message) : base(message) { }
        public KeyError(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]
    public class ValueError : Exception {
        public ValueError() : base() { }
        public ValueError(string message) : base(message) { }
        public ValueError(string message, Exception inner) : base(message, inner) { }
    }



    // https://thedeveloperblog.com/multimap (с моими доработками)

    public class MultiMap<K, V> where K : notnull {
        // readonly Dictionary<K, List<V>> dict = [];
        readonly SortedDictionary<K, List<V>> dict = [];
        // в C++ в STL для этого применяется компаратор, при том по умолчанию нам нужный - std::less<T>

        public void Add(K key, V value) {
            if (dict.TryGetValue(key, out var list))
                list.Add(value);
            else
                dict[key] = [value]; // вместо list = new List<V>(); list.Add(value); 
        }

        public void Remove(K key, V value) {
            if (!dict.TryGetValue(key, out var list))
                throw new KeyError($"multimap key not found: {key}");
            if (!list.Remove(value))
                throw new ValueError($"multimap value not found (key = {key}): {value}");
            if (list.Count == 0) dict.Remove(key);
        }

        public void Clear() {
            dict.Clear();
        }

        public IEnumerable<K> Keys => dict.Keys;

        public List<V> this[K key] {
            get {
                if (!dict.TryGetValue(key, out var list))
                    throw new KeyError("multimap key not found: " + key);
                return list;
            }
        }

        public int Count => dict.Count;
        public bool Empty => dict.Count == 0;
    }
}
