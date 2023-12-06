namespace aoc2023.day04;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        };

        var world = new World(list);
        world.Cards[0].Score().ShouldBe(8);
        world.Cards[1].Score().ShouldBe(2);
        world.Cards[2].Score().ShouldBe(2);
        world.Cards[3].Score().ShouldBe(1);
        world.Cards[4].Score().ShouldBe(0);
        world.Cards[5].Score().ShouldBe(0);
        world.Score().ShouldBe(13);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day04");
        var world = new World(list);
        world.Score().ShouldBe(24542);
    }

    [Test]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        };

        var world = new World(list);
        world.ScorePart2().ShouldBe(30);
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day04");
        var world = new World(list);
        world.ScorePart2().ShouldBe(8736438);
    }
    
    private class World
    {
        public readonly List<Card> Cards = new();
        
        public World(List<string> list)
        {
            
            foreach (var line in list)
            {
                var card = new Card(line);
                Cards.Add(card);
            }
        }
        
        public int Score()
        {
            return Cards.Sum(x => x.Score());
        }

        public int ScorePart2()
        {
            var cards = new Dictionary<Card, int>();
            Cards.ForEach(x=>cards.Add(x, 1));

            for (var i = 0; i < Cards.Count; i++)
            {
                var card = Cards[i];
                var countCards = cards[card];
                var ourWinnersCount = card.Count;
                
                for (var j =0; j < ourWinnersCount; j++)
                {
                    var bonusCard = Cards[i + j + 1];
                    cards[bonusCard]+=countCards;
                }
            }
            
            return cards.Values.Sum();
        }
    }
    
    private class Card
    {
        private string Name { get; }
        private List<int> Winners { get; }
        private List<int> Ours { get; }
        
        public Card(string line)
        {
            var split = line.Split(":");
            Name = split[0];
            var numbers = split[1].Split("|");
            Winners = numbers[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            Ours = numbers[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        }
        
        public int Score()
        {
            var ourWinnersCount = Count;
            
            switch (ourWinnersCount)
            {
                case 0:
                    return 0;
                default:
                    return (int) Math.Pow(2.0, ourWinnersCount - 1);
            }
        }

        private int? _count;

        public int Count
        {
            get
            {
                if (_count.HasValue)
                {
                    return _count.Value;
                }

                _count = Winners.Intersect(Ours).Count();
                return _count.Value;
            }
        }
    }
}