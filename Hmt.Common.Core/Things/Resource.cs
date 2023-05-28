namespace Hmt.Common.Core.Things;

public class Resource : Thing
{
    public int Count { get; set; }

    public override string ToString()
    {
        return $"{Count} {Name}";
    }
}
