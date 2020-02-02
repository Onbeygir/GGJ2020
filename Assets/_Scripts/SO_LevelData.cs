using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_",menuName = "Create Level Data")]
public class SO_LevelData : ScriptableObject
{
    public string LevelTier = "";
    public string LevelName = "";
    public GameObject LevelPrefab;
}
