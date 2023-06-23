namespace Hmt.Common.Gaming.ConsoleViews.ComponentViews;

public class ComponentFilter
{
    public const string NameContainsPrefix = "nc";
    public const string TypeContainsPrefix = "tc";
    public string NameContains { get; set; } = string.Empty;
    public string TypeContains { get; set; } = string.Empty;

    public bool ParseFromInputString(string inputString)
    {
        var parts = inputString.Split(new char[] { ':', ',' });
        for (var i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            if (part == NameContainsPrefix)
            {
                if (i == parts.Length - 1)
                    return false;
                NameContains = parts[++i];
                continue;
            }
            if (part == TypeContainsPrefix)
            {
                if (i == parts.Length - 1)
                    return false;
                TypeContains = parts[++i];
                continue;
            }
        }
        return true;
    }
}
