using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _gridArea;

    private void Start()
    {
        RandomizePosition();
    }
    private void RandomizePosition()
    {
        Bounds bounds = _gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
    }
}
