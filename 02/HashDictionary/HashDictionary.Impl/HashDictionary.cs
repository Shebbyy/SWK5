using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HashDictionary.Impl;

public class HashDictionary<TK, TV> : IDictionary<TK, TV> {
    #region Inner Class Node
        private class Node {
            public required TK Key { get; set; }
            public required TV Value { get; set; }
            public Node Next { get; set; }
        }
    #endregion
    
    #region constants and private properties
        private const int INITIAL_HASH_TABLE_SIZE = 8;
        private Node[] ht = new Node[INITIAL_HASH_TABLE_SIZE];
        private int IndexFor(TK key) => Math.Abs(key.GetHashCode() % ht.Length);
    #endregion
    
    #region helper methods
        private Node FindNode(TK key) {
            Node n = ht[IndexFor(key)];
    
            // is not is safe from operator overloading, instead of !=
            for (; n is not null; n = n.Next) {
                if (Comparer.Equals(n.Key, key)) return n;
            }
    
            return null;
        }
    
        // Slightly more efficient than n.Key.Equals(), but only veeeery slightly
        private static readonly EqualityComparer<TK> Comparer = EqualityComparer<TK>.Default;

        private bool TryAdd(TK key, TV val, out Node node) {
            node = FindNode(key);
            if (node is not null) return false;

            int idx = IndexFor(key);
            node = ht[idx] = new Node {
                Key = key, 
                Value = val, 
                Next = ht[idx]
            };
            Count++;

            return true;
        }
    #endregion

    public ICollection<TK> Keys {
        get {
            var keys = new List<TK>(ht.Length);

            foreach (KeyValuePair<TK, TV> pair in this) {
                keys.Add(pair.Key);
            }

            return keys;
        }
    }

    public ICollection<TV> Values {
        get {
            var values = new List<TV>(ht.Length);

            foreach (KeyValuePair<TK, TV> pair in this) {
                values.Add(pair.Value);
            }

            return values;
        }
    }

    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator() {
        for (int i = 0; i < ht.Length; i++) {
            for (Node n = ht[i]; n is not null; n = n.Next) {
                // yield return in the background looks at IEnumerator definition with moveNext; Is obviously not implemented here, Compiler is told to act like Enumerator is implemented
                // Bit of a StateMachine in the background for the iteration, remembers current control flow on yield, so it can continue where it stopped in the loop
                yield return new KeyValuePair<TK, TV>(n.Key, n.Value);
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(KeyValuePair<TK, TV> item) => Add(item.Key, item.Value);
    
    public void Add(TK key, TV value) {
        if (!TryAdd(key, value, out _)) {
            throw new ArgumentException("Key already exists");
        }
    }
    
    public TV this[TK key] {
        get {
            Node n = FindNode(key);
            return n is null ? throw new KeyNotFoundException() : n.Value;
        }
        
        set {
            // Either add in TryAdd, else update value
            if (!TryAdd(key, value, out Node node)) {
                node.Value = value;
            }
        }
    }
    
    public bool TryGetValue(TK key, [MaybeNullWhen(false)] out TV value) {
        Node n = FindNode(key);
        value = n is not null ? n.Value : default;
        return n is not null;
    }

    public bool Contains(KeyValuePair<TK, TV> item) => ContainsKey(item.Key);
    public bool ContainsKey(TK key) => FindNode(key) is not null;
    

    public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex) {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TK, TV> item) {
        throw new NotImplementedException();
    }

    public bool Remove(TK key) {
        throw new NotImplementedException();
    }
    
    public void Clear() {
        ht = new Node[INITIAL_HASH_TABLE_SIZE];
        Count = 0;
    }
}