using UnityEngine;

public class FoxFood : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _FoxGridArea;

    private void Start()
    {
        FoxRadomizPos();
    }
    private void FoxRadomizPos()
    {
        Bounds bounds = _FoxGridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FoxRadomizPos();
    }
}
