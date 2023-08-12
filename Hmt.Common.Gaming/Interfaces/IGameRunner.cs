using Hmt.Common.Gaming.Components;

namespace Hmt.Common.Gaming.Interfaces;

public interface IGameRunner
{
    void RunGameSession(Game game, GameSession gameSession);
}
