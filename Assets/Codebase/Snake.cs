using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    [SerializeField] private int _initialSize = 4;
    [SerializeField] private Transform _segmentPrefab;

    private List<Transform> _segments = new List<Transform>();

    private Vector2 _direction = Vector2.right;
    private Vector2 _input;


    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _input = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _input = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _input = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _input = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        // Set the new direction based on the input
        if (_input != Vector2.zero)
        {
            _direction = _input;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        float x = Mathf.Round(transform.position.x) + _direction.x;
        float y = Mathf.Round(transform.position.y) + _direction.y;

        transform.position = new Vector2(x, y);
    }

    public void Grow()
    {
        Transform segment = Instantiate(_segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }
 
    public void ResetState()
    {
        _direction = Vector2.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        _segments.Clear();
        _segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < _initialSize - 1; i++)
        {
            Grow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }
}