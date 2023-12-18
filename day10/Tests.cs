using System.Text.RegularExpressions;

namespace aoc2023.day10;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            ".....",
            ".S-7.",
            ".|.|.",
            ".L-J.",
            "....."
        };

        var world = new World(list);
        world.FurthestPoint().ShouldBe(4);
    }
    
    [Test]
    public void Example2()
    {
        var list = new List<string>
        {
            "..F7.",
            ".FJ|.",
            "SJ.L7",
            "|F--J",
            "LJ..."
        };

        var world = new World(list);
        world.FurthestPoint().ShouldBe(8);
    }
    
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day10");
        var world = new World(list);
        world.FurthestPoint().ShouldBe(4);
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
        var list = await Utilities.ReadInputByDay("DayXX");
        var world = new World(list);
    }

    private class World
    {
        private readonly List<string> _map;
        public World(List<string> list)
        {
            _map = list;
        }

        public int FurthestPoint()
        {
            var start = FindStart();
            var (first, _) = FindFirstMoves(start);
            var count = 1;
            
            var position = Move(start, first);
            var direction = GetDirection(_map[position.Y][position.X], first);
            
            while (true)
            {
                count++;
                position = Move(position, direction);
                if (_map[position.Y][position.X] == 'S')
                    break;
                direction = GetDirection(_map[position.Y][position.X], direction);
            }

            return (int) Math.Round(1.0*count/2);
        }

        private Point Move(Point current, Direction facing)
        {
            switch (facing)
            {
                case Direction.East:
                    return new Point(current.X + 1, current.Y);
                case Direction.West:
                    return new Point(current.X - 1, current.Y);
                case Direction.North:
                    return new Point(current.X, current.Y - 1);
                case Direction.South:
                    return new Point(current.X, current.Y + 1);
            }

            throw new Exception("Invalid direction");
        }

        private (Direction First, Direction Second) FindFirstMoves(Point start)
        {
            var directions = new List<Direction>();
            
            if (start.X + 1 < _map[0].Length && new[] { '-', 'J', '7' }.Contains(_map[start.Y][start.X + 1]))
                directions.Add(Direction.East);
            if (start.X - 1 >= 0 && new[] { '-', 'F', 'L' }.Contains(_map[start.Y][start.X - 1]))
                directions.Add(Direction.West);
            if (start.X + 1 < _map.Count && new[] { '|', 'J', 'L' }.Contains(_map[start.Y+1][start.X]))
                directions.Add(Direction.South);
            if (start.Y - 1 >= 0 && new[] { '|', 'F', '7' }.Contains(_map[start.Y-1][start.X]))
                directions.Add(Direction.North);
            
            return (directions[0], directions[1]);
        }

        private Point FindStart()
        {
            for (var y = 0; y < _map.Count; y++)
            {
                var row = _map[y];
                for (var x = 0; x < row.Length; x++)
                {
                    if (row[x] == 'S')
                        return new Point(x, y);
                }
            }

            throw new Exception("Start not found");
        }

        private Direction GetDirection(char symbol, Direction facing)
        {
            switch (symbol)
            {
                case '|':
                    return facing;
                case '-':
                    return facing;
                case 'L':
                    return facing == Direction.West ? Direction.North : Direction.East;
                case 'J':
                    return facing == Direction.East ? Direction.North : Direction.West;
                case '7':
                    return facing == Direction.East ? Direction.South : Direction.West;
                case 'F':
                    return facing == Direction.West ? Direction.South : Direction.East;
                
                default:
                    throw new Exception($"Unknown symbol {symbol}");
            }
        }
        
        public enum Direction
        {
            North,
            South,
            East,
            West
        }
    }
}