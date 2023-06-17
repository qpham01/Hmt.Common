using Hmt.Common.Core.Things;

namespace Hmt.Common.Core.Views.GameViews;

public abstract class GameMenuBase : GameViewBase
{
    protected Game _game;

    public GameMenuBase(Game game)
    {
        _game = game;
    }
}
