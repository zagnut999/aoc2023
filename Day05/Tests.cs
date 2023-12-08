namespace aoc2023.day05;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4"
        };

        var world = new World(list);
        world.ShouldNotBeNull();
        world.GetLocation(79).ShouldBe(82);
        world.GetClosestLocation().ShouldBe(35);
    }

    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day05");
        var world = new World(list);
        world.GetClosestLocation().ShouldBe(323142486L);
    }

    [Test]
    [Ignore("No idea.")]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4"
        };

        var world = new World(list);
        world.GetClosestLocationPart2().ShouldBe(46);
    }

    [Test]
    public void ExamplePart2GetDestinationPart2_StartsOutEndsIn()
    {
        var list = new List<string>
        {
            "seeds: 1 5",
            "",
            "seed-to-soil map:",
            "10 3 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(2);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(3);
        result[1].Start.ShouldBe(1);
        result[1].Length.ShouldBe(2);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_StartsOutEndsIn2()
    {
        var list = new List<string>
        {
            "seeds: 1 5",
            "",
            "seed-to-soil map:",
            "10 5 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(2);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(1);
        result[1].Start.ShouldBe(1);
        result[1].Length.ShouldBe(4);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_StartsOutEndsIn3()
    {
        var list = new List<string>
        {
            "seeds: 4 2",
            "",
            "seed-to-soil map:",
            "10 5 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(2);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(1);
        result[1].Start.ShouldBe(4);
        result[1].Length.ShouldBe(1);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_StartsInEndsOut()
    {
        var list = new List<string>
        {
            "seeds: 5 5",
            "",
            "seed-to-soil map:",
            "10 1 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(2);
        result[0].Start.ShouldBe(14);
        result[0].Length.ShouldBe(1);
        result[1].Start.ShouldBe(6);
        result[1].Length.ShouldBe(4);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_Before()
    {
        var list = new List<string>
        {
            "seeds: 1 5",
            "",
            "seed-to-soil map:",
            "10 6 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(1);
        result[0].Start.ShouldBe(1);
        result[0].Length.ShouldBe(5);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_After()
    {
        var list = new List<string>
        {
            "seeds: 10 5",
            "",
            "seed-to-soil map:",
            "10 1 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(1);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(5);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_Exact()
    {
        var list = new List<string>
        {
            "seeds: 1 5",
            "",
            "seed-to-soil map:",
            "10 1 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(1);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(5);
    }

    [Test]
    public void ExamplePart2GetDestinationPart2_TwoMaps()
    {
        var list = new List<string>
        {
            "seeds: 1 10",
            "",
            "seed-to-soil map:",
            "10 1 5",
            "15 6 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(2);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(5);
        result[1].Start.ShouldBe(15);
        result[1].Length.ShouldBe(5);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_TwoMapsWithAGap()
    {
        var list = new List<string>
        {
            "seeds: 1 10",
            "",
            "seed-to-soil map:",
            "10 1 5",
            "15 7 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(3);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(5);
        result[1].Start.ShouldBe(15);
        result[1].Length.ShouldBe(4);
        result[2].Start.ShouldBe(6);
        result[2].Length.ShouldBe(1);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_CompleteOverlap()
    {
        var list = new List<string>
        {
            "seeds: 1 10",
            "",
            "seed-to-soil map:",
            "10 3 5"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(3);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(5);
        result[2].Start.ShouldBe(1);
        result[2].Length.ShouldBe(2);
        result[1].Start.ShouldBe(8);
        result[1].Length.ShouldBe(3);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_CompletelyContained()
    {
        var list = new List<string>
        {
            "seeds: 2 5",
            "",
            "seed-to-soil map:",
            "10 1 10"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(1);
        result[0].Start.ShouldBe(11);
        result[0].Length.ShouldBe(5);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_CompletelyContained2()
    {
        var list = new List<string>
        {
            "seeds: 1 5",
            "",
            "seed-to-soil map:",
            "10 1 10"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(1);
        result[0].Start.ShouldBe(10);
        result[0].Length.ShouldBe(5);
    }
    
    [Test]
    public void ExamplePart2GetDestinationPart2_CompletelyContained3()
    {
        var list = new List<string>
        {
            "seeds: 5 5",
            "",
            "seed-to-soil map:",
            "10 1 10"
        };

        var world = new World(list);
        var seedRange = new Range (world.Seeds[0], world.Seeds[1]);
        var result = world.GetDestinationPart2(world.SeedToSoil, [seedRange]);
        result.Count.ShouldBe(1);
        result[0].Start.ShouldBe(14);
        result[0].Length.ShouldBe(5);
        
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day05");
        var world = new World(list);
        world.GetClosestLocationPart2().ShouldBe(79874951L);
    }
    
    private class World
    {
        public List<long> Seeds { get; }
        public List<RangeMap> SeedToSoil { get; } = new();
        private List<RangeMap> SoilToFertilizer { get; } = new();
        private List<RangeMap> FertilizerToWater { get; } = new();
        private List<RangeMap> WaterToLight { get; } = new();
        private List<RangeMap> LightToTemperature { get; } = new();
        private List<RangeMap> TemperatureToHumidity { get; } = new();
        private List<RangeMap> HumidityToLocation { get; } = new();

        public World(List<string> list)
        {
            Seeds = list[0].Split(": ")[1].Split(" ").Select(long.Parse).ToList();

            List<RangeMap> currentCollection = SeedToSoil;
            foreach (var line in list[1..])
            {
                if (string.IsNullOrEmpty(line)) continue;

                switch (line)
                {
                    case "seed-to-soil map:":
                        currentCollection = SeedToSoil;
                        break;
                    case "soil-to-fertilizer map:":
                        currentCollection = SoilToFertilizer;
                        break;
                    case "fertilizer-to-water map:":
                        currentCollection = FertilizerToWater;
                        break;
                    case "water-to-light map:":
                        currentCollection = WaterToLight;
                        break;
                    case "light-to-temperature map:":
                        currentCollection = LightToTemperature;
                        break;
                    case "temperature-to-humidity map:":
                        currentCollection = TemperatureToHumidity;
                        break;
                    case "humidity-to-location map:":
                        currentCollection = HumidityToLocation;
                        break;
                    default:
                    {
                        var split = line.Split(" ");
                        var destination = long.Parse(split[0]);
                        var source = long.Parse(split[1]);
                        var length = long.Parse(split[2]);
                        
                        currentCollection.Add(new RangeMap(source, destination, length));
                        currentCollection.Sort((x, y) => x.Source.CompareTo(y.Source));
                        
                        break;
                    }
                }
            }
        }

        public long GetClosestLocation()
        {
            var closest = long.MaxValue;
            Seeds.ForEach(x=> closest = Math.Min(closest, GetLocation(x)));
            return closest;
        }
        
        public long GetLocation(long seed)
        {
            var soil = GetDestination(SeedToSoil, seed);
            var fertilizer = GetDestination(SoilToFertilizer, soil);
            var water = GetDestination(FertilizerToWater, fertilizer);
            var light = GetDestination(WaterToLight, water);
            var temperature = GetDestination(LightToTemperature, light);
            var humidity = GetDestination(TemperatureToHumidity, temperature);
            var location = GetDestination(HumidityToLocation, humidity);

            return location;
        }

        private long GetDestination(List<RangeMap> range, long source)
        {
            var destination = source;
            foreach (var rangeMap in range)
            {
                if (rangeMap.Source <= source && source < rangeMap.Source + rangeMap.Length)
                {
                    destination = rangeMap.Destination + (source - rangeMap.Source);
                }
            }

            return destination;
        }
        public long GetClosestLocationPart2()
        {
            var seedRanges = new List<Range>();
            for(var i = 0; i < Seeds.Count; i+=2)
            {
                var seed = Seeds[i];
                var length = Seeds[i + 1];
                seedRanges.Add(new Range(seed, length));
            }
            
            var closest = long.MaxValue;
            seedRanges.ForEach(x => closest = Math.Min(closest, GetLocationPart2(x)));
            
            return closest;
        }
        
        public long GetLocationPart2(Range seedRange)
        {
            var soil = GetDestinationPart2(SeedToSoil, [seedRange]);
            var fertilizer = GetDestinationPart2(SoilToFertilizer, soil);
            var water = GetDestinationPart2(FertilizerToWater, fertilizer);
            var light = GetDestinationPart2(WaterToLight, water);
            var temperature = GetDestinationPart2(LightToTemperature, light);
            var humidity = GetDestinationPart2(TemperatureToHumidity, temperature);
            var location = GetDestinationPart2(HumidityToLocation, humidity);

            return location.Min(x=>x.Start);
        }
        
        public List<Range> GetDestinationPart2(List<RangeMap> rangeMaps, List<Range> sources)
        {
            var destination = new List<Range>();
            
            foreach (var outerSource in sources)
            {
                var pieces = new Stack<Range>();
                pieces.Push(outerSource);
                
                while (pieces.Count > 0)
                {
                    var source = pieces.Pop();
                    var notFound = true;
                    foreach (var rangeMap in rangeMaps)
                    {
                        //if exact match
                        if (rangeMap.Source == source.Start && rangeMap.Length == source.Length)
                        {
                            destination.Add(new Range(rangeMap.Destination, rangeMap.Length));
                            notFound = false;
                            break;
                        }
                        
                        //if it ends outside on the near side or starts outside on the far side
                        if ((rangeMap.Source >= source.Start + source.Length) || (rangeMap.Source + rangeMap.Length < source.Start))
                        {
                            continue;
                        }
                        
                        //if it starts in outside and ends inside
                        if ((rangeMap.Source > source.Start) &&
                                 (rangeMap.Source <= source.Start + source.Length && source.Start + source.Length < rangeMap.Source + rangeMap.Length))
                        {
                            // add the inside part in to destination
                            destination.Add(new Range(rangeMap.Destination, source.Start + source.Length - rangeMap.Source));
                            
                            // add the outside part in to pieces
                            pieces.Push(new Range(source.Start, rangeMap.Source - source.Start));
                            notFound = false;
                            break;
                        }
                        
                        //if it starts inside and ends inside
                        if ((rangeMap.Source <= source.Start && source.Start < rangeMap.Source + rangeMap.Length) &&
                            (rangeMap.Source <= source.Start + source.Length && source.Start + source.Length < rangeMap.Source + rangeMap.Length))
                        {
                            // add to destination
                            destination.Add(new Range(rangeMap.Destination + (source.Start - rangeMap.Source), source.Length));
                            notFound = false;
                            break;
                        }
                        
                        //if it starts inside and ends outside
                        if ((rangeMap.Source <= source.Start && source.Start < rangeMap.Source + rangeMap.Length) &&
                                 (source.Start + source.Length > rangeMap.Source + rangeMap.Length))
                        {
                            // add the inside part in to destination
                            destination.Add(new Range(rangeMap.Destination + (source.Start - rangeMap.Source), rangeMap.Length - (source.Start - rangeMap.Source)));
                            // add the outside part in to pieces
                            pieces.Push(new Range(rangeMap.Source + rangeMap.Length, source.Start + source.Length - (rangeMap.Source + rangeMap.Length)));
                            notFound = false;
                            break;
                        }
                        
                        //if it starts outside and ends outside (overlaps completely)
                        if ((rangeMap.Source > source.Start) && (rangeMap.Source + rangeMap.Length < source.Start + source.Length))
                        {
                            // add the inside part in to destination
                            destination.Add(new Range(rangeMap.Destination, rangeMap.Length));
                            // add the outside parts in to pieces
                            pieces.Push(new Range(source.Start, rangeMap.Source - source.Start));
                            pieces.Push(new Range(rangeMap.Source + rangeMap.Length, source.Start + source.Length - (rangeMap.Source + rangeMap.Length)));
                            notFound = false;
                            break;
                        }
                    }

                    if (notFound)
                    {
                        destination.Add(source);
                    }
                }
            }

            return destination;
        }
    }

    private record RangeMap(long Source, long Destination, long Length);

    private record Range(long Start, long Length);
    
}