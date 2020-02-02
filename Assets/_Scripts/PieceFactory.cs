using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
    public static PieceFactory Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PieceFactory>();
            return _instance;
        }
    }
    public GameObject RepairPiecePrefab;
    public GameObject RepairBoxPrefab;
    public GameObject ArtBoxPrefab;

    public SO_PiecePattern ArtPattern;


    public Transform LeftPiecePosition;
    public Transform RightPiecePosition;
    public Transform JokerPiecePosition;

    public SO_PiecePattern[] Patterns;

    private RepairPiece[] _createdPieces = new RepairPiece[3]; //max 3
    private static PieceFactory _instance;

    public void CreatePieces()
    {
        for (int i = 0; i < _createdPieces.Length; i++)
        {
            var p = _createdPieces[i];
            if (p != null)
            {
                DisposePiece(p);
            }

            _createdPieces[i] = CreatePiece();
        }
        _createdPieces[0].transform.position = LeftPiecePosition.position;
        _createdPieces[1].transform.position = RightPiecePosition.position;
        _createdPieces[2].transform.position = JokerPiecePosition.position;


        _createdPieces[0].Setup(RepairBoxPrefab, GetRandomPattern());
        _createdPieces[1].Setup(RepairBoxPrefab, GetRandomPattern());
        //        if(data != null && data.HasArtPiece)
        _createdPieces[2].Setup(ArtBoxPrefab, ArtPattern, true);
    }

    public void OnPiecePlaced(RepairPiece placedPiece)
    {
        for (int i = 0; i < _createdPieces.Length; i++)
        {
            RepairPiece item = _createdPieces[i];
            if (item != placedPiece)
            {
                Destroy(item.gameObject);
            }
            _createdPieces[i] = null;
        }

        CreatePieces();
    }

    private SO_PiecePattern GetRandomPattern()
    {
        int randomIndex = Random.Range(0, Patterns.Length);
        return Patterns[randomIndex];

    }

    private RepairPiece CreatePiece()
    {
        var p = Instantiate(RepairPiecePrefab).GetComponent<RepairPiece>();
        return p;
    }

    private void DisposePiece(RepairPiece p)
    {
        Destroy(p.gameObject);
    }
}
