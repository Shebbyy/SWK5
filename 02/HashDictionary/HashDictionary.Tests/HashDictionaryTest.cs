using FluentAssertions;
using HashDictionary.Impl;

namespace HashDictionary.Tests;

public class HashDictionaryTest {
    [Fact]
    public void AddAndIndexerGetterConsistent() {
        var dict = new HashDictionary<int, int>();

        dict.Add(1, 10);
        dict[2] = 22;

        //Assert.Equal(10, dict[1]);
        //Assert.Equal(22, dict[2]);
        // Fluent Assertions interesting NuGet Package for more readable Assertions
        dict[1].Should().Be(10);
        dict[2].Should().Be(22);
    }

    [Theory]
    [InlineData(new[] {10, 20}, 2)]
    [InlineData(new[] {10, 20, 30}, 3)]
    [InlineData(new int[] {}, 0)]
    public void Theory_CountPropertyIsOk_WhenAddingItems(IEnumerable<int> list, int expected) {
        var dict = new HashDictionary<int, int>();
        int i = 0;
        foreach (var entry in list) {
            dict.Add(i, entry);
            i++;
        }
        
        
        Assert.Equal(expected, dict.Count);
    }
}
