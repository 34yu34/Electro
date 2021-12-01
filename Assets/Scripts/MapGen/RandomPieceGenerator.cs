using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPieceGenerator 
{
    private List<MapPiece> _pieces;
    private List<MapPiece> _discardedPieces;

    private int _currentIndex = 0;

    public RandomPieceGenerator(MapPiecesGroup pieces, bool ShouldIncludeRooms)
    {
        _pieces = pieces.GetAllRequiredPieces(ShouldIncludeRooms).ToList();

        _discardedPieces = pieces.Junctions.Concat(_pieces.Where(piece => piece.OpenSides.Count() == 1)).ToList();
        
        _pieces = _pieces.Where(piece => piece.OpenSides.Count() != 1).ToList();

        _pieces = Randomizer.Shuffle(_pieces);
        
        _currentIndex = 0;
    }

    public MapPiece Next()
    {
        if (_currentIndex < _pieces.Count())
        {
            return _pieces[_currentIndex++];
        }

        if (_discardedPieces != null)
        {
            _pieces = _discardedPieces;
            _discardedPieces = null;
            _currentIndex = 0;
            return Next();
        }

        return null;
    }

    public bool IsEmpty()
    {
        return _discardedPieces == null && _currentIndex == _pieces.Count();
    }
}
