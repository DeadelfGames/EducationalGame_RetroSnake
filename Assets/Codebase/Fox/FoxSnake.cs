using System.Collections.Generic;
using UnityEngine;


public class FoxSnake : MonoBehaviour
{
    [SerializeField] private Transform _foxPrefab;
    [SerializeField] private int _sizeFox = 4;
    private List<Transform> _foxSegments = new List<Transform>();

    private Vector2 _foxInput;
    private Vector2 _foxDirection;

    private void Start()
    {
        FoxReserPos();
    }

    private void FixedUpdate()
    {
        FoxMove();
    }

    private void Update()
    {
        FoxInput();
    }

    private void FoxInput()
    {
        if (_foxDirection.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W))
                _foxInput = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.S))
                _foxInput = Vector2.down;
        }
        else if (_foxDirection.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.A))
                _foxInput = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.D))
                _foxInput = Vector2.right;
        }
    }

    private void FoxMove()
    {
        if (_foxInput != Vector2.zero)
            _foxDirection = _foxInput;

        for (int i = _foxSegments.Count - 1; i > 0; i--)
        {
            _foxSegments[i].position = _foxSegments[i - 1].position;
        }

        float x = Mathf.Round(transform.position.x) + _foxDirection.x;
        float y = Mathf.Round(transform.position.y) + _foxDirection.y;

        transform.position = new Vector2(x, y);
    }

    private void FoxGrow()
    {
        Transform foxSegment = Instantiate(_foxPrefab);
        foxSegment.position = _foxSegments[_foxSegments.Count - 1].position;
        _foxSegments.Add(foxSegment);
    }
    private void FoxReserPos()
    {
        transform.position = Vector2.zero;
        _foxDirection = Vector2.right;

        for (int i = 1; i < _foxSegments.Count; i++)
        {
            Destroy(_foxSegments[i].gameObject);
        }

        _foxSegments.Clear();
        _foxSegments.Add(transform);

        for (int i = 0; i < _sizeFox - 1; i++)
        {
            FoxGrow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
            FoxGrow();
        else if (other.gameObject.CompareTag("Obstacle"))
            FoxReserPos();
    }
}

