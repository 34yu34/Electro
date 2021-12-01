using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class MapPiecesGroup : MonoBehaviour
{
    public Room StartRoom => _startRoom;
    [SerializeField] private Room _startRoom;
    public Room EndRoom => _endRoom;
    [SerializeField] private Room _endRoom;

    private List<MapPiece> _allPieces = null;
    private IEnumerable<MapPiece> AllPieces
    {
        get
        {
            if (_allPieces != null && _allPieces.Count != 0)
            {
                return _allPieces;
            }

            _allPieces = Junctions.ToList();

            _allPieces = _allPieces.Concat(Corridors).ToList();
            _allPieces = _allPieces.Concat(Rooms).ToList();

            return _allPieces;
        }
    }

    public IEnumerable<MapPiece> Corridors => _corridors.Cast<MapPiece>();
    [SerializeField] private List<Corridor> _corridors;
    public IEnumerable<MapPiece> Rooms => _rooms.Cast<MapPiece>();
    [SerializeField] private List<Room> _rooms;
    public IEnumerable<MapPiece> Junctions => _junctions.Cast<MapPiece>();
    [SerializeField] private List<Junction> _junctions;


    public void AssertData()
    {
        Debug.Assert(_corridors.Count > 0, "No corridors provided");
        Debug.Assert(_rooms.Count > 0, "No rooms provided");
        Debug.Assert(_startRoom != null, "No Start Room Provided");
        Debug.Assert(_endRoom != null, "No End Room Provided");
    }

    public IEnumerable<MapPiece> GetAllRequiredPieces(bool shouldIncludeRoom)
    {
        return shouldIncludeRoom ? AllPieces : Corridors;
    }
}
