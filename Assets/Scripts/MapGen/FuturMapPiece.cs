using System.Collections.Generic;
using System.Linq;

public class FuturMapPiece
{
    public MapPiece CurrentPiece;

    private List<MapPiece> _neighbors;
    private bool ShouldIncludeRoom => _neighbors.All(piece => piece.PieceType == MapPieceType.Corridor);
    private Side SideToLink => _neighbors.Select(n => n.GetOpenedSideFor(Grid)).Aggregate(Side.none, (side, s) => side | s);
    private Side SideNotToLink => _neighbors.Select(n => n.GetClosedSideFor(Grid)).Aggregate(Side.none, (side, s) => side | s);

    public Grid Grid;

    public MapPieceRotation Rotation;

    private bool _isLastDeepnessLevel;

    public static FuturMapPiece CreateFutureMapPiece(List<MapPiece> neighbors, Grid position, bool isLastDeepnessLevel)
    {
        var futurePiece = new FuturMapPiece();
        futurePiece.Grid = position;
        futurePiece._neighbors = neighbors;

        futurePiece._isLastDeepnessLevel = isLastDeepnessLevel;

        return futurePiece;
    }

    public Side GetRotatedOpenSide()
    {
        return Rotation.RotateSides(CurrentPiece.OpenSides);
    }

    public bool CanConnectSide()
    {
        var rotationOpenedSide = GetRotatedOpenSide();

        if (_isLastDeepnessLevel)
        {
            return rotationOpenedSide.HasExactSides(SideToLink);
        }

        return rotationOpenedSide.HasSidesOpen(SideToLink)
               && rotationOpenedSide.HasSidesClosed(SideNotToLink);
    }

    public bool TryAllPieces(MapPiecesGroup mapPiecesGroup, bool shouldTryEndRoom = false)
    {
        var randomPieceGenerator = new RandomPieceGenerator(mapPiecesGroup, ShouldIncludeRoom);

        if (shouldTryEndRoom && TryPlace(mapPiecesGroup.EndRoom))
        {
            return true;
        }

        while (!randomPieceGenerator.IsEmpty())
        {
            if (!TryPlace(randomPieceGenerator.Next()))
            {
                continue;
            }

            return true;
        }

        return false;
    }

    public bool TryPlace(MapPiece mapPiece)
    {
        CurrentPiece = mapPiece;
        return TryAllRotations();
    }

    public bool TryAllRotations()
    {
        for (Rotation = MapPieceRotation.None;
            Rotation != MapPieceRotation.FullTurn;
            Rotation = Rotation.Next()
        )
        {
            if (!CanConnectSide())
            {
                continue;
            }

            return true;
        }

        return false;
    }
}


