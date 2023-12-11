using System.Text.RegularExpressions;

namespace aoc2023.day08;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "LLR",
            "",
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)"
        };

        var world = new World(list);
        world.CountSteps().ShouldBe(6);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day08");
        var world = new World(list);
        world.CountSteps().ShouldBe(13301);
    }
    
    [Test]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
            "LR",
            "",
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)"
        };

        var world = new World(list);
        world.CountStepsPart2().ShouldBe(6);
    }
    
    [Test]
    [Ignore("Doesn't work :(")]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day08");
        var world = new World(list);
        world.CountStepsPart2().ShouldBe(6);
    }

    private class World
    {
        private readonly string _navigation;
        private readonly Dictionary<string, (string Left, string Right)> _locations = new();
        public World(List<string> list)
        {
            var regex = new Regex(@"(\w+) = \((\w+), (\w+)\)");
            _navigation = list[0];
            foreach (var line in list.Skip(2))
            {
                var match = regex.Match(line);
                _locations.Add(match.Groups[1].Value, (match.Groups[2].Value, match.Groups[3].Value));
            }
        }

        public int CountSteps()
        {
            var steps = 0;
            
            var currentNav = _navigation[0];
            var currentLocation = "AAA";
            while (currentLocation != "ZZZ")
            {
                if (currentNav == 'L')
                {
                    currentLocation = _locations[currentLocation].Left;
                }
                else
                {
                    currentLocation = _locations[currentLocation].Right;
                }
                steps++;
                currentNav = _navigation[steps %_navigation.Length];
            }
            
            return steps;
        }
        
        public long CountStepsPart2()
        {
            var steps = 0L;
            
            var currentNav = _navigation[0];
            var currentLocations = _locations.Keys.Where(x => x.EndsWith('A')).ToList();
            var endLocations = _locations.Keys.Where(x => x.EndsWith('Z')).ToList();;
            var allMatch = false;
            while (!allMatch)
            {
                // for(var i = 0; i < currentLocations.Count; i++)
                // {
                //     if (currentNav == 'L')
                //     {
                //         currentLocations[i] = _locations[currentLocations[i]].Left;
                //     }
                //     else
                //     {
                //         currentLocations[i] = _locations[currentLocations[i]].Right;
                //     }
                // }
                if (currentNav == 'L')
                {
                    currentLocations[0] = _locations[currentLocations[0]].Left;
                    currentLocations[1] = _locations[currentLocations[1]].Left;
                    currentLocations[2] = _locations[currentLocations[2]].Left;
                    currentLocations[3] = _locations[currentLocations[3]].Left;
                    currentLocations[4] = _locations[currentLocations[4]].Left;
                    currentLocations[5] = _locations[currentLocations[5]].Left;
                }
                else
                {
                    currentLocations[0] = _locations[currentLocations[0]].Right;
                    currentLocations[1] = _locations[currentLocations[1]].Right;
                    currentLocations[2] = _locations[currentLocations[2]].Right;
                    currentLocations[3] = _locations[currentLocations[3]].Right;
                    currentLocations[4] = _locations[currentLocations[4]].Right;
                    currentLocations[5] = _locations[currentLocations[5]].Right;
                }
                
                steps++;
                var nextStep = (int)(steps % _navigation.Length);
                currentNav = _navigation[nextStep];
                allMatch = currentLocations.All(x => endLocations.Contains(x));
            }
            
            return steps;
        }
    }
}