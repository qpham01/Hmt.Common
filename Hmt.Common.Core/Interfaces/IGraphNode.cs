using Hmt.Common.Core.DataStructures;

namespace Hmt.Common.Core.Interfaces;

public interface IGraphNode
{
    string Name { get; }
    bool Blocked { get; }
    Edge[] Edges { get; }
}
