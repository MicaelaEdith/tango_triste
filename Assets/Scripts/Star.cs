using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float bottomLimit;
    private float topRespawnMin;
    private float topRespawnMax;

    public void Init(float speed, float bottomLimit, float topMin, float topMax, Color color)
    {
        this.speed = speed;
        this.bottomLimit = bottomLimit;
        this.topRespawnMin = topMin;
        this.topRespawnMax = topMax;

        GetComponent<SpriteRenderer>().color = color;
    }

    void Update()
    {
        float finalSpeed = speed * GameManager.SpeedMultiplier;

        transform.Translate(Vector3.down * finalSpeed * Time.deltaTime);

        if (transform.position.y < bottomLimit)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        float newY = Random.Range(topRespawnMin, topRespawnMax);
        float newX = transform.position.x;

        transform.position = new Vector3(newX, newY, 0);
    }
}