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

    private int _currentScore = 0;
    [SerializeField] private float _completePerc;

    private void Awake()
    {
        _instance = this;
    }

    public void OnGameStarted()
    {
        _currentScore = 0;
    }

    public void OnPiecePlacedCorrectly(RepairPiece piece)
    {
        _currentScore += piece.BoxPositions.Length;
        _completePerc = (float)_currentScore / BuildingController.Instance.ScoreAvailable;
        PieceFactory.Instance.OnPiecePlaced(piece);
    }

    public void GoBackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
