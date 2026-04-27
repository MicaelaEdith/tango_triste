using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnInterval = 0.1f;
    [SerializeField]
    private int spawnBatchSize = 6;
    [SerializeField]
    private int maxShips = 200;

    private float timer;
    private int currentShips = 0;

    private float leftX;
    private float rightX;
    private float topY;
    private float bottomLimit;
    public int count = 0;

    void Start()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        leftX = cam.transform.position.x - width / 2f;
        rightX = cam.transform.position.x + width / 2f;

        topY = cam.transform.position.y + height / 2f + 1f;

        bottomLimit = cam.transform.position.y - height / 2f - 5f;
    }

    void Update()
    {
        if(GameManager.Level == 3){

            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnBatch(spawnBatchSize);
            }
        }
    }

    void SpawnBatch(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (currentShips >= maxShips) return;

            float x = Random.Range(leftX, rightX);

            GameObject obj = Instantiate(enemyPrefab, new Vector3(x, topY, 0f), Quaternion.identity);

            EnemyShip ship = obj.GetComponent<EnemyShip>();
            ship.Init(bottomLimit, this);

            currentShips++;
        }
    }

    public void OnShipDestroyed()
    {
        currentShips--;
    }
}