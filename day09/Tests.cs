using System.Text.RegularExpressions;

namespace aoc2023.day09;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        };

        var world = new World(list);
        world.NextValuesSum().ShouldBe(114);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day09");
        var world = new World(list);
        world.NextValuesSum().ShouldBe(1647269739);
    }
    
    [Test]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        };

        var world = new World(list);
        world.NextValuesSumPart2().ShouldBe(2);
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day09");
        var world = new World(list);
        world.NextValuesSumPart2().ShouldBe(864);
    }

    private class World
    {
        private readonly List<List<int>> _list = new();
        public World(List<string> list)
        {
            
            foreach (var line in list)
            {
                _list.Add(line.Split(' ').Select(int.Parse).ToList());
            }
        }

        public int NextValuesSum()
        {
            var sum = 0;
            foreach (var line in _list)
            {
                var nextValue = CalculateNextValue(line);
                sum += nextValue;
            }
            return sum;
        }

        private int CalculateNextValue(List<int> line)
        {
            var values = new List<List<int>> { line };

            var current = line;
            while (values.Last().Sum() != 0)
            {
                var newLine = new List<int>();
                for(var i =0; i < current.Count - 1; i++)
                {
                    var value = current[i];
                    var next = current[i+1];
                    
                    newLine.Add(next - value);
                }

                values.Add(newLine);
                current = newLine;
            }
            
            current = values.Last();
            current.Add(0);
            for (var i = values.Count - 1; i > 0;  i--)
            {
                var previous = values[i-1];
                previous.Add(current.Last() + previous.Last());
                current = previous;
            }

            return values[0].Last();
        }
        
        public int NextValuesSumPart2()
        {
            var sum = 0;
            foreach (var line in _list)
            {
                var nextValue = CalculateNextValuePart2(line);
                sum += nextValue;
            }
            return sum;
        }
        
        private int CalculateNextValuePart2(List<int> line)
        {
            var values = new List<List<int>> { line };

            var current = line;
            while (values.Last().Sum() != 0)
            {
                var newLine = new List<int>();
                for(var i =0; i < current.Count - 1; i++)
                {
                    var value = current[i];
                    var next = current[i+1];
                    
                    newLine.Add(next - value);
                }

                values.Add(newLine);
                current = newLine;
            }
            
            current = values.Last();
            current.Add(0);
            for (var i = values.Count - 1; i > 0;  i--)
            {
                var previous = values[i-1];
                previous.Insert(0, previous.First() - current.First());
                current = previous;
            }

            return values[0].First();
        }
    }
}