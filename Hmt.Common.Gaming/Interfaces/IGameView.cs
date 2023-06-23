using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Gaming.Interfaces;

public interface IGameView : IView
{
    void Show(IGameRunner gameRunner);
}
