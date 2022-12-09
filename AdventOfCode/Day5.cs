/*
 *
    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2

 */

using System.Text;

public class Day5
{ 
    public static string Part2()
    {
        var matrix = GetMatrix();

        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input5.txt"))
        {
            if(!line.StartsWith("move")) continue;

            var parts = line
                .Replace("move", "")
                .Replace("from", "")
                .Replace("to", "")
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            var move = new Move(parts[0], parts[1]-1, parts[2]-1);

            var temp = new List<char>();
            for (int i = 0; i < move.Amount; i++)
            {
                var from = matrix[move.From].Last();
                matrix[move.From].RemoveAt(matrix[move.From].Count - 1);
                temp = temp.Prepend(from).ToList();
            }

            matrix[move.To].AddRange(temp);
        }

        var result = new StringBuilder();
        foreach (var linkedList in matrix)
        {
            result.Append(linkedList?.Last());
        }
        
        return result.ToString();
    }
    
    public static string Part1()
    {
        var matrix = GetMatrix();

        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input5.txt"))
        {
            if(!line.StartsWith("move")) continue;

            var parts = line
                .Replace("move", "")
                .Replace("from", "")
                .Replace("to", "")
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            var move = new Move(parts[0], parts[1]-1, parts[2]-1);

            for (int i = 0; i < move.Amount; i++)
            {
                var from = matrix[move.From].Last();
                matrix[move.From].RemoveAt(matrix[move.From].Count - 1);
                
                matrix[move.To].Add(from);
            }
        }

        var result = new StringBuilder();
        foreach (var linkedList in matrix)
        {
            result.Append(linkedList?.Last());
        }
        
        return result.ToString();
    }

    private record Move(int Amount, int From, int To);
    
    private static List<char>[] GetMatrix()
    {
        var (height, width) = GetMatrixSize();

        var matrix = new List<char>[width];
        for (var i = 0; i < width; i++)
        {
            matrix[i] = new List<char>();
        }

        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input5.txt"))
        {
            if (line.Trim().StartsWith('1'))
                break;

            var viewIndex = 1;
            var matrixIndex = 0;
            var row = line.ToCharArray();
            while (true)
            {
                if (matrixIndex == width)
                    break;

                if (!char.IsSeparator(row[viewIndex]))
                {
                    matrix[matrixIndex] = matrix[matrixIndex].Prepend(row[viewIndex]).ToList();
                }

                viewIndex += 4;
                matrixIndex++;
            }
        }

        return matrix;
    }


    private static (int height, int width) GetMatrixSize()
    {
        var height = 0;
        var width = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input5.txt"))
        {
            if (line.Trim().StartsWith('1'))
            {
                width = int.Parse(line.Trim().LastOrDefault().ToString());
                break;
            }

            height++;
        }

        return (height, width);
    }
}