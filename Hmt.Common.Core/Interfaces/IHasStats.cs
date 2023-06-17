using Hmt.Common.Core.Things;

namespace Hmt.Common.Core.Interfaces;

public interface IHasStats
{
    List<Stat> Stats { get; set; }
    int GetStatValue(string statName)
    {
        var stat = Stats.Find(x => x.Name == statName);
        if (stat == null)
            return int.MinValue;
        return stat.Value;
    }

    int ChangeStat(string statName, int change)
    {
        var stat = Stats.Find(x => x.Name == statName);
        if (stat == null)
            return int.MinValue;
        stat.Value += change;
        return stat.Value;
    }
}
