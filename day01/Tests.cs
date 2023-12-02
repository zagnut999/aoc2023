using System.Text;
using System.Text.RegularExpressions;

namespace aoc2023.day01;

[TestFixture]
public class Tests
{
    [Test]
    public async Task ReadDaysFile()
    {
        var list = await Utilities.ReadInputByDay("Day01");
        list.ShouldNotBeEmpty();
    }
    
    Dictionary<string, string> keysPart1 = new()
    {
        {"1", "1"},
        {"2", "2"},
        {"3", "3"},
        {"4", "4"},
        {"5", "5"},
        {"6", "6"},
        {"7", "7"},
        {"8", "8"},
        {"9", "9"}
    };
    
    Dictionary<string, string> keysPart2 = new () {
        {"1", "1"},
        {"2", "2"},
        {"3", "3"},
        {"4", "4"},
        {"5", "5"},
        {"6", "6"},
        {"7", "7"},
        {"8", "8"},
        {"9", "9"},
        {"one", "1"},
        {"two", "2"},
        {"three", "3"},
        {"four", "4"},
        {"five", "5"},
        {"six", "6"},
        {"seven", "7"},
        {"eight", "8"},
        {"nine", "9"},
    };
    
    [Test]
    public void Example()
    {
        var sample = new List<string>
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        };
        
        var trebuchet = new Trebuchet();
        var result = trebuchet.ParseCalibration(sample, keysPart1);
        
        result.ShouldBe(142);
    }
    
    [Test]
    public async Task Actual()
    {
        var list = await Utilities.ReadInputByDay("Day01");
        var trebuchet = new Trebuchet();
        var result = trebuchet.ParseCalibration(list, keysPart1);
        
        result.ShouldBe(53194);
    }

    [Test]
    public void ExamplePart2()
    {
        var sample = new List<string>
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen",
        };
        
        
        var trebuchet = new Trebuchet();
        var result = trebuchet.ParseCalibration(sample, keysPart2);
        result.ShouldBe(281);
    }
    
    [Test]
    public async Task ActualPart2()
    {
        var list = await Utilities.ReadInputByDay("Day01");
        var trebuchet = new Trebuchet();
        var result = trebuchet.ParseCalibration(list, keysPart2);
        result.ShouldBe(54249);
    }

    private class Trebuchet
    {
        public int ParseCalibration(List<string> input, Dictionary<string, string> keys)
        {
            var regex = new Regex(@"\D");
            var sum = 0;
            foreach (var line in input)
            {
                var newLine = new StringBuilder();
                
                for (var i = 0; i < line.Length; i++)
                {
                    var subString = line[i..];
                    foreach (var key in keys.Keys.Where(key => subString.StartsWith(key)))
                    {
                        newLine.Append(keys[key]);
                    }
                }
                var numbers = newLine.ToString();
                sum += int.Parse(numbers.First().ToString() + numbers.Last());
            }

            return sum;
        }
    }
}