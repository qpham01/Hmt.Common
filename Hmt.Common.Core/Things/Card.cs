using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class Card : Component, ICard
{
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public Card(string type) : base(type) { }
}
