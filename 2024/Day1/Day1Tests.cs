using Xunit;
using Xunit.Abstractions;

namespace _2024.Day1;

public class Day1Tests(ITestOutputHelper testOutputHelper)
{
    private const int Day = 1;

    public static IEnumerable<object[]> TestData =>
    new List<object[]>
    {
        new object[] { Day1.Part1, "example.txt", "11" },
        new object[] { Day1.Part1, "input.txt", "1879048" },
        new object[] { Day1.Part2, "example.txt", "31" },
        new object[] { Day1.Part2, "input.txt", "21024792" }
    };

    [Theory, MemberData(nameof(TestData))]
    public void Test(Func<string, string> fn, string file, string expectedResult)
    {
        var input = File.ReadAllText($"../../../Day{Day}/{file}");
        var result = fn(input);
        Assert.Equal(expectedResult, result);
        testOutputHelper.WriteLine(result);
    }
}