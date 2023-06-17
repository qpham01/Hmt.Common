using Hmt.Common.Core.DataStructures;
using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class GraphSpace : Component, IGraphNode, ISpace
{
    public List<string> AllowedPieceTypes { get; set; } = new();
    public List<Piece> ContainedPieces { get; set; } = new();
    public List<GraphSpace> SubSpaces { get; set; } = new();

    #region IGraphNode

    public bool Blocked { get; set; }

    public List<Edge> Edges { get; set; } = new();

    public GraphSpace(string type) : base(type) { }

    #endregion
}
