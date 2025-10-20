using System.Collections;

namespace HashDictionary.Impl;

public class HashDictionary<TK, TV> : IDictionary<TK, TV> {
    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator() {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<TK, TV> item) {
        throw new NotImplementedException();
    }

    public void Clear() {
        throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<TK, TV> item) {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex) {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TK, TV> item) {
        throw new NotImplementedException();
    }
    
    public void Add(TK key, TV value) {
        throw new NotImplementedException();
    }

    public bool ContainsKey(TK key) {
        throw new NotImplementedException();
    }

    public bool Remove(TK key) {
        throw new NotImplementedException();
    }

    public bool TryGetValue(TK key, out TV value) {
        throw new NotImplementedException();
    }

    public TV this[TK key] {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
    
    public ICollection<TK> Keys { get; }
    public ICollection<TV> Values { get; }
    public int Count { get; }
    public bool IsReadOnly { get; }
}