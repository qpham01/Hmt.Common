using Hmt.Common.Core.DataStructures;

namespace Hmt.Common.Core.Interfaces;

public interface IGraphNode : IHasName
{
    bool Blocked { get; }
    Edge[] Edges { get; }
}
