using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameController>();
            return _instance;
        }
    }
    private enum GameState
    {
        Setup, 
        InGame,
        WaitingToEnd,
        Ended
    }

    private GameState _state;
    private static GameController _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void OnPiecePlacedCorrectly()
    {

    }

    public void GoBackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
