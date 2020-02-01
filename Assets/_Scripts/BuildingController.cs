using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public SpriteRenderer BuildingSpriteRenderer;
    public SpriteRenderer BuildingGrid;

    [Multiline]
    public string BuildingData;
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
                    Value = int.Parse(lines[j][i].ToString())
                };
                GameObject go = new GameObject();
                go.transform.SetParent(GridParent.transform);
                go.transform.localPosition = _grid[j][i].Pos;
                go.name = _grid[j][i].Value.ToString();
            }
        }

        GridParent.transform.localPosition = Vector3.zero;
    }
}
