using Xunit;
using Xunit.Abstractions;

namespace AoC2024.Day3;

public class Day3Tests(ITestOutputHelper testOutputHelper)
{
    private const int Day = 3;

    public static IEnumerable<object[]> TestData =>
    new List<object[]>
    {
        new object[] { Day3.Part1, "example.txt", 161 },
        new object[] { Day3.Part1, "input.txt", 173517243 },
        new object[] { Day3.Part2, "example2.txt", 48 },
        new object[] { Day3.Part2, "input.txt", 100450138 }
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
    [InlineData(
        "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))",
        "xmul(2,4)&mul[3,7]!^?mul(8,5))")]
    [InlineData(
        "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))",
        "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))")]
    public void HandleDisabledTest(string input, string expectedResult)
    {
        // Arrange

        // Act
        var result = Day3.HandleDisabled(input);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}