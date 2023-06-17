using Hmt.Common.Core.Interfaces;
using Hmt.Common.Core.Things;
using Hmt.Common.Core.Views.BoardViews;
using Hmt.Common.Core.Views.CardViews;
using Hmt.Common.Core.Views.GraphSpaceView;
using Hmt.Common.Core.Views.PieceView;
using Hmt.Common.Core.Views.ScenarioView;

namespace Hmt.Common.Core.Views.GameViews;

public class GameMenuComponents : GameMenuBase
{
    public GameMenuComponents(Game game) : base(game) { }

    public override void Show()
    {
        while (true)
        {
            var choices = new[]
            {
                "Go Back",
                "Save Game",
                "Edit Boards",
                "Edit Cards",
                "Edit Graph Spaces",
                "Edit Pieces",
                "Edit Scenarios",
            };

            var choice = Choose("Game Component Action", "Select action", choices, false);
            IView? nextView = null;
            if (choice == 0)
                break;
            else if (choice == 1)
            {
                SaveGame(_game);
                continue;
            }
            else if (choice == 2)
            {
                nextView = new BoardMenuTop(_game);
            }
            else if (choice == 3)
            {
                nextView = new CardMenuTop(_game);
            }
            else if (choice == 4)
            {
                nextView = new GraphSpaceMenuTop(_game);
            }
            else if (choice == 5)
            {
                nextView = new PieceMenuTop(_game);
            }
            else if (choice == 6)
            {
                nextView = new ScenarioMenuTop(_game);
            }

            if (nextView != null)
                nextView.Show();
        }
    }

    public override void Show(IGameRunner gameRunner)
    {
        throw new NotImplementedException();
    }
}
