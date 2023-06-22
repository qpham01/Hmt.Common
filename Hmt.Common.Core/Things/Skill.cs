namespace Hmt.Common.Core.Things;

public class Skill : Trait
{
    public Skill()
    {
        var level = new Stat { Name = "Level", Value = 0 };
        Stats.Add(level);
    }
}
