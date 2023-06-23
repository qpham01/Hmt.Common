using Hmt.Common.Core.Interfaces;
using Hmt.Common.Core.Things;

namespace Hmt.Common.Gaming.Components;

public class Player : Thing, IHasStatus
{
    public string Status { get; set; } = string.Empty;
}
