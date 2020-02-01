using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{


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
