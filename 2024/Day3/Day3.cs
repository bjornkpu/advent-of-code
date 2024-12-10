using System.Text.RegularExpressions;

namespace AoC2024.Day3;

internal static class Day3
{
    public static int Part1(string input) => ParseInput(input)
        .Sum(t => t.Item1 * t.Item2);

    public static int Part2(string input) => ParseInput(HandleDisabled(input))
        .Sum(t => t.Item1 * t.Item2);

    private static List<(int, int)> ParseInput(string input)
    {
        const string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var matches = Regex.Matches(input, pattern);

        var results = new List<(int, int)>();
        foreach (Match match in matches)
        {
            if (match.Groups.Count != 3) continue;
            var firstInt = int.Parse(match.Groups[1].Value);
            var secondInt = int.Parse(match.Groups[2].Value);
            results.Add((firstInt, secondInt));
        }
        return results;
    }

    public static string HandleDisabled(string input)
    {
        return input
                .Replace("\n", "")
                .Replace("don't()","\ndon't()")
                .Replace("do()","\n")
                .Split("\n")
                .Where(str => !str.StartsWith("don't()"))
                .Aggregate((a, b) => a + b)
            ;
    }
}