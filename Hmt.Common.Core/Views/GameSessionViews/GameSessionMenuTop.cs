using Hmt.Common.Core.Interfaces;
using Hmt.Common.Core.Things;
using Hmt.Common.Core.Views.GameViews;

namespace Hmt.Common.Core.Views.GameSessionViews;

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
