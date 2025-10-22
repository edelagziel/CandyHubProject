using System.Collections;
using TMPro;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [Header("Tiles:")]
    [SerializeField] private TileProfileSO[] _tilesProfile; // All tiles profile
    [SerializeField] private GameObject _tilePrefab; // Board tile prefab
    
    [Header("Grid:")]
    [SerializeField] private int _width; // Board width
    [SerializeField] private int _height; // Board height
    private GameObject[,] _board; // Board grid

    [Header("Player:")]
    [SerializeField] private TileType _currentChoose; // Current attack tool

    [Header("UI:")]
    [SerializeField] private TextMeshProUGUI resultText;


    private void Start()
    {
        SetupBoard();
        SetCameraPos();
    }

    private void LateUpdate()
    {
        ClickOnTile();
    }

    private void SetupBoard()
    {
        try
        {
            _board = new GameObject[_width, _height];

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    GameObject tempTile = _board[j, i] = Instantiate(_tilePrefab, new Vector2(j, i), Quaternion.identity);
                    SetRandomTileProfile(tempTile);
                }
            }

            Debug.Log("Board has set successfully !");
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error loading board:\n{e}");
        }
    }

    private void SetRandomTileProfile(GameObject tile)
    {
        // Get Random type for tile
        int randomProfile = Random.Range(0, _tilesProfile.Length);

        // Set random type for tile
        if (_tilesProfile[randomProfile] != null && _tilesProfile[randomProfile].Icon != null)
        {
            tile.GetComponent<TileManager>()._tileProfile = _tilesProfile[randomProfile];
            tile.transform.GetChild(0).transform.GetChild(0).transform.GetComponentInChildren<SpriteRenderer>().sprite = _tilesProfile[randomProfile].Icon;
        }
    } // DEBUG

    private void SetCameraPos()
    {
        // Set camera in the center of the board (For all sizes)
        float xPos = (float)(_board.GetLength(0) - 1) / 2;
        float yPos = (float)(_board.GetLength(1) - 1) / 2;

        // Set camera backward to see all board
        float zPos = (_board.GetLength(0) * 2) * -1;

        // Set the new position to main camera
        Camera.main.transform.position = new Vector3(xPos, yPos, zPos);
    }

    public void ChooseAttack_Rock() => _currentChoose = TileType.Rock;

    public void ChooseAttack_Paper() => _currentChoose = TileType.Paper;

    public void ChooseAttack_Scissors() => _currentChoose = TileType.Scissors;

    private void ClickOnTile()
    {
        // Check if player choose attack tool
        if (_currentChoose == TileType.None) { return; }


        // Check if the left mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Define the board plane (XY plane at Z = 0)
            Plane xyPlane = new Plane(Vector3.forward, Vector3.zero);

            if (xyPlane.Raycast(ray, out float distance))
            {
                // Get world point where the ray hits the plane
                Vector3 worldPoint = ray.GetPoint(distance);

                // Use OverlapPoint to detect which tile is here
                Collider2D hit = Physics2D.OverlapPoint(worldPoint);

                // Check if raycast hit a tile collider
                if (hit != null)
                {
                    StartCoroutine(CheckResult(hit.gameObject));
                }
            }
        }
    }

    IEnumerator CheckResult(GameObject tile)
    {
        // Get selected tile sprite renderer
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();

        // Show selected tile and disable all board
        //ChangeBoardActive(false);
        sr.color = Color.green;

        // Check for winner
        TileType opponentTileType = tile.GetComponentInParent<TileManager>()._tileProfile.TileType;
        switch (_currentChoose)
        {
            case TileType.Rock:
                if (opponentTileType == TileType.Rock) { resultText.text = "Tie"; }
                else if (opponentTileType == TileType.Paper) { resultText.text = "Lose"; }
                else if (opponentTileType == TileType.Scissors) { resultText.text = "Win"; }
                break;

            case TileType.Paper:
                if (opponentTileType == TileType.Rock) { resultText.text = "Win"; }
                else if (opponentTileType == TileType.Paper) { resultText.text = "Tie"; }
                else if (opponentTileType == TileType.Scissors) { resultText.text = "Lose"; }
                break;

            case TileType.Scissors:
                if (opponentTileType == TileType.Rock) { resultText.text = "Lose"; }
                else if (opponentTileType == TileType.Paper) { resultText.text = "Win"; }
                else if (opponentTileType == TileType.Scissors) { resultText.text = "Tie"; }
                break;
        }

        // Reset current attack tool
        _currentChoose = TileType.None;

        // wait 2 seconds
        yield return new WaitForSeconds(2);

        // Release selected tile and enable all board
        //ChangeBoardActive(true);
        sr.color = Color.white;
    }

    private void ChangeBoardActive(bool toEnable)
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _board[j, i].gameObject.SetActive(toEnable);
            }
        }
    }
}
