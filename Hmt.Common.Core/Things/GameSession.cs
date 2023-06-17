namespace Hmt.Common.Core.Things;

public class GameSession
{
    public Scenario Scenario { get; set; } = new();
    public List<Player> Players { get; set; } = new();
}
