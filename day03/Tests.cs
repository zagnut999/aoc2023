namespace aoc2023.day03;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };

        var world = new World(list);
        world.MissingPartNumber().ShouldBe(4361);
    }
    
    [Test]
    public void Example_Ending()
    {
        var list = new List<string>
        {
            "...*",
            ".152",
            "...."
        };

        var world = new World(list);
        world.MissingPartNumber().ShouldBe(152);
    }
    
    [Test]
    public void Example_Beginning()
    {
        var list = new List<string>
        {
            "...*",
            "152.",
            "...."
        };

        var world = new World(list);
        world.MissingPartNumber().ShouldBe(152);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day03");
        var world = new World(list);
        world.MissingPartNumber().ShouldBe(535235);
    }

    [Test]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };
        
        var world = new World(list);
        world.GearRatios().ShouldBe(467835);
    }
    
    [Test]
    public void ExamplePart2_Basic()
    {
        var list = new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
        };
        
        var world = new World(list);
        world.GearRatios().ShouldBe(467*35);
    }
    
    [Test]
    public void ExamplePart2_Basic2()
    {
        var list = new List<string>
        {
            "..467",
            ".*...",
            "..35."
        };
        
        var world = new World(list);
        world.GearRatios().ShouldBe(467*35);
    }
    
    [Test]
    public void ExamplePart2_Basic3()
    {
        var list = new List<string>
        {
            ".....",
            "..2*2",
            "......"
        };
        
        var world = new World(list);
        world.GearRatios().ShouldBe(4);
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day03");
        
        
        var world = new World(list);
        world.GearRatios().ShouldBe(79844424);
    }
    
    private class World(List<string> list)
    {
        private readonly List<int> _partNumbers = new();
        private readonly string _numbers = "1234567890";

        private bool CheckNeighborsForIndicators(string currentNumber, int currentLine, int currentColumn)
        {
            var minWidth = currentColumn - currentNumber.Length - 1;
            minWidth = minWidth < 0 ? 0 : minWidth;
            var maxWidth = currentColumn;
            maxWidth = maxWidth > list[currentLine].Length - 1 ? list[currentLine].Length - 1 : maxWidth;
            var minHeight = currentLine - 1;
            minHeight = minHeight < 0 ? 0 : minHeight;
            var maxHeight = currentLine + 1;
            maxHeight = maxHeight > list.Count - 1 ? list.Count - 1 : maxHeight;
            
            for (var height = minHeight; height <= maxHeight; height++)
            {
                for (var width = minWidth; width <= maxWidth; width++)
                {
                    var c = list[height][width];
                    if (!(_numbers.Contains(c) || c == '.'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int MissingPartNumber()
        {
            for (var currentLine = 0; currentLine < list.Count; currentLine++)
            {
                var line = list[currentLine];
                var currentNumber = "";
                for (var currentColumn = 0; currentColumn < line.Length; currentColumn++)
                {
                    var c = line[currentColumn];
                    if (_numbers.Contains(c))
                    {
                        currentNumber += c;
                        if (currentColumn == line.Length - 1)
                        {
                            if (CheckNeighborsForIndicators(currentNumber, currentLine, currentColumn))
                                _partNumbers.Add(int.Parse(currentNumber));
                            currentNumber = "";
                        }
                    }
                    else
                    {
                        if (currentNumber.Length > 0)
                        {
                            if (CheckNeighborsForIndicators(currentNumber, currentLine, currentColumn))
                                _partNumbers.Add(int.Parse(currentNumber));
                            currentNumber = "";
                        }
                    }
                }
            }
            return _partNumbers.Sum();
        }
        
        private (int a, int b) CheckNeighborsForNumbers(int currentLine, int currentColumn)
        {
            var result = new Dictionary<Point, int>();
            var minWidth = currentColumn - 1;
            minWidth = minWidth < 0 ? 0 : minWidth;
            var maxWidth = currentColumn + 1;
            maxWidth = maxWidth > list[currentLine].Length - 1 ? list[currentLine].Length - 1 : maxWidth;
            var minHeight = currentLine - 1;
            minHeight = minHeight < 0 ? 0 : minHeight;
            var maxHeight = currentLine + 1;
            maxHeight = maxHeight > list.Count - 1 ? list.Count - 1 : maxHeight;
            
            for (var height = minHeight; height <= maxHeight; height++)
            {
                for (var width = minWidth; width <= maxWidth; width++)
                {
                    var c = list[height][width];
                    if (!_numbers.Contains(c)) continue;
                    
                    var (point, value) = FindNumber(height, width);
                    result.TryAdd(point, value);
                    
                    if (result.Keys.Count == 2)
                        return (result.First().Value, result.Last().Value);
                }
            }

            return (0, 0);
        }

        private (Point, int) FindNumber(int height, int width)
        {
            var line = list[height];
            var startWidth = width;
            var endWidth = width;
            var tempWidth = width;
            
            while (tempWidth > 0)
            {
                tempWidth--;
                if (_numbers.Contains(line[tempWidth]))
                {
                    startWidth = tempWidth;
                }
                else
                {
                    break;
                }
            }
            tempWidth = width;
            while (tempWidth < line.Length - 1)
            {
                tempWidth++;
                if (_numbers.Contains(line[tempWidth]))
                {
                    endWidth = tempWidth;
                }
                else
                {
                    break;
                }
            }
            
            return (Point.New(height, startWidth), int.Parse(line.Substring(startWidth, endWidth - startWidth + 1)));
        }

        public int GearRatios()
        {
            var sum = 0;
            for (var currentLine = 0; currentLine < list.Count; currentLine++)
            {
                var line = list[currentLine];
                for (var currentColumn = 0; currentColumn < line.Length; currentColumn++)
                {
                    var c = line[currentColumn];
                    if (c != '*') continue;
                    
                    var numbers = CheckNeighborsForNumbers(currentLine, currentColumn);
                    sum += numbers.a * numbers.b;
                }
            }
            return sum;
        }
    }
}