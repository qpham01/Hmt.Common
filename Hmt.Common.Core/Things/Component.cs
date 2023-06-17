using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class Component : TypedThing, IHasStats, IHasResources
{
    public Component(string type) : base(type) { }

    public List<Stat> Stats { get; set; } = new();
    public List<Resource> Resources { get; set; } = new();
}
