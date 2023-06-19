using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class Card : Component, ICard
{
    public string Status { get; set; } = string.Empty;
}
