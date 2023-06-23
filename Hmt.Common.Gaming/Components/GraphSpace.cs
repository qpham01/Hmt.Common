using Hmt.Common.Core.DataStructures;
using Hmt.Common.Core.Interfaces;
using Hmt.Common.Gaming.Interfaces;

namespace Hmt.Common.Gaming.Components;

public class GraphSpace : Component, IGraphNode, ISpace
{
    public List<string> AllowedPieceTypes { get; set; } = new();
    public List<Piece> ContainedPieces { get; set; } = new();
    public List<GraphSpace> SubSpaces { get; set; } = new();

    #region IGraphNode

    public bool Blocked { get; set; }

    public List<Edge> Edges { get; set; } = new();

    #endregion
}
