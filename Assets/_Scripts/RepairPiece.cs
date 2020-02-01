using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPiece : MonoBehaviour
{
    public Vector2[] BoxPositions;

    public void Setup(GameObject boxPrefab, Vector2[] boxPositions, bool isArt = false ,int boxPixelSize = 9, int pixelUnit = 100)
    {
        if (isArt)
        {
            BoxPositions = new Vector2[] { Vector2.one};
        }
        else
        {
            BoxPositions = boxPositions;
            
        }

        foreach (var pos in BoxPositions)
        {
            GameObject box = Instantiate(boxPrefab);
            float x = pos.x - 1;
            float y = pos.y - 1;
            x *= (float)boxPixelSize / pixelUnit;
            y *= (float)boxPixelSize / pixelUnit;
            box.transform.SetParent(transform);
            box.transform.localPosition = new Vector3(x, y, 0f);
            
        }


    }

    public void OnDragStarted()
    {

    }
    public void OnDragEnded()
    {

    }
    public void OnDragging()
    {

    }

}

