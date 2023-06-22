using Hmt.Common.Core.Things;
using Hmt.Common.Core.Views.ComponentViews;

namespace Hmt.Common.Core.Views.PieceView;

public class PieceMenuTop : ComponentMenuTop<Piece>
{
    public PieceMenuTop(Game game) : base(game, nameof(Piece)) { }

    protected override void AddNewComponent(Piece newPiece)
    {
        _game.Pieces.Add(newPiece);
    }

    protected override List<Piece> GetComponents(ComponentFilter? filter)
    {
        var boards = _game.Pieces;
        return ApplyFilter(boards, filter);
    }
}
