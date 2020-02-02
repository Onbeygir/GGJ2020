using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_",menuName = "Create Level Data")]
public class SO_LevelData : ScriptableObject
{
    public int LevelTier;
    public int LevelIndex;
    public string LevelName = "";
    public GameObject LevelPrefab;
}
