using Hmt.Common.Core.Things;
using Hmt.Common.Gaming.Interfaces;

namespace Hmt.Common.Gaming.Components;

public class Component : TypedThing, IHasStats, IHasResources
{
    public List<Stat> Stats { get; set; } = new();
    public List<Resource> Resources { get; set; } = new();

    public void SetResource(Resource resource)
    {
        var existing = Resources.FirstOrDefault(x => x.Name == resource.Name);
        if (existing == null)
            Resources.Add(resource);
        else
            existing.Count = resource.Count;
    }

    public void SetStat(Stat stat)
    {
        var existing = Stats.FirstOrDefault(x => x.Name == stat.Name);
        if (existing == null)
            Stats.Add(stat);
        else
            existing.Value = stat.Value;
    }
}
