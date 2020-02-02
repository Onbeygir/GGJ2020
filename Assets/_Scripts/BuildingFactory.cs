using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{

    public BuildingController[] BuildingPrefabs;

    private BuildingController _currentBuilding;
    private int _currentIndex;

    private void Start()
    {
        _currentBuilding = BuildingController.Instance;
        for (int i = 0; i < BuildingPrefabs.Length; i++)
        {
            if (BuildingPrefabs[i].Equals(_currentBuilding))
            {
                _currentIndex = i;
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(_currentBuilding.gameObject);
            _currentIndex = (_currentIndex+1) % (BuildingPrefabs.Length-1);
            _currentBuilding = Instantiate(BuildingPrefabs[_currentIndex]);
        }
    }
}
