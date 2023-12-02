namespace aoc2023.day02;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var sample = new List<string>
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };
        
        var world = new World(sample, new Draw("12 red, 13 green, 14 blue"));
        world.Games.Count.ShouldBe(5);
        world.ValidGames().Count.ShouldBe(3);
        world.ValidGames().Sum(x=>x.GameID).ShouldBe(8);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day02");
        var world = new World(list, new Draw("12 red, 13 green, 14 blue"));
        world.ValidGames().Sum(x=>x.GameID).ShouldBe(2505);
    }

    [Test]
    public void ExamplePart2()
    {
        var sample = new List<string>
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };
        
        var world = new World(sample, new Draw("12 red, 13 green, 14 blue"));
        world.Power().ShouldBe(70265);
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day02");
        var world = new World(list, new Draw("12 red, 13 green, 14 blue"));
        world.Power().ShouldBe(2286);
    }
    
    private class World
    {
        private readonly Draw _maxDraw;
        
        public World(List<string> games, Draw maxDraw)
        {
            Games = games.Select(game => new Game(game)).ToList();
            _maxDraw = maxDraw;
        }

        public List<Game> Games { get; }
        
        public List<Game> ValidGames()
        {
            return Games.Where(game => game.Draws.All(draw => draw.Red <= _maxDraw.Red && draw.Green <= _maxDraw.Green && draw.Blue <= _maxDraw.Blue)).ToList();
        }
        
        public int Power()
        {
            return Games.Sum(game => game.Power());
        }
    }

    private class Game
    {
        public Game(string game)
        {
            var gameParts = game.Split(":");
            GameID = int.Parse(gameParts[0].Split(" ")[1]);
            Draws = gameParts[1].Split(";").Select(draw => new Draw(draw.Trim())).ToList();
        }
        public int GameID { get; }
        public List<Draw> Draws { get; }

        public int Power()
        {
            var red = Draws.Max(draw => draw.Red);
            var green = Draws.Max(draw => draw.Green);
            var blue = Draws.Max(draw => draw.Blue);
            
            return red * green * blue;
        }
    }

    private class Draw
    {
        public Draw(string draw)
        {
            foreach (var drawPart in draw.Split(","))
            {
                var drawPartParts = drawPart.Trim().Split(" ");
                var color = drawPartParts[1];
                var count = int.Parse(drawPartParts[0]);
                switch (color)
                {
                    case "red":
                        Red = count;
                        break;
                    case "green":
                        Green = count;
                        break;
                    case "blue":
                        Blue = count;
                        break;
                }
            }
        }
        
        public int Red { get; init; }
        public int Green { get; init; }
        public int Blue { get; init; }
    }
}