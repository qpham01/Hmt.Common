namespace Hmt.Common.Gaming.Components;

public class GameSession
{
    public Scenario Scenario { get; set; } = new();
    public List<Player> Players { get; set; } = new();
}
