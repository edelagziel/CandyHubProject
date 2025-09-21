using System.Collections;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [Header("Grid:")]
    [SerializeField] private TileProfileSO[] _tilesProfile; // All tiles profile
    [SerializeField] private GameObject _tilePrefab; // Board tile prefab
    [Space(10)]
    [SerializeField] private int _width; // Board width
    [SerializeField] private int _height; // Board height
    private GameObject[,] _board; // Board grid


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
            tile.transform.GetChild(0).transform.GetChild(0).transform.GetComponentInChildren<SpriteRenderer>().sprite = _tilesProfile[randomProfile].Icon;
        }
    }

    private void SetCameraPos()
    {
        // Set camera in the center of the board (For all sizes)
        float xPos = (float)(_board.GetLength(0) - 1) / 2;
        float yPos = (float)(_board.GetLength(1) - 1) / 2;

        // Set camera backward to see all board
        //float zPos = (_board.GetLength(0) * 2) * -1;
        Camera.main.orthographicSize = (float)_board.GetLength(0) + 1;

        // Set the new position to main camera
        Camera.main.transform.position = new Vector3(xPos, yPos, Camera.main.transform.position.z);
    }

    private void ClickOnTile()
    {
        // Check if the left mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse pos to world pos (XY plane only)
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            // Check which 2D collider is at that position
            Collider2D hit = Physics2D.OverlapPoint(mousePos2D);

            // Check if raycast hit a tile collider
            if (hit != null)
            {
                GameObject clickedTile = hit.gameObject;

                // DEBUG
                //Debug.Log($"Clicked tile at world pos: {clickedTile.transform.position}");
                StartCoroutine(MarkTile(clickedTile));
            }
        }
    }

    IEnumerator MarkTile(GameObject tile)
    {
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
        sr.color = Color.green;

        yield return new WaitForSeconds(1);

        sr.color = Color.white;
    }
}
