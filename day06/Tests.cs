using System.Text;

namespace aoc2023.day06;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };

        var world = new World(list);
        world.ShouldNotBeNull();
        world.RaceThemAll().ShouldBe(288);
    }

    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day06");
        var world = new World(list);
        world.RaceThemAll().ShouldBe(4811940);
    }
    
    [Test]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };
    
        var world = new World(list);
        world.RaceThemAll2().ShouldBe(71503);
        
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day06");
        var world = new World(list);
        world.RaceThemAll2().ShouldBe(30077773);
    }

    private class World
    {
        private readonly List<Stats> _races = new();
        
        public World(List<string> list)
        {
            var times = list[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            var distances = list[1].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            
            for (var i = 0; i < times.Count; i++)
            {
                _races.Add(new Stats(times[i], distances[i]));
            }
        }

        public int RaceThemAll()
        {
            var marginOfError = 1;
            foreach (var race in _races)
            {
                marginOfError *= RaceOne(race);
            }

            return marginOfError;
        }

        public int RaceThemAll2()
        {
            var timesString = new StringBuilder();
            var distancesString = new StringBuilder();
            foreach (var race in _races)
            {
                timesString.Append(race.Time.ToString());
                distancesString.Append(race.Distance.ToString());
            }

            var bigRace = new Stats(long.Parse(timesString.ToString()), long.Parse(distancesString.ToString()));
            return RaceOne(bigRace);
        }

        private int RaceOne(Stats race)
        {
            var numWinners = 0;
            for (var time = 0; time < race.Time; time++)
            {
                var speed = time;
                var runTime = race.Time - time;
                var distance = speed * runTime;
                if (distance > race.Distance)
                {
                    numWinners++;
                }
            }
            return numWinners;
        }

        private record Stats(long Time, long Distance);
    }
}