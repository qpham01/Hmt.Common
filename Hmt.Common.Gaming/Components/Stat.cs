using Hmt.Common.Core.Things;

namespace Hmt.Common.Gaming.Components;

public class Resource : Thing
{
    public int Count { get; set; }

    public override string ToString()
    {
        return $"{Count} {Name}";
    }
}
