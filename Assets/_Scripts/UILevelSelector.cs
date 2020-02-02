using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelSelector : MonoBehaviour
{
    public SO_LevelData[] LevelData;
    public UISelectableLevel SelectablePrefab;
    public GameObject ContentParent;

    public SO_LevelData CurrentLevelData;

    private void Awake()
    {
        FillContent();
    }
    public void FillContent()
    {
        foreach (var ld in LevelData)
        {
            UISelectableLevel uiLevel = Instantiate(SelectablePrefab);
            uiLevel.LevelName.text = ld.LevelName;
            uiLevel.transform.SetParent(ContentParent.transform);
            uiLevel.transform.localScale = Vector3.one;
            uiLevel.LevelData = ld;
            uiLevel.LevelButton.onClick.AddListener(() =>
            {
                CurrentLevelData = uiLevel.LevelData;
            });
        }
    }

    
}
