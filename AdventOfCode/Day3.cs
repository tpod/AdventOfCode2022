public class Day3
{
    public static int Part1()
    {
        var sum = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input3.txt"))
        {
            if (string.IsNullOrEmpty(line)) continue;
            
            var firstPart = line[..(line.Length / 2)].ToCharArray();
            var secondPart = line.Substring(line.Length / 2, line.Length / 2).ToCharArray();

            foreach (var c in firstPart)
            {
                if (secondPart.Contains(c))
                {
                    sum += CharacterPriority[c];
                    break;
                }
            }
        }

        return sum;
    }
    
    public static int Part2()
    {
        var sum = 0;
        var groups = new GroupFactory();
        
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input3.txt"))
        {
            if (string.IsNullOrEmpty(line)) continue;
            groups.Add(line);
        }

        foreach (var group in groups.Groups)
        {
            sum += CharacterPriority[group.FindBadge()];
        }

        return sum;
    }

    private class GroupFactory
    {
        public List<Group> Groups { get; } = new() { new Group() };

        public void Add(string line)
        {
            var currentGroup = Groups.Last();
            if(currentGroup.Bags.Count == 3) 
                Groups.Add(new Group());
            
            currentGroup = Groups.Last();
            
            currentGroup.Bags.Add(line);
        }
    }
    
    private class Group
    {
        public List<string> Bags { get; } = new();

        public char FindBadge()
        {
            foreach (var c in Bags.First())
            {
                if (Bags[1].Contains(c) && Bags[2].Contains(c))
                    return c;
            }

            throw new Exception("No badge found");
        }
    }
    
    private static Dictionary<char, int> CharacterPriority = new() 
    {
        ['a'] = 1, 
        ['b'] = 2, 
        ['c'] = 3, 
        ['d'] = 4, 
        ['e'] = 5, 
        ['f'] = 6, 
        ['g'] = 7, 
        ['h'] = 8, 
        ['i'] = 9, 
        ['j'] = 10, 
        ['k'] = 11, 
        ['l'] = 12, 
        ['m'] = 13, 
        ['n'] = 14, 
        ['o'] = 15, 
        ['p'] = 16, 
        ['q'] = 17, 
        ['r'] = 18, 
        ['s'] = 19, 
        ['t'] = 20, 
        ['u'] = 21, 
        ['v'] = 22, 
        ['w'] = 23, 
        ['x'] = 24, 
        ['y'] = 25, 
        ['z'] = 26,
        ['A'] = 27,
        ['B'] = 28,
        ['C'] = 29,
        ['D'] = 30,
        ['E'] = 31,
        ['F'] = 32,
        ['G'] = 33,
        ['H'] = 34,
        ['I'] = 35,
        ['J'] = 36,
        ['K'] = 37,
        ['L'] = 38,
        ['M'] = 39,
        ['N'] = 40,
        ['O'] = 41,
        ['P'] = 42,
        ['Q'] = 43,
        ['R'] = 44,
        ['S'] = 45,
        ['T'] = 46,
        ['U'] = 47,
        ['V'] = 48,
        ['W'] = 49,
        ['X'] = 50,
        ['Y'] = 51,
        ['Z'] = 52,
    };
}