using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Create Player Data")]
public class SO_PlayerData : ScriptableObject
{
    public SO_LevelData BuildingData;
    public int NumberOfArtPieces;
    public int NumberOfManPower;

    public bool IsReady = false;

    private int _currentNumberOfArtPieces;
    private int _currentNumberOfManPower;

    public bool HasManPower { get { return _currentNumberOfManPower > 0; } }
    public bool HasArtPiece { get { return _currentNumberOfArtPieces > 0; } }

    public void Setup(SO_LevelData buildingData, int no_artPieces, int no_manpower)
    {
        BuildingData = buildingData;
        NumberOfArtPieces = no_artPieces;
        NumberOfManPower = no_manpower;
    }
}
