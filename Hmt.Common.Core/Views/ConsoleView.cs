using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Views;

public abstract class ConsoleView : IView
{
    public const string TableSeparator = "|";

    public ConsoleView() { }

    public abstract void Show();

    public int Choose(string title, string prompt, IReadOnlyList<string> choices)
    {
        Console.WriteLine("");
        Console.WriteLine(title);
        var choiceMap = new Dictionary<string, int>();
        for (var i = 0; i < choices.Count; i++)
        {
            choiceMap.Add(choices[i], i);
        }
        var sortedChoices = choices.OrderBy(x => x).ToArray();
        for (var i = 0; i < choices.Count; i++)
        {
            Console.WriteLine($"{i + 1:00}. {sortedChoices[i]}");
        }
        var choice = Choose(prompt, 1, choices.Count);
        return choiceMap[sortedChoices[choice]];
    }

    public int Choose(string prompt, int min, int max)
    {
        var choice = -1;
        var validChoice = false;
        while (!validChoice)
        {
            try
            {
                var message = $"{prompt} ({min} to {max}): ";
                Console.Write(message);
                choice = int.Parse(Console.ReadLine() ?? string.Empty);
                validChoice = choice >= min && choice <= max;
            }
            catch (Exception)
            {
                validChoice = false;
            }
        }
        return choice - 1;
    }

    protected string? GetInput(string text)
    {
        Write(text);
        return Console.ReadLine();
    }

    protected void WriteLineFailure(string message)
    {
        WriteLineInColor(message, ConsoleColor.Red);
    }

    protected void WriteLineSuccess(string message)
    {
        WriteLineInColor(message, ConsoleColor.Green);
    }

    protected void WriteLine(string text)
    {
        Console.WriteLine(text);
    }

    protected void Write(string text)
    {
        Console.Write(text);
    }

    protected void WriteLineInColor(string text, ConsoleColor color)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = prevColor;
    }

    protected void WriteInColor(string text, ConsoleColor color)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = prevColor;
    }
}
