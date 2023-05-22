namespace Hmt.Common.DataStructures;

public interface IGraphNode
{
    string Name { get; }
    bool Blocked { get; }
    Edge[] Edges { get; }
}
