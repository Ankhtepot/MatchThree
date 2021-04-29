using Enums;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

//Fireball Games * * * PetrZavodny.com

public class Board : MonoBehaviour
{
#pragma warning disable 649
    public int width;
    public int height;

    public int borderSize;
    
    public GameObject tilePrefab;
    public GameObject[] gamePiecePrefabs;
    public EInterpType gamePieceInterpolation = EInterpType.SmootherStep;
    public Camera mainCamera;

    private Tile[,] m_allTiles;
    private GamePiece[,] m_allGamePieces;
#pragma warning restore 649

    private void Start()
    {
        m_allTiles = new Tile[width, height];
        m_allGamePieces = new GamePiece[width, height];
        
        SetupTiles();
        SetupCamera();
        FillRandom();
    }

    private void SetupTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), quaternion.identity);
                tile.name = $"Tile ({i},{j})";

                m_allTiles[i, j] = tile.GetComponent<Tile>();
                m_allTiles[i, j].Init(i, j, this);

                tile.transform.parent = transform;
            }
        }
    }

    private void SetupCamera()
    {
        mainCamera.transform.position = new Vector3((width - 1)/2f, (height - 1)/2f, -10f);

        float aspectRatio = Screen.width / (float) Screen.height;
        float verticalSize = height / 2f + borderSize;
        float horizontalSize = (width / 2f + borderSize) / aspectRatio;

        mainCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
    }

    private GameObject GetRandomGamePiece()
    {
        int randomIdx = Random.Range(0, gamePiecePrefabs.Length);

        if (!gamePiecePrefabs[randomIdx])
        {
            Debug.LogWarning($"BOARD: {randomIdx} does not contain a valid GamePiece prefab!");
        }

        return gamePiecePrefabs[randomIdx];
    }

    private void PlaceGamePiece(GamePiece gamePiece, int x, int y, EInterpType interpolation)
    {
        if (!gamePiece)
        {
            Debug.LogWarning($"BOARD: invalid gamePiece.");
            return;
        }

        gamePiece.interpolation = interpolation;
        gamePiece.transform.position = new Vector3(x, y, 0);
        gamePiece.transform.rotation = Quaternion.identity;
        gamePiece.SetCoord(x, y);
    }

    private void FillRandom()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity);

                if (!randomPiece) return;
                
                PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j, gamePieceInterpolation);
            }
        }
    }
}
