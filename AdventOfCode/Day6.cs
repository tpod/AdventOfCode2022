public class Day6
{
    public static int Part2()
    {
        var dataStream = File.ReadAllText(@$"{Environment.CurrentDirectory}/input6.txt").ToCharArray();

        var marker = new LinkedList<char>();
        var count = 0;
        foreach (var c in dataStream)
        {
            count++;
            marker.AddLast(c);
            if (marker.Count == 15)
            {
                marker.RemoveFirst();
                
                if(IsUniqueMarker(marker))
                    return count;
            }
        }
        
        return count;
    }
    
    public static int Part1()
    {
        var dataStream = File.ReadAllText(@$"{Environment.CurrentDirectory}/input6.txt").ToCharArray();

        var marker = new LinkedList<char>();
        var count = 0;
        foreach (var c in dataStream)
        {
            count++;
            marker.AddLast(c);
            if (marker.Count == 5)
            {
                marker.RemoveFirst();
                
                if(IsUniqueMarker(marker))
                    return count;
            }
        }
        
        return count;
    }

    private static bool IsUniqueMarker(LinkedList<char> marker)
    {
        return marker.All(c => marker.Count(c1 => c1 == c) <= 1);
    }
}