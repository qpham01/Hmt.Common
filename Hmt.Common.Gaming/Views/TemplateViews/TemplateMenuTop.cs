using Hmt.Common.Gaming.Components;
using Hmt.Common.Gaming.Views.ComponentViews;

namespace Hmt.Common.Gaming.Views.TemplateViews;

public class TemplateMenuTop : ComponentMenuTop<Template>
{
    public TemplateMenuTop(Game game) : base(game, nameof(Template)) { }

    protected override void AddNewComponent(Template newTemplate)
    {
        _game.Templates.Add(newTemplate.Type, newTemplate);
    }

    protected override List<Template> GetComponents(ComponentFilter? filter)
    {
        var templates = _game.Templates.Values.ToList();
        return ApplyFilter(templates, filter);
    }
}
