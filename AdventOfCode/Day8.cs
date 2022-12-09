public class Day8
{
    public static int Part2()
    {
        var grid = GetGrid();

        var highestScore = 0;
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                var self = grid[y, x].Height;

                //Look up
                var upScore = 0;
                for (var j = 1; j < grid.GetLength(1); j++)
                {
                    if (AddToScore(grid, y - j, x, self, ref upScore)) break;
                }

                //Look down
                var downScore = 0;
                for (var j = 1; j < grid.GetLength(1); j++)
                {
                    if (AddToScore(grid, y + j, x, self, ref downScore)) break;
                }

                //Look left
                var leftScore = 0;
                for (var i = 1; i < grid.GetLength(0); i++)
                {
                    if (AddToScore(grid, y, x - i, self, ref leftScore)) break;
                }

                //Look right
                var rightScore = 0;
                for (var i = 1; i < grid.GetLength(0); i++)
                {
                    if (AddToScore(grid, y, x + i, self, ref rightScore)) break;
                }

                var totalScore = upScore * downScore * leftScore * rightScore;
                if(totalScore > highestScore)
                {
                    highestScore = totalScore;
                }
            }
        }

        return highestScore;
    }

    private static bool AddToScore(Tree[,] grid, int y, int x, int self, ref int score)
    {
        try
        {
            if (grid[y, x].Height > self)
            {
                score++;
                return true;
            }
            else if (grid[y, x].Height == self)
            {
                score++;
                return true;
            }
            else
            {
                score++;
                return false;
            }
        }
        catch (Exception)
        {
            return true;
        }
    }

    public static int Part1()
    {
        var grid = GetGrid();

        //Look down
        for (var j = 0; j < grid.GetLength(1); j++)
        {
            var highest = -1;
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                if (grid[i, j].Height > highest)
                {
                    highest = grid[i, j].Height;
                    grid[i, j].Visible = true;
                }
            }
        }
        
        //Look up
        for (var j = 0; j < grid.GetLength(1); j++)
        {
            var highest = -1;
            for (var i = grid.GetLength(0) - 1; i >= 0; i--)
            {
                if (grid[i, j].Height > highest)
                {
                    highest = grid[i, j].Height;
                    grid[i, j].Visible = true;
                }
            }
        }
        
        //Look left
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            var highest = -1;
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j].Height > highest)
                {
                    highest = grid[i, j].Height;
                    grid[i, j].Visible = true;
                }
            }
        }
        
        //Look right
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            var highest = -1;
            for (var j = grid.GetLength(1) - 1; j >= 0; j--)
            {
                if (grid[i, j].Height > highest)
                {
                    highest = grid[i, j].Height;
                    grid[i, j].Visible = true;
                }
            }
        }
        
        var result = 0;
        Console.WriteLine();
        for (var j = 0; j < grid.GetLength(1); j++)
        {
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                if (grid[j, i].Visible)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    result++;
                }
                else
                    Console.ForegroundColor = ConsoleColor.Gray;
                
                Console.Write(grid[j, i].Height + " ");  
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.ResetColor();
        
        return result;
    }

    private static Tree[,] GetGrid()
    {
        Tree[,]? grid = null;
        var currentRow = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input8.txt"))
        {
            var row = line.ToCharArray();

            grid ??= new Tree[row.Length, row.Length];

            var currentColumn = 0;
            foreach (var c in row)
            {
                grid[currentRow, currentColumn] = new Tree(int.Parse(c.ToString()));
                currentColumn++;
            }

            currentRow++;
        }

        return grid!;
    }

    private class Tree
    {
        public Tree(int height)
        {
            Height = height;
        }

        public int Height { get; }
        public bool Visible { get; set; }
    }
}