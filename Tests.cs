using System.Text.RegularExpressions;

namespace aoc2023.dayXX;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            
        };

        var world = new World(list);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("DayXX");
        var world = new World(list);
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
        public World(List<string> list)
        {
            
        }

    }
}