namespace AoC2024.Day2;

public static class Day2
{
    public static int Part1(string input) => ParseInput(input).Count(IsSafe);
    public static int Part2(string input) => ParseInput(input).Count(IsSafeV2);


    private static List<List<int>> ParseInput(string input) =>
        input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => Array
                .ConvertAll(line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse))
            .Select(levels => new List<int>(levels))
            .ToList();

    public static bool IsSafe(List<int> levels)
    {
        if (levels.Count < 2) return true;
        var increasing = levels[1] > levels[0];
        for (var i = 1; i < levels.Count; i++)
        {
            var diff = levels[i] - levels[i - 1];
            if (diff != 0 && Math.Abs(diff) <= 3 && (!increasing || diff >= 0) && (increasing || diff <= 0)) continue;
            return false;
        }
        return true;
    }
    public static bool IsSafeV2(List<int> levels)
    {
        var noDirectionTrouble = IsIncreasingOrDecreasing(levels);
        var noRangeTrouble = IsAdjacentDifferenceWithinRange(levels);

        if (noDirectionTrouble && noRangeTrouble)
        {
            return true;
        }

        var permutationsOfLevels = new List<List<int>>();
        for (var i = 0; i < levels.Count; i++)
        {
            var newLevels = new List<int>(levels);
            newLevels.RemoveAt(i);
            permutationsOfLevels.Add(newLevels);
        }

        foreach (var permutation in permutationsOfLevels)
        {
            var noDirectionTroubleInPermutation = IsIncreasingOrDecreasing(permutation);
            var noRangeTroubleInPermutation = IsAdjacentDifferenceWithinRange(permutation);

            if (noDirectionTroubleInPermutation && noRangeTroubleInPermutation)
            {
                return true;
            }
        }
        return false;
    }


    public static bool IsIncreasingOrDecreasing(List<int> levels)
    {
        if (levels.Count < 2) return true;

        var increasing = levels[1] > levels[0];
        for (var i = 1; i < levels.Count; i++)
        {
            if ((increasing && levels[i] < levels[i - 1]) || (!increasing && levels[i] > levels[i - 1]))
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsAdjacentDifferenceWithinRange(List<int> levels)
    {
        for (var i = 1; i < levels.Count; i++)
        {
            var diff = Math.Abs(levels[i] - levels[i - 1]);
            if (diff is < 1 or > 3)
            {
                return false;
            }
        }
        return true;
    }
}