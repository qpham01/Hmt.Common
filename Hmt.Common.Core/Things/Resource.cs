namespace Hmt.Common.Core.Things;

public class Stat : Thing
{
    public int Value { get; set; }

    public override string ToString()
    {
        return $"{Name}: {Value}";
    }
}
