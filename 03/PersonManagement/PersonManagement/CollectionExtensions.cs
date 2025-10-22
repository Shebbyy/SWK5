namespace PersonManagement;

public static class CollectionExtensions {
    // Bad practice for oneliners but utils can of course sometimes very useful
    // Extension Methods are only allowed in static classes
    public static void AddAll<T>(this ICollection<T> target, IEnumerable<T> source) {
        foreach (var item in source) {
            target.Add(item);
        }
    }
}