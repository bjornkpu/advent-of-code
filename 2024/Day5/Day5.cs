using System.Text.RegularExpressions;

namespace AoC2024.Day5;

internal static class Day5
{
    public static int Part1(string input)
    {
       var parts = input.Replace("\r","").Split("\n\n");
       var rulesPart = parts[0];
       var updatesPart = parts[1];

       List<Update> updates = [];
       foreach (var updateRow in updatesPart.Split("\n"))
       {
           var update = new Update();
           update.loadPages(updateRow);
           update.loadRules(rulesPart);
           update.CheckCorrectness();
           updates.Add(update);
       }

       return updates
           .Where(u => u.isCorrect)
           .Sum(u => u.GetMiddle());
    }
    public static int Part2(string input)
    {
        var parts = input.Replace("\r","").Split("\n\n");
        var rulesPart = parts[0];
        var updatesPart = parts[1];

        List<Update> updates = [];
        foreach (var updateRow in updatesPart.Split("\n"))
        {
            var update = new Update();
            update.loadPages(updateRow);
            update.loadRules(rulesPart);
            update.CheckCorrectness();
            updates.Add(update);
        }

        var incorrectUpdates = updates
            .Where(u => !u.isCorrect)
            .ToList();

        foreach (var update in incorrectUpdates)
        {
            update.Correct();
        }

        return incorrectUpdates
            .Sum(u => u.GetMiddle());
    }
}

class Update
{
    public bool isCorrect = true;
    public List<int> pages = [];
    private List<(int, int)> rules = [];

    public void loadPages(string input)
    {
        pages.AddRange(input.Split(",").Select(int.Parse));
    }
    public void loadRules(string input)
    {
        rules = input
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var parts = line.Split("|");
                return (int.Parse(parts[0]), int.Parse(parts[1]));
            })
            .ToList();
    }
    public int GetMiddle()
    {
        if (pages.Count % 2 == 1)
        {
            var middle = pages[pages.Count / 2];
            return middle;
        }
        throw new Exception("Invalid number of pages");
    }

    public void CheckCorrectness()
    {
        foreach (var rule in rules)
        {
            var pattern = $@"{rule.Item2}.*{rule.Item1}";
            var match = Regex.IsMatch(string.Join(",", pages), pattern);

            if (!match) continue;

            isCorrect = false;
            return;
        }
        isCorrect = true;
    }

    public void Correct()
    {
        while (!isCorrect)
        {
            var updateString = string.Join(",", pages);
            foreach (var rule in rules)
            {
                var pattern = $@"{rule.Item2}.*{rule.Item1}";
                var match = Regex.IsMatch(updateString, pattern);

                if (!match) continue;

                updateString = updateString.Replace(rule.Item1.ToString(), "x")
                    .Replace(rule.Item2.ToString(), rule.Item1.ToString())
                    .Replace("x", rule.Item2.ToString());

                pages = updateString.Split(",").Select(int.Parse).ToList();
                break;
            }

            CheckCorrectness();
        }
    }
}