using Hmt.Common.Gaming.Components;

namespace Hmt.Common.Gaming.Interfaces;

public interface ISpace
{
    Dictionary<string, int> MaxCountPerType { get; set; }
    Dictionary<string, Piece> ContainedPieces { get; set; }

    bool IsPieceAllowed(string pieceType)
    {
        return MaxCountPerType.Keys.Contains(pieceType);
    }
}
