using UnityEngine;

public class EnemyZigZagSpawner : MonoBehaviour
{
    private GameObject enemyPrefab;

    private int enemiesPerRow = 3;
    private int totalRows = 5;
    private float spaceBetweenEnemies = 5f;
    private float delayBetweenRows = 19f;
    private float spawnOffsetY = 4f;

    private int currentRow = 0;
    private float timer = 0f;
    private bool isSpawning = false;

    private float startY;
    private float startX;

    private bool finishedSpawning = false;
    private bool levelCanEnd = false;
    private float endTimer = 0f;

    void OnEnable()
    {
        if (GameManager.Level != 4) return;

        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize * 2f;
        float bottomEdge = cam.transform.position.y - screenHeight / 2f;

        startY = bottomEdge - spawnOffsetY;

        float totalWidth = (enemiesPerRow - 1) * spaceBetweenEnemies;
        startX = cam.transform.position.x - totalWidth / 2f;

        currentRow = 0;
        timer = 0f;
        isSpawning = true;

        levelCanEnd = false;
        endTimer = 0f;

        SpawnRow();
        currentRow++;
    }

    void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;

            if (timer >= delayBetweenRows)
            {
                timer = 0f;

                SpawnRow();
                currentRow++;

                if (currentRow >= totalRows)
                {
                    isSpawning = false;
                    finishedSpawning = true;
                }
            }
        }

    
        if (finishedSpawning)
        {
            endTimer += Time.deltaTime;

            if (endTimer >= 44f)
            {
                levelCanEnd = true;
            }
        }
    }

    void SpawnRow()
    {
        float rowY = startY;

        for (int i = 0; i < enemiesPerRow; i++)
        {
            float x = startX + i * spaceBetweenEnemies;
            Vector3 spawnPos = new Vector3(x, rowY, 0f);

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void SetEnemyPrefab(GameObject prefab)
    {
        enemyPrefab = prefab;
    }

    public bool CanEndLevel()
    {
        return levelCanEnd;
    }
}