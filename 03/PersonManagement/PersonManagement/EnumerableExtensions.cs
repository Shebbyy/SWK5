namespace PersonManagement;

public static class EnumerableExtensions {
    // Alternative to Predicate<T> -> Func<T, bool>
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> items, Predicate<T> comp) {
        foreach (var item in items) {
            if (comp(item)) {
                yield return item;
            }
        }
    }
    
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> handler) {
        foreach (var item in items) {
            handler(item);
        }
    }
}