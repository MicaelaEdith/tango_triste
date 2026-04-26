using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private int meteorCount = 25;

    [SerializeField]
    private float minSpeed = 2.5f;
    [SerializeField]
    private float maxSpeed = 4f;

    [SerializeField]
    private float minRotation = -150f;
    [SerializeField]
    private float maxRotation = 150f;

    private float leftX;
    private float rightX;
    private float bottomLimit;
    private float spawnMinY;
    private float spawnMaxY;

    void Start()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        float camTop = cam.transform.position.y + height / 2f;
        float camBottom = cam.transform.position.y - height / 2f;

        leftX = cam.transform.position.x - width / 2f;
        rightX = cam.transform.position.x + width / 2f;

        bottomLimit = camBottom - 1f;

        spawnMinY = camTop + 1f;
        spawnMaxY = camTop + height;

        if (GameManager.Level == 1) meteorCount = 20;
        if (GameManager.Level == 3) meteorCount = 35;

        SpawnAll();
    }

    void SpawnAll()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            GameObject obj = Instantiate(meteorPrefab);

            Meteor meteor = obj.GetComponent<Meteor>();

            float speed = Random.Range(minSpeed, maxSpeed);
            float rotation = Random.Range(minRotation, maxRotation);

            meteor.Init(
                speed,
                rotation,
                bottomLimit,
                spawnMinY,
                spawnMaxY,
                leftX,
                rightX
            );
        }
    }
}