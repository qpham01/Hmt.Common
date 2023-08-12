using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.ConsoleViews.ComponentViews;
using Hmt.Common.Gaming.ConsoleViews.GraphSpaceViews;

namespace Hmt.Common.Gaming.ConsoleViews.BoardViews;

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

    protected override Board? GetNewComponent()
    {
        var input = GetComponentInput(true);
        if (input == null)
            return null;

        var board = ParseComponentLine(input, null);
        if (board == null)
            return null;
        AddSpacesToBoard(board);
        return board;
    }

    protected override void EditComponent(Board toEdit)
    {
        var input = GetComponentInput(false);
        if (input == null)
            return;
        ParseComponentLine(input, toEdit);
    }

    private void AddSpacesToBoard(Board? board)
    {
        var spaceMenu = new GraphSpaceMenuTop(_game, board);
        spaceMenu.Show();
    }
}
