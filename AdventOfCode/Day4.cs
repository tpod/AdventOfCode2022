public class Day4
{
    public static int Part1()
    {
        var sum = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input4.txt"))
        {
            if (string.IsNullOrEmpty(line)) continue;

            var pair = new AssignmentPair(line);

            if (pair.FullyContained())
            {
                sum++;
            }
        }

        return sum;
    }
    
    public static int Part2()
    {
        var sum = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input4.txt"))
        {
            if (string.IsNullOrEmpty(line)) continue;

            var pair = new AssignmentPair(line);

            if (pair.AnyOverLap())
            {
                sum++;
            }

        }

        return sum;
    }

    private class AssignmentPair
    {
        public Assignment First { get; }
        public Assignment Second { get; }

        public AssignmentPair(string line)
        {
            var parts = line.Split(",");
            First = new Assignment(parts[0]);
            Second = new Assignment(parts[1]);
        }
        
        public bool FullyContained()
        {
            if(First.SectionFrom >= Second.SectionFrom && First.SectionTo <= Second.SectionTo)
                return true;
            if(Second.SectionFrom >= First.SectionFrom && Second.SectionTo <= First.SectionTo)
                return true;
            
            return false;
        }
        
        public bool AnyOverLap()
        {
            if(First.SectionFrom >= Second.SectionFrom && First.SectionFrom <= Second.SectionTo)
                return true;
            if(First.SectionTo >= Second.SectionFrom && First.SectionTo <= Second.SectionTo)
                return true;
            if(Second.SectionFrom >= First.SectionFrom && Second.SectionFrom <= First.SectionTo)
                return true;
            if(Second.SectionTo >= First.SectionFrom && Second.SectionTo <= First.SectionTo)
                return true;

            return false;
        }
    }
    
    private class Assignment
    {
        public int SectionFrom { get; }
        public int SectionTo { get; }

        public Assignment(string line)
        {
            var parts = line.Split("-");
            SectionFrom = int.Parse(parts[0]);
            SectionTo = int.Parse(parts[1]);
        }
    }
}