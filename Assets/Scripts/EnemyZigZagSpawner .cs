using UnityEngine;
using System.Collections.Generic;

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

    private bool levelCanEnd = false;

    private List<GameObject> lastRowEnemies = new List<GameObject>();

    private float topLimit;

    void Start()
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize * 2f;
        float bottomEdge = cam.transform.position.y - screenHeight / 2f;
        float topEdge = cam.transform.position.y + screenHeight / 2f;

        startY = bottomEdge - spawnOffsetY;
        topLimit = topEdge + 2f;

        float totalWidth = (enemiesPerRow - 1) * spaceBetweenEnemies;
        startX = cam.transform.position.x - totalWidth / 2f;

        currentRow = 0;
        timer = 0f;
        isSpawning = true;
        levelCanEnd = false;

        SpawnRow();
        currentRow++;
    }

    void Update()
    {
        HandleSpawning();
        CheckLastRowExit();
    }

    void HandleSpawning()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (timer >= delayBetweenRows)
        {
            timer = 0f;

            SpawnRow();
            currentRow++;

            if (currentRow >= totalRows)
            {
                isSpawning = false;
            }
        }
    }

    void SpawnRow()
    {
        float rowY = startY;

        bool isLastRow = (currentRow == totalRows - 1);

        if (isLastRow)
        {
            lastRowEnemies.Clear();
        }

        for (int i = 0; i < enemiesPerRow; i++)
        {
            float x = startX + i * spaceBetweenEnemies;
            Vector3 spawnPos = new Vector3(x, rowY, 0f);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            if (isLastRow)
            {
                lastRowEnemies.Add(enemy);
            }
        }
    }

    void CheckLastRowExit()
    {
        if (levelCanEnd) return;
        if (lastRowEnemies.Count == 0) return;

        bool allOut = true;

        foreach (GameObject enemy in lastRowEnemies)
        {
            if (enemy == null) continue;

            if (enemy.transform.position.y < topLimit)
            {
                allOut = false;
                break;
            }
        }

        if (allOut)
        {
            levelCanEnd = true;
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