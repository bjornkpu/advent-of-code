using Xunit;
using Xunit.Abstractions;

namespace AoC2024.Day5;

public class Day5Tests(ITestOutputHelper testOutputHelper)
{
    private const int Day = 5;

    public static IEnumerable<object[]> TestData =>
    new List<object[]>
    {
        new object[] { Day5.Part1, "example.txt", 143 },
        new object[] { Day5.Part1, "input.txt", 7198 },
        new object[] { Day5.Part2, "example.txt", 123 },
        new object[] { Day5.Part2, "input.txt", 4230 }
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
    [InlineData("75,97,47,61,53", "97,75,47,61,53")]
    [InlineData("61,13,29", "61,29,13")]
    [InlineData("97,13,75,29,47", "97,75,47,29,13")]
    public void HandleDisabledTest(string input, string expectedResult)
    {
        // Arrange
        var rules = File
            .ReadAllText($"../../../Day{Day}/example.txt")
            .Replace("\r","")
            .Split("\n\n")[0];
        var update = new Update();
        update.loadPages(input);
        update.loadRules(rules);
        update.CheckCorrectness();

        // Act
        update.Correct();

        // Assert
        Assert.Equal(expectedResult, string.Join(",", update.pages));
    }
}

