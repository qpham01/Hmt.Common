using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.Interfaces;
using Hmt.Common.Gaming.ConsoleViews.GameViews;

namespace Hmt.Common.Gaming.ConsoleViews.GameSessionViews;

public class GameSessionMenuTop : GameMenuBase
{
    private IGameRunner _gameRunner;
    private GameSession _gameSession;

    public GameSessionMenuTop(Game game, GameSession gameSession, IGameRunner gameRunner) : base(game)
    {
        _gameSession = gameSession;
        _gameRunner = gameRunner;
    }

    public override void Show()
    {
        _gameRunner.RunGameSession(_game, _gameSession);
    }

    public override void Show(IGameRunner gameRunner)
    {
        throw new NotImplementedException();
    }
}
