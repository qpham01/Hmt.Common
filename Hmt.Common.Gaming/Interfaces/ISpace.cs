namespace Hmt.Common.Gaming.Interfaces;

public interface ISpace
{
    public List<string> AllowedPieceTypes { get; set; }

    bool IsPieceAllowed(string pieceType)
    {
        return AllowedPieceTypes.Contains(pieceType);
    }
}
