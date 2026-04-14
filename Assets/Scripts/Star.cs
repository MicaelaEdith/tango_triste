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

        float moveY = -finalSpeed;
        float moveX = GameManager.HorizontalDirection * GameManager.HorizontalInfluence;

        transform.Translate(new Vector3(moveX, moveY, 0f) * Time.deltaTime);

        if (transform.position.y < bottomLimit)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        float leftEdge = cam.transform.position.x - screenWidth / 2f;
        float rightEdge = cam.transform.position.x + screenWidth / 2f;

        float newX = Random.Range(leftEdge, rightEdge);
        float newY = Random.Range(topRespawnMin, topRespawnMax);

        transform.position = new Vector3(newX, newY, 0f);
    }
}