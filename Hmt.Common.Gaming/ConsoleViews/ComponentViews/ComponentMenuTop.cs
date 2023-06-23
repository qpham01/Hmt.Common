using Hmt.Common.Core.Interfaces;
using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.Interfaces;
using Hmt.Common.Gaming.ConsoleViews.GameViews;

namespace Hmt.Common.Gaming.ConsoleViews.ComponentViews;

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

    #region IView, IGameView

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
                var newComponent = GetNewComponent();
                if (newComponent == null)
                    continue;
                AddNewComponent(newComponent);
                WriteLineSuccess($"{_componentName} named {newComponent?.Name} has been added.");
                ShowComponent(newComponent!);
            }
            else if (choice == 4)
            {
                var name = GetInput($"Enter name of {_componentName.ToLower()} to edit");
                if (string.IsNullOrWhiteSpace(name))
                    continue;
                var toEdit = GetComponents(null).FirstOrDefault(c => c.Name.StartsWith(name));
                if (toEdit != null)
                {
                    ShowComponent(toEdit);
                    EditComponent(toEdit);
                    WriteLineSuccess($"{_componentName} named {toEdit.Name} has been changed.");
                    ShowComponent(toEdit);
                }
                else
                    WriteLineFailure($"Cannot find {_componentName.ToLower()} named {name} to delete.");
            }
            else if (choice == 5)
            {
                var name = GetInput($"Enter name of {_componentName.ToLower()} to delete");
                if (string.IsNullOrWhiteSpace(name))
                    continue;
                var components = GetComponents(null);
                var toDelete = components.FirstOrDefault(c => c.Name.StartsWith(name));
                if (toDelete != null)
                {
                    ShowComponent(toDelete);
                    if (GetYesNo($"Confirm deletion of {_componentName.ToLower()} named {toDelete.Name}", false))
                    {
                        components.Remove(toDelete);
                        WriteLineSuccess($"{_componentName} named {toDelete.Name} has been deleted.");
                    }
                }
                else
                    WriteLineFailure($"Cannot find {_componentName.ToLower()} named {name} to delete.");
            }
        }
    }

    public override void Show(IGameRunner gameRunner)
    {
        throw new NotImplementedException();
    }

    #endregion

    protected virtual string? GetComponentInput(bool isNew)
    {
        var prompt = "Please type in on one line the following |-separated formatted string";
        prompt += isNew ? "(n: and t: are required):" : ":";
        WriteLineInColor(prompt, _promptColor);
        WriteLineInColor(
            $"n:<name>|t:<type>|d:<description>|1 or more of r:<ResourceName=Count>|1 or more of s:<StatName=Value>",
            _infoColor
        );
        WriteLineLabeledText(
            "Example: ",
            $"n:Goblin|t:Monster|d:A green-skinned humanoid.|r:gold=3|r:gems=0|s:health=10|s:attack=5"
        );
        var input = GetInput("Please enter component line");
        return input;
    }

    protected virtual T? GetNewComponent()
    {
        var input = GetComponentInput(true);
        if (input == null)
            return null;

        var component = ParseComponentLine(input, null);
        return component;
    }

    protected virtual void EditComponent(T toEdit)
    {
        var input = GetComponentInput(false);
        if (input == null)
            return;
        ParseComponentLine(input, toEdit);
    }

    private T? ParseComponentLine(string input, T? component)
    {
        if (component == null)
            component = new T();
        var parts = input.Split('|');

        var namePart = parts.FirstOrDefault(x => x.StartsWith("n:"));
        var typePart = parts.FirstOrDefault(x => x.StartsWith("t:"));
        if (namePart != null)
        {
            var name = namePart.Split(":");
            component.Name = name[1];
        }
        if (typePart != null)
        {
            var typeParts = typePart.Split(":");
            var type = GetTypeFromStart(typeParts[1], _game.Templates.Values.ToList());
            component.Type = type == null ? typeParts[1] : type;
        }
        var isTemplate = component is Template;
        if (isTemplate && string.IsNullOrWhiteSpace(component.Name))
            component.Name = $"T{component.Type}";
        if (string.IsNullOrWhiteSpace(component.Name))
        {
            WriteLineFailure($"No name (n:) for new {_componentName.ToLower()} in input {input}");
            return null;
        }
        if (string.IsNullOrWhiteSpace(component.Type))
        {
            WriteLineFailure($"No type (t:) for new {_componentName.ToLower()} in input {input}");
            return null;
        }
        Template? template = null;
        if (!isTemplate && !_game.Templates.TryGetValue(component.Type, out template))
        {
            WriteLineFailure($"No template found for {_componentName.ToLower()} of type {component.Type}");
            return null;
        }
        var descriptionPart = parts.FirstOrDefault(x => x.StartsWith("d:"));
        if (descriptionPart != null)
        {
            var description = descriptionPart.Split(":");
            component.Description = description[1];
        }
        var resources = parts.Where(x => x.StartsWith("r:"));
        foreach (var resourcePart in resources)
        {
            var resourceString = resourcePart.Split(":");
            var resourceParts = resourceString[1].Split("=");
            var resourceName =
                template == null ? resourceParts[0] : GetNameFromStart(resourceParts[0], template.Resources, template);
            if (resourceName == null)
            {
                WriteLineFailure($"Cannot find resource name for {resourceParts[0]}.");
                continue;
            }
            var resource = new Resource { Name = resourceName, Count = int.Parse(resourceParts[1]) };
            component.SetResource(resource);
        }
        var stats = parts.Where(x => x.StartsWith("s:"));
        foreach (var statPart in stats)
        {
            var statString = statPart.Split(":");
            var statParts = statString[1].Split("=");
            var statName = template == null ? statParts[0] : GetNameFromStart(statParts[0], template.Stats, template);
            if (statName == null)
            {
                WriteLineFailure($"Cannot find resource name for {statParts[0]}.");
                continue;
            }
            var stat = new Stat { Name = statName, Value = int.Parse(statParts[1]) };
            component.SetStat(stat);
        }
        return component;
    }

    private string? GetNameFromStart<U>(string namePart, List<U> things, Template template) where U : IHasName
    {
        var name = things.FirstOrDefault(x => x.Name.StartsWith(namePart))?.Name;
        return name;
    }

    private string? GetTypeFromStart<U>(string typePart, List<U> things) where U : IHasType
    {
        var type = things.FirstOrDefault(x => x.Type.StartsWith(typePart))?.Type;
        return type;
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
        var choice = Choose($"{_componentName}s to Show", "Select component to show more details", choices, false);
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
            return componentFilter;
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
}
