using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MiniGame_Snake : MonoBehaviour, IMiniGame
{
    [SerializeField, Range(1,5)] private int _difficulty = 1;
    [SerializeField] private int _gridWidth = 10, _gridHeight = 15, _snakeInitialLength = 3;
    [SerializeField] private AudioClip _eatClip;
    [SerializeField] private List<Transform> segments = new List<Transform>();

    private bool _isGameOver, _clearScreen;
    private int _clearScreenIndex;
    private List<Transform> _snakeSegments = new List<Transform>();
    private Vector2 _snakeDirection = Vector2.right;
    private Vector2 foodPosition;
    private Transform[,] grid;
    private AudioSource _audioSource;

    public void StartGame()
    {
        if (_audioSource == null) _audioSource = gameObject.GetComponent<AudioSource>();
        GameOver();
    }

    public void StopGame()
    {
        CancelInvoke();
    }

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
            grid[(int)_snakeSegments[i].position.x, (int)_snakeSegments[i].position.y].gameObject.SetActive(true);
        }

        SpawnFood();
        InvokeRepeating("Move", 1f, 0.5f / _difficulty);
    }

    private void Update()
    {
        if (!_isGameOver)
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

        if (!_isGameOver)
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

    

    private void EatFood()
    {
        //Makes food flash
        if (grid[(int)foodPosition.x, (int)foodPosition.y].gameObject.activeInHierarchy) 
            grid[(int)foodPosition.x, (int)foodPosition.y].gameObject.SetActive(false);
        else
            grid[(int)foodPosition.x, (int)foodPosition.y].gameObject.SetActive(true);

        if ((Vector2)_snakeSegments[0].position == foodPosition)
        {
            // Extend the snake's length by adding a new segment
            Transform newSegment = Instantiate(segments[0], _snakeSegments[_snakeSegments.Count - 1].position, Quaternion.identity);
            _snakeSegments.Add(newSegment);
            _audioSource.PlayOneShot(_eatClip);
            grid[(int)foodPosition.x, (int)foodPosition.y].gameObject.SetActive(true); //reverses food flashing
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
        _isGameOver = true;
        _clearScreen = true;
        _clearScreenIndex = 0;
        InvokeRepeating("ClearScreen", 0.003f, 0.003f);
    }

    private void ClearScreen()
    {
        if(_isGameOver)
        {
            if (_clearScreen && _clearScreenIndex < segments.Count)
            {
                segments[_clearScreenIndex].gameObject.SetActive(true);
                _clearScreenIndex++;
            }
            else if (_clearScreen)
            {
                _clearScreen = false;
            }
            else if(_clearScreenIndex > 0)
            {
                _clearScreenIndex--;
                segments[_clearScreenIndex].gameObject.SetActive(false);
            }
            else if(_clearScreenIndex <= 0)
            {
                if(_snakeSegments.Count > 0)
                {
                    for (int i = 0; i < _snakeSegments.Count; i++)
                    {
                        Destroy(_snakeSegments[i].gameObject);
                    }
                }

                _isGameOver = false;
                _clearScreenIndex = 0;
                CancelInvoke();
                SetGame();
            }
        }
    }
}