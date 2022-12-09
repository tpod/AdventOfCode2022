namespace AdventOfCode;

public static class Day1
{
    public static int Part2()
    {
        var caloriesPerElf = new List<int>();
        var current = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input1.txt"))
        {
            if (string.IsNullOrEmpty(line))
            {
                caloriesPerElf.Add(current);
                current = 0;
            }
            else
            {
                var calories = int.Parse(line);
                current += calories;
            }
        }

        return caloriesPerElf
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }

    public static int Part1()
    {
        var highest = 0;
        var current = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input1.txt"))
        {
            if (string.IsNullOrEmpty(line))
            {
                if (current > highest)
                {
                    highest = current;
                }

                current = 0;
            }
            else
            {
                var calories = int.Parse(line);
                current += calories;
            }
        }

        return highest;
    }
}