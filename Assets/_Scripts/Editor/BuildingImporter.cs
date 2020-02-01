using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingImporter : AssetPostprocessor
{
    void OnPostProcessSprites(Texture2D texture, Sprite[] sprites)
    {
        Debug.Log("Sprites: " + sprites.Length);
    }
}
