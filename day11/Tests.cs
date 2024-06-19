namespace aoc2023.day11;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        };

        var world = new World(list);
        world.Expand();
        var result = world.SumOfDistances();
        result.ShouldBe(374);
    }
    
    [Test]
    public void Example_Expand()
    {
        var list = new List<string>
        {
            "#...",
            "....",
            "..#.",
            "...#"
        };

        var world = new World(list);
        world.Expand();
        world.Points.Any(point=> point.Equals(new Point(0,0))).ShouldBeTrue();
        world.Points.Any(point=> point.Equals(new Point(3,3))).ShouldBeTrue();
        world.Points.Any(point=> point.Equals(new Point(4,4))).ShouldBeTrue();
        world.ManhattanDistance(new Point(0,0), new Point(3,3)).ShouldBe(6);
        world.SumOfDistances().ShouldBe(16);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day11");
        var world = new World(list);
        world.Expand();
        var result = world.SumOfDistances();
        result.ShouldBe(374);
    }
    
    [Test]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
        };

        var world = new World(list);
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day11");
        var world = new World(list);
    }

    private class World
    {
        public readonly List<Point> Points = new();
        public World(List<string> list)
        {
            var y = 0;
            foreach (var line in list)
            {
                var x = 0;
                foreach (var c in line)
                {
                    if (c == '#')
                    {
                        Points.Add(new Point(x, y));
                    }

                    x++;
                }

                y++;
            }
        }

        public void Expand()
        {
            var maxX = Points.Max(p => p.X);
            var maxY = Points.Max(p => p.Y);
            
            for (var x=0;x<=maxX+1;x++)
            {
                if (Points.All(point => point.X != x))
                {
                    Points.Where(point=> point.X > x).ToList().ForEach(point => point.X++);
                    x++;
                }
            }
            for (var y=0;y<=maxY+1;y++)
            {
                if (Points.All(point => point.Y != y))
                {
                    Points.Where(point=> point.Y > y).ToList().ForEach(point => point.Y++);
                    y++;
                }
            }
        }
        
        public int ManhattanDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }
        
        public int SumOfDistances()
        {
            var sum = 0;
            
            for (var i = 0; i < Points.Count; i++)
            {
                var p1 = Points[i];
                for (var j = i + 1; j < Points.Count; j++)
                {
                    var p2 = Points[j];
                    var distance = ManhattanDistance(p1, p2);

                    sum += distance;
                }
            }

            return sum;
        }
    }
}