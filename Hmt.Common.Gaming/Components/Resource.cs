using Hmt.Common.Core.Things;

namespace Hmt.Common.Gaming.Components;

public class Stat : Thing
{
    public int Value { get; set; }

    public override string ToString()
    {
        return $"{Name}: {Value}";
    }
}
