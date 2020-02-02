using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public static BuildingController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BuildingController>();
            }
            return _instance;
        }
    }
    private static BuildingController _instance;
    public SpriteRenderer BuildingSpriteRenderer;
    public SpriteRenderer BuildingGrid;

    [Multiline]
    public string BuildingData;
    public float MaxPlacementDistance;
    public int PixelUnit = 100;
    public int BoxPixelWidth = 9;

    public GameObject GridParent;

    private GridSlot[][] _grid;

    private struct GridSlot
    {
        public Vector2 Pos;
        public int Value;
    }

    public void Awake()
    {
        _instance = this;
        if (BuildingData != null && BuildingData.Length != 0)
        {
            var pos = GridParent.transform.position;
            Destroy(GridParent.gameObject);
            GridParent = new GameObject();
            GridParent.transform.SetParent(transform);
            GridParent.transform.position = pos;
            GridParent.name = "GridParent";


            GenerateBuildingData();
        }
    }

    private void GenerateBuildingData()
    {
        string[] lines = BuildingData.Split(new string[] { "\n", System.Environment.NewLine }, System.StringSplitOptions.None);
        float mid = lines[0].Length / 2f;
        Debug.Log(mid);


        float distanceBetween = (float)BoxPixelWidth / PixelUnit;
        _grid = new GridSlot[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            string item = (string)lines[i];
            _grid[i] = new GridSlot[lines[i].Length];
        }

        for (int j = 0; j < lines.Length; j++)
        {
            for (int i = 0; i < lines[j].Length; i++)
            {
                _grid[j][i] = new GridSlot()
                {
                    Pos = new Vector2(GridParent.transform.localPosition.x + i * distanceBetween, GridParent.transform.localPosition.y + j * distanceBetween),
                    Value = int.Parse(lines[lines.Length - 1 - j][i].ToString())
                };
                GameObject go = new GameObject();
                go.transform.SetParent(GridParent.transform);
                go.transform.localPosition = _grid[j][i].Pos;
                go.name = _grid[j][i].Value.ToString();
            }
        }

        GridParent.transform.localPosition = Vector3.zero;
    }

    public bool TryPlacingPiece(RepairPiece piece, out Vector2 snapPos)
    {
        //piece.PiecePattern.LeadPosition
        float distance = float.PositiveInfinity;
        Vector2 piecePos = piece.transform.position;
        float tempDist;
        int y = 0, x = 0;
        for (int i = 0; i < _grid.Length; i++)
        {
            GridSlot[] item = (GridSlot[])_grid[i];
            for (int j = 0; j < item.Length; j++)
            {
                GridSlot slot = (GridSlot)item[j];
                tempDist = Vector2.Distance(slot.Pos, piecePos);
                if(tempDist < distance)
                {
                    distance = tempDist;
                    y = i;
                    x = j;
                }
            }
        }
        GridSlot gs = _grid[y][x];
        if (distance < MaxPlacementDistance && gs.Value == 0)
        {
            snapPos = gs.Pos;
            return true;
        }
        else
        {
            snapPos = Vector2.zero;
            return false;
        }
    }
}
