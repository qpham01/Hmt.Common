using Hmt.Common.Core.Interfaces;
using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.Interfaces;
using Hmt.Common.Gaming.ConsoleViews.BoardViews;
using Hmt.Common.Gaming.ConsoleViews.CardViews;
using Hmt.Common.Gaming.ConsoleViews.GraphSpaceViews;
using Hmt.Common.Gaming.ConsoleViews.PieceViews;
using Hmt.Common.Gaming.ConsoleViews.ScenarioView;
using Hmt.Common.Gaming.ConsoleViews.TemplateViews;

namespace Hmt.Common.Gaming.ConsoleViews.GameViews;

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
                "Edit Templates"
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
            else if (choice == 7)
            {
                nextView = new TemplateMenuTop(_game);
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
