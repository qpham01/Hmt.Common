using Hmt.Common.Core.DataStructures;
using Hmt.Common.Core.Interfaces;
using Hmt.Common.Gaming.Interfaces;

namespace Hmt.Common.Gaming.Components;

public class GraphSpace : Component, IGraphNode, ISpace
{
    public Dictionary<string, int> MaxCountPerType { get; set; } = new();
    public Dictionary<string, Piece> ContainedPieces { get; set; } = new();

    #region IGraphNode

    public bool Blocked { get; set; }

    public List<Edge> Edges { get; set; } = new();

    #endregion
}
