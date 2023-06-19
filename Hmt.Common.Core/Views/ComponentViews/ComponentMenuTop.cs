using Hmt.Common.Core.Interfaces;
using Hmt.Common.Core.Things;
using Hmt.Common.Core.Views.GameViews;

namespace Hmt.Common.Core.Views.ComponentViews;

public abstract class ComponentMenuTop<T> : GameMenuBase where T : Component, new()
{
    #region Data

    protected string _componentName;
    protected static string? _lastFilterString = null;

    #endregion

    #region Constructors

    public ComponentMenuTop(Game game, string componentName) : base(game)
    {
        _componentName = componentName;
    }

    #endregion

    #region Abstract

    protected abstract List<T> GetComponents(ComponentFilter? filter);
    protected abstract void AddNewComponent(T newComponent);

    #endregion

    public override void Show()
    {
        while (true)
        {
            var choices = new List<string>
            {
                "Go Back",
                "Save Game",
                $"Show {_componentName}s",
                $"Create New {_componentName}",
                $"Edit {_componentName}",
                $"Delete {_componentName}"
            };
            var choice = Choose($"{_componentName} Action", "Select action", choices, false);
            if (choice == 0)
                break;
            else if (choice == 1)
            {
                SaveGame(_game);
                continue;
            }
            else if (choice == 2)
            {
                var components = GetComponents(GetComponentFilter());
                ShowComponents(components);
            }
            else if (choice == 3)
            {
                var newComponent = GetNewComponentInput();
                if (newComponent != null)
                    AddNewComponent(newComponent);
            }
            else if (choice == 4) { }
            else if (choice == 5)
            {
                var name = GetInput("Enter name of component to delete");
                if (string.IsNullOrWhiteSpace(name))
                    continue;
                var components = GetComponents(null);
                var toDelete = components.FirstOrDefault(c => c.Name == name);
                if (toDelete != null)
                {
                    components.Remove(toDelete);
                    WriteLineSuccess($"{_componentName} named {toDelete.Name} has been deleted.");
                }
                else
                    WriteLineFailure($"Cannot find {_componentName.ToLower()} named {name} to delete.");
            }
        }
    }

    protected virtual T? GetNewComponentInput()
    {
        WriteLineInColor(
            $"Please type in on one line the following |-separated formatted string (n: and t: are required):",
            _promptColor
        );
        WriteLineInColor(
            $"n:<name>|t:<type>|d:<description>|1 or more of r:<ResourceName=Count>|1 or more of s:<StatName=Value>",
            _infoColor
        );
        WriteLineLabeledText(
            "Example: ",
            $"n:Goblin|t:Monster|d:A green-skinned humanoid.|r:gold=3|r:gems=0|s:health=10|s:attack=5"
        );
        var input = GetInput("Please enter component line");
        if (input == null)
            return null;
        var newComponent = ParseNewLine(input);
        return newComponent;
    }

    private T? ParseNewLine(string input)
    {
        var parts = input.Split('|');

        var namePart = parts.FirstOrDefault(x => x.StartsWith("n:"));
        var typePart = parts.FirstOrDefault(x => x.StartsWith("t:"));
        if (namePart == null)
        {
            WriteLineFailure($"No name (n:) in input {input}");
            return null;
        }

        if (typePart == null)
        {
            WriteLineFailure($"No type (t:) in input {input}");
            return null;
        }
        var newComponent = new T();
        var name = namePart.Split(":");
        newComponent.Name = name[1];
        var type = typePart.Split(":");
        newComponent.Type = type[1];
        var descriptionPart = parts.FirstOrDefault(x => x.StartsWith("d:"));
        if (descriptionPart != null)
        {
            var description = descriptionPart.Split(":");
            newComponent.Description = description[1];
        }
        var resources = parts.Where(x => x.StartsWith("r:"));
        foreach (var resourcePart in resources)
        {
            var resourceString = resourcePart.Split(":");
            var resourceParts = resourceString[1].Split("=");
            var resource = new Resource { Name = resourceParts[0], Count = int.Parse(resourceParts[1]) };
            newComponent.Resources.Add(resource);
        }
        var stats = parts.Where(x => x.StartsWith("s:"));
        foreach (var statPart in stats)
        {
            var statString = statPart.Split(":");
            var statParts = statString[1].Split("=");
            var stat = new Stat { Name = statParts[0], Value = int.Parse(statParts[1]) };
            newComponent.Stats.Add(stat);
        }
        return newComponent;
    }

    protected virtual void ShowComponents(List<T> components)
    {
        if (components.Count == 0)
        {
            WriteLineInColor($"No {_componentName.ToLower()} to show.", _failureColor);
            return;
        }
        var choices = components.Select(x => x.Name).ToList();
        choices.Insert(0, "Go Back");
        var choice = Choose("Components to Show", "Select component to show more details", choices, false);
        if (choice == 0)
            return;
        choice--;
        var component = components[choice];
        ShowComponent(component);
        HitEnterPrompt();
    }

    protected virtual void ShowComponent(T component)
    {
        var resources = component.Resources.Select(x => x.ToString()).ToList();
        var resourcesString = string.Join(", ", resources);
        var stats = component.Stats.Select(x => x.ToString()).ToList();
        var statsString = string.Join(", ", stats);
        WriteLabeledText("Name: ", component.Name);
        Write(", ");
        WriteLineLabeledText("Type: ", component.Type);
        if (!string.IsNullOrWhiteSpace(component.Description))
            WriteLineLabeledText("Description: ", component.Description);
        if (stats.Count > 0)
            WriteLineLabeledText("Stats: ", statsString);
        if (resources.Count > 0)
            WriteLineLabeledText("Resources: ", resourcesString);
    }

    protected virtual ComponentFilter? GetComponentFilter()
    {
        var filterString = GetInput(
            "Enter filter string (nc:<name contains filter>,tc:<type contains filter>)",
            _lastFilterString
        );
        if (filterString != null)
        {
            var componentFilter = new ComponentFilter();
            componentFilter.ParseFromInputString(filterString);
        }
        return null;
    }

    protected virtual List<T> ApplyFilter(List<T> components, ComponentFilter? filter)
    {
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.NameContains))
                components = components.Where(x => x.Name.Contains(filter.NameContains)).ToList();
            if (!string.IsNullOrWhiteSpace(filter.TypeContains))
                components = components.Where(x => x.Type.Contains(filter.TypeContains)).ToList();
        }
        return components;
    }

    public override void Show(IGameRunner gameRunner)
    {
        throw new NotImplementedException();
    }
}
