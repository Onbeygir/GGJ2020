using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

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

    public SO_PiecePattern PiecePattern;
    public Transform LeadingBox;

    private Vector3 _initialPos;

    public void Setup(GameObject boxPrefab, SO_PiecePattern pattern, bool isArt = false ,int boxPixelSize = 9, int pixelUnit = 100)
    {
        PiecePattern = pattern;
        if (isArt)
        {
            BoxPositions = new Vector2[] { Vector2.one};
        }
        else
        {
            BoxPositions = PiecePattern.BoxPositions;
            
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

            if(PiecePattern.LeadPosition == pos)
            {
                LeadingBox = box.transform;
            }
        }

        _dragState = DragStates.NotDragging;
        _initialPos = transform.position;
    }

    public void OnDragStarted(BaseEventData data)
    {
        if (_locked) return;
        //Debug.Log("DragStarted");
        _dragState = DragStates.Dragging;
    }
    public void OnDragEnded(BaseEventData data)
    {
        if (_dragState != DragStates.Dragging) return;

        //Debug.Log("DragEnded");
        _dragState = DragStates.WasDragging;
        Vector2 snapPos;
        if(BuildingController.Instance.TryPlacingPiece(this, out snapPos))
        {
            //success
            _locked = true;
            var tw = transform.DOMove(snapPos, .2f);
            GameController.Instance.OnPiecePlacedCorrectly(this);
        }
        else
        {
            //return back
            _locked = true;
            var tw = transform.DOMove(_initialPos, 1f);
            tw.SetEase(Ease.OutBack);
            tw.onComplete += () => { _locked = false; };
        }
    }
    public void OnDragging(BaseEventData data)
    {
        if (_dragState != DragStates.Dragging) return;

        //Debug.Log("Dragging");
        Vector2 dragPos = Vector2.zero;
        dragPos = Camera.main.ScreenToWorldPoint(data.currentInputModule.input.mousePosition);
        transform.position = dragPos;
        data.Use();
    }

}

