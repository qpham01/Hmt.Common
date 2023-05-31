using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Helpers;

public static class Randomizer
{
    private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

    public static int GetRandomInteger(int min, int max)
    {
        return _random.Next(min, max);
    }

    public static double GetRandomDouble(double min, double max)
    {
        return _random.NextDouble() * (max - min) + min;
    }

    public static IReadOnlyList<T> Choose<T>(IReadOnlyCollection<T> values, int count)
    {
        if (count > values.Count)
            throw new ArgumentException($"Count {count} cannot be greater than the number of values.");
        var result = new List<T>(count);
        for (var i = 0; i < count; i++)
        {
            var input = values.ToList();
            var output = new List<T>(count);
            for (var j = 0; j < values.Count; j++)
            {
                var index = _random.Next(0, input.Count);
                output.Add(input[index]);
                input.RemoveAt(index);
            }
            result = output;
        }
        return result;
    }
}
