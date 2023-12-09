namespace aoc2023.day07;

[TestFixture]
public class Tests
{
    [Test]
    public void Example()
    {
        var list = new List<string>
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        };

        var world = new World(list);
        world.Winnings().ShouldBe(6440);
    }
    
    [Test]
    public void Example_Hand1()
    {
        var input = "32T3K 765";
        var split = input.Split(' ');
        var hand = new Hand(split[0], int.Parse(split[1]));
        hand.HandType.ShouldBe(HandType.Pair);
        hand.Bid.ShouldBe(765);
    }
    
    [Test]
    public void Example_Hand2()
    {
        var input = "T55J5 684";
        var split = input.Split(' ');
        var hand = new Hand(split[0], int.Parse(split[1]));
        hand.HandType.ShouldBe(HandType.ThreeOfAKind);
        hand.Bid.ShouldBe(684);
    }
    
    [Test]
    public void Example_Hand3()
    {
        var input = "KK677 28";
        var split = input.Split(' ');
        var hand = new Hand(split[0], int.Parse(split[1]));
        hand.HandType.ShouldBe(HandType.TwoPair);
        hand.Bid.ShouldBe(28);
    }
    
    [Test]
    public void Example_Hand4()
    {
        var input = "KTJJT 220";
        var split = input.Split(' ');
        var hand = new Hand(split[0], int.Parse(split[1]));
        hand.HandType.ShouldBe(HandType.TwoPair);
        hand.Bid.ShouldBe(220);
    }
    
    [Test]
    public void Example_Hand5()
    {
        var input = "QQQJA 483";
        var split = input.Split(' ');
        var hand = new Hand(split[0], int.Parse(split[1]));
        hand.HandType.ShouldBe(HandType.ThreeOfAKind);
        hand.Bid.ShouldBe(483);
    }

    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day07");
        var world = new World(list);
        world.Winnings().ShouldBe(251545216);
    }
    
    [Test]
    public void ExamplePart2()
    {
        var list = new List<string>
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        };

        var world = new World(list, true);
        world.Winnings().ShouldBe(5905);
    }
    
    [Test]
    public void ExamplePart2_Hand5()
    {
        var input = "QQQJA 483";
        var split = input.Split(' ');
        var hand = new Hand(split[0], int.Parse(split[1]), true);
        hand.HandType.ShouldBe(HandType.FourOfAKind);
        hand.Bid.ShouldBe(483);
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day07");
        var world = new World(list, true);
        world.Winnings().ShouldBe(250384185);
    }

    private class World
    {
        private bool JokersWild { get; }
        private List<Hand> Hands { get; } = new();
        
        public World(List<string> list, bool jokersWild = false)
        {
            JokersWild = jokersWild;
            foreach (var line in list)
            {
                var split = line.Split(' ');
                var cards = split[0];
                var bid = int.Parse(split[1]);
                Hands.Add(new Hand(cards, bid, jokersWild));
            }
        }

        public int Winnings()
        {
            Hands.Sort((a, b) =>
            {
                if (a.HandType == b.HandType)
                {
                    for (var i = 0; i < a.Cards.Length; i++)
                    {
                        if (a.Cards[i].Value(JokersWild) != b.Cards[i].Value(JokersWild))
                            return a.Cards[i].Value(JokersWild).CompareTo(b.Cards[i].Value(JokersWild));
                    }
                }
                return a.HandType.CompareTo(b.HandType);
            });
            var winnings = 0;
            for (var i = 0; i < Hands.Count; i++)
            {
                winnings += Hands[i].Bid * (i + 1);
            }

            return winnings;
        }
    }
    
    private class Hand
    {
        public int Bid { get; }
        private bool JokersWild { get; }
        public string Cards { get; }
        
        public HandType HandType { get;  }
        
        public Hand(string cards, int bid, bool jokersWild = false)
        {
            Bid = bid;
            JokersWild = jokersWild;
            Cards = cards;

            HandType = Rank();
        }

        public override string ToString()
        {
            return $"{Cards} : {Bid}";
        }

        private HandType Rank()
        {
            if (JokersWild && Cards.Contains('J'))
            {
                var bestHandType = HandType.HighCard;
                var possibleCards = new List<char> {'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2'};
                foreach(var cardToTry in possibleCards)
                {
                    var cards = Cards.Replace('J', cardToTry);
                    var hand = new Hand(cards, Bid);
                    if (hand.HandType > bestHandType)
                        bestHandType = hand.HandType;
                }

                return bestHandType;
            }
            
            var handType = HandType.HighCard;

            if (IsFiveOfAKind())
                handType = HandType.FiveOfAKind;
            else if (IsFourOfAKind())
                handType = HandType.FourOfAKind;
            else if (IsFullHouse())
                handType = HandType.FullHouse;
            else if (IsThreeOfAKind())
                handType = HandType.ThreeOfAKind;
            else if (IsTwoPair())
                handType = HandType.TwoPair;
            else if (IsPair())
                handType = HandType.Pair;
            
            return handType;
        }

        private bool IsPair()
        {
            return Cards.GroupBy(c => c.Value(JokersWild)).Any(g => g.Count() == 2);
        }

        private bool IsTwoPair()
        {
            return Cards.GroupBy(c => c.Value(JokersWild)).Count(g => g.Count() == 2) == 2;
        }

        private bool IsThreeOfAKind()
        {
            return Cards.GroupBy(c => c.Value(JokersWild)).Any(g => g.Count() == 3);
        }

        private bool IsFullHouse()
        {
            return IsThreeOfAKind() && IsPair();
        }

        private bool IsFourOfAKind()
        {
            return Cards.GroupBy(c => c.Value(JokersWild)).Any(g => g.Count() == 4);
        }

        private bool IsFiveOfAKind()
        {
            return Cards.GroupBy(c => c.Value(JokersWild)).Any(g => g.Count() == 5);
        }
    }
}

public static class Card
{
    public static int Value(this char c, bool jokersWild=false)
    {
        return c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => jokersWild? 1: 11,
            'T' => 10,
            _ => int.Parse(c.ToString())
        };
    }
}

public enum HandType
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}