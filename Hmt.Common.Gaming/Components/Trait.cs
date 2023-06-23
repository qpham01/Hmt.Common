using Hmt.Common.Core.Things;
using Hmt.Common.Gaming.Interfaces;
using System.Text;

namespace Hmt.Common.Gaming.Components;

public class Trait : TypedThing, IHasStats
{
    public List<Stat> Stats { get; set; } = new();

    public override string ToString()
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(Type))
            sb.Append($"{Type}: ");
        if (!string.IsNullOrWhiteSpace(Name))
            sb.Append($"{Name} ");
        foreach (var stat in Stats)
            sb.Append(stat.ToString());
        return sb.ToString();
    }
}
