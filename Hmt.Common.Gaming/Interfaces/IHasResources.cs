using Hmt.Common.Gaming.Components;

namespace Hmt.Common.Gaming.Interfaces;

public interface IHasResources
{
    List<Resource> Resources { get; set; }
    int GetResourceValue(string resourceName)
    {
        var resource = Resources.Find(x => x.Name == resourceName);
        if (resource == null)
            return int.MinValue;
        return resource.Count;
    }

    int ChangeResource(string resourceName, int change)
    {
        var resource = Resources.Find(x => x.Name == resourceName);
        if (resource == null)
            return int.MinValue;
        resource.Count += change;
        return resource.Count;
    }
}
