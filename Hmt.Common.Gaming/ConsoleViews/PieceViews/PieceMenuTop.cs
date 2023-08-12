using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.ConsoleViews.ComponentViews;

namespace Hmt.Common.Gaming.ConsoleViews.PieceViews;

public class PieceMenuTop : ComponentMenuTop<Piece>
{
    public PieceMenuTop(Game game) : base(game, nameof(Piece)) { }

    protected override void AddNewComponent(Piece newPiece)
    {
        _game.Pieces.Add(newPiece);
    }

    protected override List<Piece> GetComponents(ComponentFilter? filter)
    {
        var pieces = _game.Pieces;
        return ApplyFilter(pieces, filter);
    }
}
