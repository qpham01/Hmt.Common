using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class Thing : IHasName
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
