using Hmt.Common.Gaming.Interfaces;

namespace Hmt.Common.Gaming.Components;

public class Card : Component, ICard
{
    public string Status { get; set; } = string.Empty;
}
