using Hmt.Common.Gaming.Components;

namespace Hmt.Common.Gaming.Views.GameViews;

public abstract class GameMenuBase : GameViewBase
{
    protected Game _game;

    public GameMenuBase(Game game)
    {
        _game = game;
    }
}
