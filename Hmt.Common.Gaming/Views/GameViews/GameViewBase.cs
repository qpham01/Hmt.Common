using Hmt.Common.Core.Views;
using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.Interfaces;
using Newtonsoft.Json;

namespace Hmt.Common.Gaming.Views.GameViews;

public abstract class GameViewBase : ConsoleView, IGameView
{
    protected static string? _currentGamePath = null;
    protected static string? _currentGameSessionPath = null;

    public abstract void Show(IGameRunner gameRunner);

    public virtual Game? LoadGame()
    {
        var gamePath = GetInput("Enter full path to game file");
        if (string.IsNullOrWhiteSpace(gamePath))
            return null;
        if (!File.Exists(gamePath))
        {
            WriteLineFailure($"File {gamePath} does not exist");
            return null;
        }
        var text = File.ReadAllText(gamePath);
        var game = JsonConvert.DeserializeObject<Game>(text);
        if (game == null)
        {
            WriteLineFailure($"Failed to load game from {gamePath}.");
            return null;
        }

        WriteLineSuccess($"Game {game.Name} loaded.");
        _currentGamePath = gamePath;
        return game;
    }

    public virtual void SaveGame(Game game)
    {
        var gamePath = GetInput("Enter full path to game file", _currentGamePath);
        if (string.IsNullOrWhiteSpace(gamePath))
            return;
        if (File.Exists(gamePath))
        {
            var confirm = GetYesNo($"{gamePath} already exist. Confirm overwrite", true);
            if (!confirm)
                return;
        }
        var text = JsonConvert.SerializeObject(game, Formatting.Indented);
        File.WriteAllText(gamePath, text);
        _currentGamePath = gamePath;
        WriteLineSuccess($"Game {game.Name} saved to {gamePath}.");
    }

    public virtual GameSession? LoadGameSession()
    {
        var gameSessionPath = GetInput("Enter full path to game session file", _currentGameSessionPath);
        if (string.IsNullOrWhiteSpace(gameSessionPath))
            return null;
        if (!File.Exists(gameSessionPath))
        {
            WriteLineFailure($"File {gameSessionPath} does not exist");
            return null;
        }
        var text = File.ReadAllText(gameSessionPath);
        var gameSession = JsonConvert.DeserializeObject<GameSession>(text);
        if (gameSession == null)
        {
            WriteLineFailure($"Failed to load game session from {gameSessionPath}.");
            return null;
        }
        WriteLineSuccess($"Game session loaded from {gameSessionPath}.");

        _currentGameSessionPath = gameSessionPath;
        return gameSession;
    }

    public virtual void SaveGameSession(GameSession gameSession)
    {
        var gameSessionPath = GetInput("Enter full path to game session file", _currentGameSessionPath);
        if (string.IsNullOrWhiteSpace(gameSessionPath))
            return;
        if (File.Exists(gameSessionPath))
        {
            var confirm = GetYesNo($"{gameSessionPath} already exist. Confirm overwrite", true);
            if (!confirm)
                return;
        }
        var text = JsonConvert.SerializeObject(gameSession, Formatting.Indented);
        File.WriteAllText(gameSessionPath, text);
        _currentGameSessionPath = gameSessionPath;
        WriteLineSuccess($"Game session saved to {gameSessionPath}.");
    }
}
