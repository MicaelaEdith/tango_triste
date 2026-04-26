using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject garbagePrefab;
    [SerializeField]
    private float spawnCooldown = 5f;

    private float timer;

    private float leftX;
    private float rightX;
    private float topY;
    private float bottomY;

    private PlayerHealth playerHealth;

    void Start()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        leftX = cam.transform.position.x - width / 2f;
        rightX = cam.transform.position.x + width / 2f;
        topY = cam.transform.position.y + height / 2f + 1f;
        bottomY = cam.transform.position.y - height / 2f - 1f;

        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth == null) return;

        if (playerHealth.currentHealth > 75 && GameManager.Level != 2)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnCooldown)
        {

            if(GameManager.garbage < 5 || GameManager.Level == 2){
                SpawnGarbage();
                timer = 0f;

            }
        }
    }

    void SpawnGarbage()
    {
        float x = Random.Range(leftX, rightX);

        GameObject obj = Instantiate(garbagePrefab, new Vector3(x, topY, 0f), Quaternion.identity);

        SpaceGarbage garbage = obj.GetComponent<SpaceGarbage>();
        garbage.Init(bottomY);
    }
}