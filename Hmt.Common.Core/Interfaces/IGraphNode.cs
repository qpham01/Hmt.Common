using Hmt.Common.Core.DataStructures;

namespace Hmt.Common.Core.Interfaces;

public interface IGraphNode : IHasName
{
    bool Blocked { get; set; }
    List<Edge> Edges { get; set; }

    public virtual void AddEdge(IGraphNode to, double distance, bool bidirectional)
    {
        Edges.Add(new Edge(this, to, distance));
        if (bidirectional)
            to.AddEdge(this, distance, false);
    }
}
