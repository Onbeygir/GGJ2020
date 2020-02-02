using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    public static BuildingFactory Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<BuildingFactory>();
            }
            return _instance;
        }
    }
    public BuildingController[] BuildingPrefabs;

    private BuildingController _currentBuilding;
    private int _currentIndex;
    private static BuildingFactory _instance;

    private bool _startDone = false;

    private void Start()
    {
        if (!_startDone)
        {
            _startDone = true;
            _currentBuilding = BuildingController.Instance;
            for (int i = 0; i < BuildingPrefabs.Length; i++)
            {
                if (BuildingPrefabs[i].Equals(_currentBuilding))
                {
                    _currentIndex = i;
                }
            }
        }
    }

    public void BuildBuilding(GameObject buildingPrefab)
    {
        Start();
        for (int i = 0; i < BuildingPrefabs.Length; i++)
        {
            if(buildingPrefab.name == BuildingPrefabs[i].name)
            {
                Build(i);
            }
        }
    }

    private void Build(int index)
    {
        if(_currentBuilding != null)
            Destroy(_currentBuilding.gameObject);

        _currentBuilding = Instantiate(BuildingPrefabs[index]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(_currentBuilding.gameObject);
            _currentIndex = (_currentIndex+1) % (BuildingPrefabs.Length);
            _currentBuilding = Instantiate(BuildingPrefabs[_currentIndex]);
        }
    }
}
