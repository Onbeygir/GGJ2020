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
    public int BuildingIndex;
    public SO_PlayerData PlayerData;

    public void OnGameReadyPressed()
    {
        PlayerData.Setup(BuildingIndex, Artwork.CurrentValue, ManPower.CurrentValue);
        
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
        SceneManager.LoadScene("Boardgame");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
