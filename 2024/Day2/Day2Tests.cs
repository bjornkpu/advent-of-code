using Xunit;
using Xunit.Abstractions;

namespace AoC2024.Day2;

public class Day2Tests(ITestOutputHelper testOutputHelper)
{
    private const int Day = 2;

    public static IEnumerable<object[]> TestData =>
    new List<object[]>
    {
        new object[] { Day2.Part1, "example.txt", 2 },
        new object[] { Day2.Part1, "input.txt", 670 },
        new object[] { Day2.Part2, "example.txt", 4 },
        new object[] { Day2.Part2, "input.txt", 700 }
    };

    [Theory, MemberData(nameof(TestData))]
    public void Test(Func<string, int> fn, string file, int expectedResult)
    {
        var input = File.ReadAllText($"../../../Day{Day}/{file}");
        var result = fn(input);
        Assert.Equal(expectedResult, result);
        testOutputHelper.WriteLine(result.ToString());
    }
    
    [Theory]
    [InlineData("7 6 4 2 1", true)]
    [InlineData("1 2 7 8 9", false)]
    [InlineData("9 7 6 2 1", false)]
    [InlineData("1 3 2 4 5", false)]
    [InlineData("8 6 4 4 1", false)]
    [InlineData("1 3 6 7 9", true)]
    public void IsSafeTests(string report, bool expectedResult)
    {
        // Arrange
        var levels = Array.ConvertAll(report.Split(' '), int.Parse);

        // Act
        var result = Day2.IsSafe(new List<int>(levels));

        // Assert
        Assert.Equal(expectedResult, result);
    }
    [Theory]
    [InlineData("7 6 4 2 1", true)]
    [InlineData("1 2 7 8 9", false)]
    [InlineData("9 7 6 2 1", false)]
    [InlineData("1 3 2 4 5", true)]
    [InlineData("8 6 4 4 1", true)]
    [InlineData("1 3 6 7 9", true)]
    public void IsSafeV2Tests(string report, bool expectedResult)
    {
        // Arrange
        var levels = Array.ConvertAll(report.Split(' '), int.Parse);

        // Act
        var result = Day2.IsSafeV2(new List<int>(levels));

        // Assert
        Assert.Equal(expectedResult, result);
    }
}