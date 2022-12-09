namespace AdventOfCode;

public static class Day2
{
    public static int Part2()
    {
        var score = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input2.txt"))
        {
            if (string.IsNullOrEmpty(line)) continue;
            
            var row = line.Split(' ');
            var game = new Game2(row[0], row[1]);
            score += game.GetPoints();
        }

        return score;
    }
    
    public static int Part1()
    {
        var score = 0;
        foreach (var line in File.ReadLines(@$"{Environment.CurrentDirectory}/input2.txt"))
        {
            if (string.IsNullOrEmpty(line)) continue;
            
            var row = line.Split(' ');
            var game = new Game(row[0], row[1]);
            score += game.GetPoints();
        }

        return score;
    }

    class Game2
    {
        public Hand HandA { get; }
        public Result Result { get; }

        public Game2(string handA, string result)
        {
            HandA = ToHand(handA);
            Result = ToResult(result);
        }

        public int GetPoints()
        {
            return Points.PointsForGameResult(Result) + Points.PointsForHand(GetHand());
        }

        private Hand GetHand()
        {
            if(Result == Result.Draw) return HandA;
            if(HandA == Hand.Paper && Result == Result.Win) return Hand.Scissors;
            if(HandA == Hand.Paper && Result == Result.Lose) return Hand.Rock;
            if(HandA == Hand.Rock && Result == Result.Win) return Hand.Paper;
            if(HandA == Hand.Rock && Result == Result.Lose) return Hand.Scissors;
            if(HandA == Hand.Scissors && Result == Result.Win) return Hand.Rock;
            if(HandA == Hand.Scissors && Result == Result.Lose) return Hand.Paper;
            throw new Exception("Invalid hand");
        }
        
        private Hand ToHand(string handA)
        {
            return handA switch
            {
                "A" => Hand.Rock,
                "B" => Hand.Paper,
                "C" => Hand.Scissors,
                _ => throw new Exception("Invalid hand")
            };
        }

        private Result ToResult(string result)
        {
            return result switch
            {
                "X" => Result.Lose,
                "Y" => Result.Draw,
                "Z" => Result.Win,
                _ => throw new Exception("Invalid hand")
            };
        }
    }

    class Game
    {
        private Hand HandA { get; }
        private Hand HandB { get; }

        public Game(string handA, string handB)
        {
            HandA = ToHand(handA);
            HandB = ToHand(handB);
        }
        
        public int GetPoints()
        {
            return Points.PointsForGameResult(Play()) + Points.PointsForHand(HandB);
        }

        private Result Play()
        {
            if (HandA == HandB) return Result.Draw;
            if (HandA == Hand.Rock && HandB == Hand.Scissors) return Result.Lose;
            if (HandA == Hand.Rock && HandB == Hand.Paper) return Result.Win;
            if (HandA == Hand.Scissors && HandB == Hand.Rock) return Result.Win;
            if (HandA == Hand.Scissors && HandB == Hand.Paper) return Result.Lose;
            if (HandA == Hand.Paper && HandB == Hand.Rock) return Result.Lose;
            if (HandA == Hand.Paper && HandB == Hand.Scissors) return Result.Win;
            throw new Exception("Unknown hand");
        }

        private Hand ToHand(string handA)
        {
            return handA switch
            {
                "A" => Hand.Rock,
                "B" => Hand.Paper,
                "C" => Hand.Scissors,
                "X" => Hand.Rock,
                "Y" => Hand.Paper,
                "Z" => Hand.Scissors,
                _ => throw new Exception("Invalid hand")
            };
        }
    }

    enum Hand
    {
        Rock,
        Paper,
        Scissors
    }

    enum Result
    {
        Win,
        Lose,
        Draw
    }

    static class Points
    {
        public static int PointsForGameResult(Result result)
        {
            return result switch
            {
                Result.Win => 6,
                Result.Lose => 0,
                Result.Draw => 3,
                _ => throw new Exception("Invalid result")
            };
        }

        public static int PointsForHand(Hand hand)
        {
            return hand switch
            {
                Hand.Rock => 1,
                Hand.Paper => 2,
                Hand.Scissors => 3,
                _ => throw new Exception("Invalid hand")
            };
        }
    }
}