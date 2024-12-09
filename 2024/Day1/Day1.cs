namespace AoC2024.Day1;

internal static class Day1
{
    public static string Part1(string input)
    {
        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (var row in input.Split("\n"))
        {
            var parts = row.Split("   ");
            leftList.Add(int.Parse(parts[0]));
            rightList.Add(int.Parse(parts[1]));
        }

        leftList.Sort();
        rightList.Sort();

        var distances = leftList
            .Select((value, index) => Math.Abs(value - rightList[index]))
            .ToList();

        return distances.Sum().ToString();
    }
    public static string Part2(string input)
    {
        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (var row in input.Split("\n"))
        {
            var parts = row.Split("   ");
            leftList.Add(int.Parse(parts[0]));
            rightList.Add(int.Parse(parts[1]));
        }

        var similarityList = leftList
            .Select(value => value * rightList
                .Count(x => x == value))
            .ToList();

        return similarityList.Sum().ToString();
    }
}