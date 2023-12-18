namespace aoc2023;

internal static class Utilities
{
    public static async Task<List<string>> ReadInput(string path)
    {
        return (await File.ReadAllLinesAsync(path)).ToList();
    }
    
    public static async Task<List<string>> ReadInputByDay(string day)
    {
        var path = $"../../../{day}/input.txt";

        if (File.Exists(path))
        {
            var list = await File.ReadAllLinesAsync(path);
            return list.ToList();
        }

        return new List<string>();
    }

    public static async Task<string> ReadInputByDayRaw(string day)
    {
        var path = $"../../../{day}/input.txt";

        if (File.Exists(path))
        {
            return await File.ReadAllTextAsync(path);
        }

        return string.Empty;
    }

    public static IReadOnlyList<T> Add<T>(this IReadOnlyList<T> list, T node)
    {
        var newList = new List<T>();
        newList.AddRange(list);
        newList.Add(node);
        return newList.AsReadOnly();
    }
}

public class Point(int x, int y)
{
    public static Point New(int x, int y) => new(x, y);
    public static Point New(Point point) => new(point.X, point.Y);
            
    public int X { get; } = x;
    public int Y { get; } = y;

    public override bool Equals(object? obj)
    {
        if (obj is Point vector)
        {
            return vector.X == X && vector.Y == Y;
        }
        return false;
    }
            
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
