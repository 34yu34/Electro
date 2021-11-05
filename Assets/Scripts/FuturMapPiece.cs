using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FuturMapPiece
{
    public List<MapPiece> TriedPieces;

    
    private List<MapPiece> _neighbors;
    public bool ShouldIncludeRoom => _neighbors.All(piece => piece.PieceType == MapPieceType.Corridor);
    public Side SideToLink => _neighbors.Select(n => n.GetOpenedSideFor(Grid)).Aggregate(Side.none, (side, s) => side | s);
    public Side SideNotToLink => _neighbors.Select(n => n.GetClosedSideFor(Grid)).Aggregate(Side.none, (side, s) => side | s);

    
    public Grid Grid;

    public MapPiece CurrentTriedPiece;

    public MapPieceRotation Rotation;

    public static FuturMapPiece createFuturMapPiece(List<MapPiece> neighbors, Grid position)
    {
        var future_piece = new FuturMapPiece(); 
        future_piece.Grid = position;
        future_piece._neighbors = neighbors;
        future_piece.TriedPieces = new List<MapPiece>();

        return future_piece;
    }
}
