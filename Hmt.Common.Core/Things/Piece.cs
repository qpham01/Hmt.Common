namespace Hmt.Common.Core.Things;

public class Piece : Component
{
    public string? OwnerName { get; set; }
    public GraphSpace? AtSpace { get; set; }

    public Piece(string type) : base(type) { }
}
