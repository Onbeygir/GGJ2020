using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RepairPiece : MonoBehaviour
{
    public Vector2[] BoxPositions;

    public enum DragStates
    {
        NotDragging,
        Dragging,
        WasDragging
    }

    [SerializeField] protected bool _locked;
    [SerializeField] protected DragStates _dragState;


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

    public void OnDragStarted(BaseEventData data)
    {
        Debug.Log("DragStarted");
    }
    public void OnDragEnded(BaseEventData data)
    {
        Debug.Log("DragEnded");
    }
    public void OnDragging(BaseEventData data)
    {
        Debug.Log("Dragging");
        Vector2 dragPos = Vector2.zero;
        dragPos = Camera.main.ScreenToWorldPoint(data.currentInputModule.input.mousePosition);
        transform.position = dragPos;
        data.Use();
    }

}

