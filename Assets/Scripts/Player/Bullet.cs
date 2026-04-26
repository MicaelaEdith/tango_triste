using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 12f;
    [SerializeField]
    private float topLimit = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > topLimit)
        {
            Destroy(gameObject);
        }
    }
}