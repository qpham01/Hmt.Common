using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.Interfaces;
using Hmt.Common.Gaming.ConsoleViews.GameSessionViews;

namespace Hmt.Common.Gaming.ConsoleViews.GameViews;

public class GameMenuTop : GameMenuBase
{
    protected GameMenuComponents _gameMenuComponents;
    protected GameSessionMenuTop _gameSessionMenuTop;
    protected IGameRunner _gameRunner;

    public GameMenuTop(IGameRunner gameRunner) : base(new Game())
    {
        _gameMenuComponents = new GameMenuComponents(_game);
        _gameSessionMenuTop = new GameSessionMenuTop(_game, new GameSession(), gameRunner);
        _gameRunner = gameRunner;
    }

    public override void Show()
    {
        Game? game = null;
        while (true)
        {
            var choices = new List<string> { "Quit", "Create Game", "Load Game" };
            if (game != null)
            {
                choices.AddRange(new[] { "Edit Game", "New Game Session", "Load Game Session" });
            }
            var choice = Choose("Game Action", "Select action", choices, false);
            if (choice == 0)
                break;
            else if (choice == 1)
            {
                var gameName = GetInput("Enter name of game");
                if (string.IsNullOrWhiteSpace(gameName))
                    continue;
                game = new Game { Name = gameName };
                WriteLine($"New game {game.Name} created.");
            }
            else if (choice == 2)
            {
                game = LoadGame();
                if (game == null)
                    continue;
            }
            else if (choice == 3)
            {
                if (game == null)
                    continue;
                var nextView = _gameSessionMenuTop;
                nextView.Show();
            }
            else if (choice == 4)
            {
                if (game == null)
                    continue;
                var nextView = _gameSessionMenuTop;
                nextView.Show();
            }
            else if (choice == 5)
            {
                if (game == null)
                    continue;
                var session = LoadGameSession();
                if (session != null)
                {
                    var nextView = new GameSessionMenuTop(game, session, _gameRunner);
                    nextView.Show();
                }
            }
            else if (choice == 4)
                break;
        }
    }
}
