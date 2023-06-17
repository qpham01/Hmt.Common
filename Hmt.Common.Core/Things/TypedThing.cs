using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class TypedThing : Thing, IHasType
{
    public string Type { get; private set; } = string.Empty;

    public TypedThing(string type)
    {
        Type = type;
    }
}
