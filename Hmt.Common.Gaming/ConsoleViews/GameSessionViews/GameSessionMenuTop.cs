using Hmt.Common.Core.Interfaces;
using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.Interfaces;
using Hmt.Common.Gaming.ConsoleViews.GameViews;

namespace Hmt.Common.Gaming.ConsoleViews.GameSessionViews;

public class GameSessionMenuTop : GameMenuBase
{
    public GameSessionMenuTop(Game game, GameSession gameSession, IGameRunner gameRunner) : base(game) { }

    public override void Show()
    {
        throw new NotImplementedException();
    }

    public override void Show(IGameRunner gameRunner)
    {
        throw new NotImplementedException();
    }
}
