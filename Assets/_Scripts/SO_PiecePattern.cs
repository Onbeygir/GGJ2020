using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pattern", menuName = "Create Pattern")]
public class SO_PiecePattern : ScriptableObject
{
    [Header("Must be between 0 to 2(including 2)")]
    public Vector2[] BoxPositions;

    public Vector2 LeadPosition;

    public Vector2 PositionalDifference(int boxPositionIndex)
    {
        return LeadPosition - BoxPositions[boxPositionIndex];
    }
    public Vector2 PositionalDifference(Vector2 pos)
    {
        return LeadPosition - pos;
    }
}
