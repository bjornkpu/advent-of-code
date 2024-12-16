using Xunit;
using Xunit.Abstractions;

namespace AoC2024.Day6;

public class Day6Tests(ITestOutputHelper testOutputHelper)
{
    private const int Day = 6;

    public static IEnumerable<object[]> TestData =>
    new List<object[]>
    {
        new object[] { Day6.Part1, "example.txt", 41 },
        new object[] { Day6.Part1, "input.txt", 5551 },
        // new object[] { Day6.Part2, "example.txt", -2 },
        // new object[] { Day6.Part2, "input.txt", -2 }
    };

    [Theory, MemberData(nameof(TestData))]
    public void Test(Func<string, int> fn, string file, int expectedResult)
    {
        var input = File.ReadAllText($"../../../Day{Day}/{file}");
        var result = fn(input);
        Assert.Equal(expectedResult, result);
        testOutputHelper.WriteLine(result.ToString());
    }
}