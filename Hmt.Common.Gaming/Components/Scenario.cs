namespace Hmt.Common.Gaming.Components;

public class Scenario : Component
{
    public int MinPlayerCount { get; set; }
    public int MaxPlayerCount { get; set; }
    public List<Player> Players { get; set; } = new();
    public List<Board> Boards { get; set; } = new();
    public List<Deck<Card>> Decks { get; set; } = new();
    public List<Piece> Pieces { get; set; } = new();
}
