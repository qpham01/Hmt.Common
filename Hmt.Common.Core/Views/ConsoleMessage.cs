namespace Hmt.Common.Core.Views;

public class ConsoleMessage
{
    public ConsoleMessage(string message) : this(message, ConsoleColor.Gray) { }

    public ConsoleMessage(string message, ConsoleColor color)
    {
        Message = message;
        Color = color;
    }

    public string Message { get; set; } = string.Empty;
    public ConsoleColor Color { get; set; }
}
