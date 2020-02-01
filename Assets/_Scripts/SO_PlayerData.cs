using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_PlayerData : ScriptableObject
{
    public int BuildingID;
    public int NumberOfArtPieces;
    public int NumberOfManPower;

    public bool IsReady = false;

    private int _currentNumberOfArtPieces;
    private int _currentNumberOfManPower;

    public bool HasManPower { get { return _currentNumberOfManPower > 0; } }
    public bool HasArtPiece { get { return _currentNumberOfArtPieces > 0; } }
}
