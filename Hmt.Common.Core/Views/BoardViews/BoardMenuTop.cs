using Hmt.Common.Core.Things;
using Hmt.Common.Core.Views.ComponentViews;

namespace Hmt.Common.Core.Views.BoardViews;

public class BoardMenuTop : ComponentMenuTop<Board>
{
    public BoardMenuTop(Game game) : base(game, nameof(Board)) { }

    protected override void AddNewComponent(Board newBoard)
    {
        _game.Boards.Add(newBoard);
    }

    protected override List<Board> GetComponents(ComponentFilter? filter)
    {
        var boards = _game.Boards;
        return ApplyFilter(boards, filter);
    }
}
