using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.ConsoleViews.ComponentViews;

namespace Hmt.Common.Gaming.ConsoleViews.GraphSpaceViews;

internal class GraphSpaceMenuTop : ComponentMenuTop<GraphSpace>
{
    protected Board _board;

    public GraphSpaceMenuTop(Game game, Board board) : base(game, nameof(GraphSpace))
    {
        _board = board;
    }

    protected override void AddNewComponent(GraphSpace space)
    {
        if (!_board.Spaces.TryGetValue(space.Type, out var graphSpaces))
        {
            graphSpaces = new List<GraphSpace>();
            _board.Spaces.Add(space.Type, graphSpaces);
        }
        graphSpaces.Add(space);
    }

    protected override List<GraphSpace> GetComponents(ComponentFilter? filter)
    {
        if (filter == null)
            return _board.Spaces.Values.SelectMany(x => x).ToList();

        var types = string.IsNullOrWhiteSpace(filter.TypeContains)
            ? new List<string>()
            : _board.Spaces.Keys.Where(x => x.Contains(filter.TypeContains));
        var graphSpaces = new List<GraphSpace>();
        foreach (var type in types)
        {
            var spaces = _board.Spaces[type];
            if (!string.IsNullOrWhiteSpace(filter.NameContains))
            {
                spaces = spaces.Where(x => x.Name.Contains(filter.NameContains)).ToList();
            }
            graphSpaces.AddRange(spaces);
        }
        return graphSpaces;
    }
}
