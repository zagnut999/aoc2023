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
