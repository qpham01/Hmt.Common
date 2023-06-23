using Hmt.Common.Core.Things;

namespace Hmt.Common.Gaming.Components;

public class Game : Thing
{
    public List<Board> Boards { get; set; } = new();
    public List<GraphSpace> GraphSpaces { get; set; } = new();
    public List<Piece> Pieces { get; set; } = new();
    public List<Deck<Card>> Decks { get; set; } = new();
    public List<Card> Cards { get; set; } = new();
    public Dictionary<string, Template> Templates { get; set; } = new();
}
