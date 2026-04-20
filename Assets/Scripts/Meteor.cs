using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float speed;
    private float rotationSpeed;

    private float bottomLimit;
    private float spawnMinY;
    private float spawnMaxY;
    private float leftX;
    private float rightX;

    public void Init(
        float speed,
        float rotationSpeed,
        float bottomLimit,
        float spawnMinY,
        float spawnMaxY,
        float leftX,
        float rightX)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.bottomLimit = bottomLimit;
        this.spawnMinY = spawnMinY;
        this.spawnMaxY = spawnMaxY;
        this.leftX = leftX;
        this.rightX = rightX;

        Respawn();
    }

    void Update()
    {
        if (GameManager.Level != 1) return;

        transform.position += Vector3.down * speed * Time.deltaTime;

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (transform.position.y < bottomLimit)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        float x = Random.Range(leftX, rightX);
        float y = Random.Range(spawnMinY, spawnMaxY);

        transform.position = new Vector3(x, y, 0f);

        rotationSpeed = Random.Range(-150f, 150f);
    }
}