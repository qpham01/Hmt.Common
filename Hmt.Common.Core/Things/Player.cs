using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class Player : Thing, IHasStatus
{
    public string Status { get; set; } = string.Empty;
}
