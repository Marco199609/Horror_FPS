using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Tetris_Snake : MonoBehaviour
{
    [SerializeField, Range(1,10)] private int _difficulty = 1;
    [SerializeField] private int _gridWidth = 10, _gridHeight = 15, _snakeInitialLength = 3;
    [SerializeField] private AudioClip _eatClip;
    [SerializeField] private List<Transform> segments = new List<Transform>();

    private List<Transform> _snakeSegments = new List<Transform>();
    private Vector2 _snakeDirection = Vector2.right;

    private bool isGameOver = false, _showGameOverScreen;
    private int _gameOverScreenSquareIndex = 0;

    private Transform[,] grid;
    private Vector2 foodPosition;
    private AudioSource _audioSource;

    private void SetGame()
    {
        grid = new Transform[_gridWidth, _gridHeight];
        _snakeDirection = Vector2.right;

        int segmentIndex = 0;
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                grid[x, y] = segments[segmentIndex].transform;
                segmentIndex++;
            }
        }

        _snakeSegments = new List<Transform>();

        for (int i = 0; i < _snakeInitialLength; i++)
        {
            _snakeSegments.Add(Instantiate(segments[0].gameObject).transform);
            _snakeSegments[i].position = new Vector2(_snakeInitialLength - 1 - i, 0);
            _snakeSegments[i].gameObject.SetActive(true);
        }

        InvokeRepeating("Move", 1f, 0.5f / _difficulty);
        InvokeRepeating("ShowGameOverScreen", 0.01f, 0.01f);
    }


    public void StartGame()
    {
        if(_audioSource == null) _audioSource = gameObject.GetComponent<AudioSource>();
        _showGameOverScreen = true;
        InvokeRepeating("ShowGameOverScreen", 0.01f, 0.01f);
    }

    public void StopGame()
    {
        CancelInvoke();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && _snakeDirection != Vector2.down)
                _snakeDirection = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.DownArrow) && _snakeDirection != Vector2.up)
                _snakeDirection = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && _snakeDirection != Vector2.right)
                _snakeDirection = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.RightArrow) && _snakeDirection != Vector2.left)
                _snakeDirection = Vector2.right;
        }
    }



    private void Move()
    {
        if (!isGameOver)
        {
            Vector2 newHeadPos = (Vector2)_snakeSegments[0].position + _snakeDirection;

            // Check if the new head position is valid
            if (newHeadPos.x < 0 || newHeadPos.x >= _gridWidth ||
                newHeadPos.y < 0 || newHeadPos.y >= _gridHeight)
            {
                GameOver();
                return;
            }

            // Check if the new head position collides with the snake's body
            foreach (Transform segment in _snakeSegments)
            {
                if ((Vector2)segment.position == newHeadPos)
                {
                    GameOver();
                    return;
                }
            }

            // Deactivate the last grid square the snake occupied
            grid[(int)_snakeSegments[_snakeSegments.Count - 1].position.x, (int)_snakeSegments[_snakeSegments.Count - 1].position.y].gameObject.SetActive(false);

            // Move the snake's body segments
            for (int i = _snakeSegments.Count - 1; i > 0; i--)
            {
                _snakeSegments[i].position = _snakeSegments[i - 1].position;
            }

            // Move the snake's head to the new position
            _snakeSegments[0].position = newHeadPos;

            // Activate the grid square at the new head position of the snake
            grid[(int)newHeadPos.x, (int)newHeadPos.y].gameObject.SetActive(true);


            EatFood();
        }
    }

    private void ShowGameOverScreen()
    {
        if (_showGameOverScreen)
        {
            if (_gameOverScreenSquareIndex < segments.Count)
            {
                segments[_gameOverScreenSquareIndex].gameObject.SetActive(true);
                _gameOverScreenSquareIndex++;
            }
            else
            {
                for(int i = 0; i < segments.Count; i++)
                {
                    segments[i].gameObject.SetActive(false);
                }

                isGameOver = false;
                _showGameOverScreen = false;
                _gameOverScreenSquareIndex = 0;
                CancelInvoke();
                SetGame();
                SpawnFood();
            }  
        }
    }

    private void EatFood()
    {
        if ((Vector2)_snakeSegments[0].position == foodPosition)
        {
            // Extend the snake's length by adding a new segment
            Transform newSegment = Instantiate(segments[0], _snakeSegments[_snakeSegments.Count - 1].position, Quaternion.identity);
            _snakeSegments.Add(newSegment);
            _audioSource.PlayOneShot(_eatClip);
            SpawnFood();
        }
    }

    private void SpawnFood()
    {
        int x = Random.Range(0, _gridWidth);
        int y = Random.Range(0, _gridHeight);

        while (grid[x, y].gameObject.activeSelf)
        {
            x = Random.Range(0, _gridWidth);
            y = Random.Range(0, _gridHeight);
        }

        foodPosition = new Vector2 (x, y);
        grid[x, y].gameObject.SetActive(true);
    }

    private void GameOver()
    {
        isGameOver = true;
        _showGameOverScreen = true;
        Debug.Log("Game Over!");
        // Implement your game over logic here.
    }
}