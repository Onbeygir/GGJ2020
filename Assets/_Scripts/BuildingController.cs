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
                float halfX = (float)lines[j].Length / 2;
                _grid[j][i] = new GridSlot()
                {
                    Pos = new Vector2(GridParent.transform.position.x + (i- halfX) * distanceBetween, GridParent.transform.position.y + (j-.5f) * distanceBetween),
                    Value = int.Parse(lines[lines.Length - 1 - j][i].ToString())
                };
                GameObject go = new GameObject();
                go.transform.SetParent(GridParent.transform);
                go.transform.position = _grid[j][i].Pos;
                go.name = _grid[j][i].Value.ToString();
            }
        }

        //GridParent.transform.localPosition = Vector3.zero;
    }

    public bool TryPlacingPiece(RepairPiece piece, out Vector2 snapPos)
    {
        //piece.PiecePattern.LeadPosition
        float distance = float.PositiveInfinity;
        Vector2 piecePos = piece.LeadingBox.position;
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
                    //Debug.Log(i + "/" + j + " closest distance " + tempDist);
                    distance = tempDist;
                    y = i;
                    x = j;
                }
            }
        }
        GridSlot gs = _grid[y][x];
        GameObject ng1 = new GameObject();
        ng1.transform.position = piece.LeadingBox.position;
        ng1.name = "piece.LeadingBox.position";

        GameObject ng2 = new GameObject();
        ng2.transform.position = gs.Pos;
        ng2.name = "gs.Pos";

        bool inSlot = false;
        
        if (distance < MaxPlacementDistance && gs.Value == 0)
        {
            snapPos = gs.Pos;
            inSlot = true;
        }
        else
        {
            snapPos = Vector2.zero;
            inSlot = false;
            return false;
        }
        Vector2Int coord = new Vector2Int(y, x);
        List<Vector2Int> sum = new List<Vector2Int>();
        //sum my bitch up DUUUUU du du duuu dududu
        foreach (Vector2 boxPos in piece.PiecePattern.BoxPositions)
        {
            Vector2 p = piece.PiecePattern.PositionalDifference(boxPos);
            p = new Vector2(p.y, p.x);
            p = coord + p;
            int value = _grid[(int)p.y][(int)p.x].Value;
            //Debug.Log(value);
            if (value > 0)
            {
                Debug.Log("OVERLAPPING!");
                return false;
            }
            else
            {
                sum.Add(new Vector2Int((int)p.x, (int)p.y));
            }
        }
        bool grounded = false;
        foreach (var boxPos in piece.PiecePattern.BoxPositions)
        {
            Vector2 p = piece.PiecePattern.PositionalDifference(boxPos);
            p = new Vector2(p.y, p.x);
            Vector2Int pInt = new Vector2Int((int)p.x, (int)p.y);
            pInt = coord + pInt;
            pInt.x += 1;
            if (_grid[pInt.y][pInt.x].Value > 0)
                grounded = true;
        }
        if (!grounded)
        {
            Debug.Log("NOT GROUNDED!");
            return false;
        }

        foreach (var item in sum)
        {
            _grid[item.x][item.y].Value++;
        }
        Debug.Log("yes");
        return inSlot;
    }

    private void OnDrawGizmos()
    {
        if (_grid == null) return;
        foreach (var item in _grid)
        {
            foreach (var slot in item)
            {
                Gizmos.color = Color.yellow;
                if (slot.Value == 2)
                    Gizmos.color = Color.blue;
                if (slot.Value == 1)
                    Gizmos.color = Color.green;
                Gizmos.DrawSphere(slot.Pos, .01f);
            }
        }
    }
}
