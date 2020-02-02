using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public EventSystem ESystem;
    public Button DoneButton;
    public WinscreenController Winscreen;
    public GameObject LoseScreen;
    public float DoneThreshold = 0.65f;
    public ClockController Clock;
    public float GameTimeLength = 45f;
    public SO_PlayerData PlayerData;

    private GameState _state;
    private static GameController _instance;

    private int _currentScore = 0;
    [SerializeField] private float _completePerc;

    private void Awake()
    {
        _instance = this;
        DoneButton.onClick.AddListener(OnDoneButtonPressed);

        OnGameStarted();
    }

    public void OnGameStarted()
    {
        _currentScore = 0;
        DoneButton.gameObject.SetActive(false);
        Winscreen.HideScreen();
        LoseScreen.gameObject.SetActive(false);
        Clock.StartClock(GameTimeLength, OnTimeUp);

        PieceFactory.Instance.CreatePieces();
    }

    public void OnTimeUp()
    {
        EndGame(CalculateSuccess());

    }

    public void OnPiecePlacedCorrectly(RepairPiece piece)
    {
        _currentScore += piece.BoxPositions.Length;
        _completePerc = (float)_currentScore / BuildingController.Instance.ScoreAvailable;
        PieceFactory.Instance.OnPiecePlaced(piece);

        if(DoneThreshold < _completePerc && !DoneButton.isActiveAndEnabled)
        {
            Debug.Log("Enable");
            DoneButton.gameObject.SetActive(true);
        }

        if(_completePerc == 1f)
        {
            EndGame(true);
        }
    }

    public void OnDoneButtonPressed()
    {
        EndGame(CalculateSuccess());
    }

    public bool CalculateSuccess()
    {
        return (_completePerc > DoneThreshold);        
    }

    public void EndGame(bool success)
    {
        Debug.Log("GameEnded");
        if(success)
            Winscreen.ShowWinscreen(_completePerc);
        else
        {
            //todo crit failure
            LoseScreen.gameObject.SetActive(true);
        }
    }

    public void GoBackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
