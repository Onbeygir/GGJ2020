using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public float TimeToWaitBeforeStarting = 1.2f;
    public GameObject StartPopup;
    public UIValueController Artwork;
    public UIValueController ManPower;
    public UILevelSelector BuildingSelector;
    public SO_PlayerData PlayerData;

    public void OnGameReadyPressed()
    {
        if (BuildingSelector.CurrentLevelData == null)
        {
            //ignore
        }
        else
        {
            PlayerData.Setup(BuildingSelector.CurrentLevelData, Artwork.CurrentValue, ManPower.CurrentValue);
            SceneManager.LoadScene("Boardgame");
        }

        
    }

    public void ShowStart()
    {
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        StartPopup.gameObject.SetActive(true);
        yield return new WaitForSeconds(TimeToWaitBeforeStarting);
        SceneManager.LoadScene("Boardgame");
    }

    public void OnCampaignPressed()
    {
        SceneManager.LoadScene("Campaign");
    }
    public void OnBoardgamePressed()
    {
        SceneManager.LoadScene("Setup");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
