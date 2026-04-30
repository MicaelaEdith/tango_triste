using UnityEngine;
using System.Collections.Generic;

public class Level5Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject zigzagPrefab;

    private List<EnemyShip> ships = new List<EnemyShip>();

    private float leftX;
    private float rightX;
    private float topY;
    private float bottomY;

    private float zigzagTimer;
    private float zigzagInterval = 20f;

    private int zigzagIndex = 0;
    private int[] zigzagOrder = new int[3];

    private float leftLane;
    private float centerLane;
    private float rightLane;

    public int shipsDestroyed = 0;

    private bool stopSpawning = false;
    private float endTimer = 0f;
    [SerializeField] private float endDelay = 5f;

    public bool IsFinished => stopSpawning && endTimer >= endDelay;

    void Start()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        leftX = cam.transform.position.x - width / 2f;
        rightX = cam.transform.position.x + width / 2f;

        topY = cam.transform.position.y + height / 2f + 2f;
        bottomY = cam.transform.position.y - height / 2f - 2f;

        leftLane = leftX + width * 0.25f;
        centerLane = cam.transform.position.x;
        rightLane = rightX - width * 0.25f;

        SpawnMeteors();
        SpawnShips();

        ShuffleZigZagOrder();
    }

    void Update()
    {
        if (stopSpawning)
        {
            endTimer += Time.deltaTime;
            return;
        }

        HandleZigZagSpawn();

        if (shipsDestroyed >= 60)
        {
            stopSpawning = true;
            GameManager.ChadText = "Quedan pocas... Disparales a todas!";
        }
    }

    void SpawnMeteors()
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log("paso spawner meteorito: "+i);
            GameObject obj = Instantiate(meteorPrefab);

            Meteor m = obj.GetComponent<Meteor>();

            float speed = Random.Range(1.5f, 3.5f);
            float rotation = Random.Range(-150f, 150f);

            m.Init(
                speed,
                rotation,
                bottomY,
                topY,
                topY + 5f,
                leftX,
                rightX
            );

        }
    }

    void SpawnShips()
    {
        for (int i = 0; i < 25; i++)
        {
            SpawnSingleShip();
        }
    }

    void SpawnSingleShip()
    {
        if (stopSpawning) return;

        float x = Random.Range(leftX, rightX);

        float screenHeight = topY - bottomY;
        float spawnRange = screenHeight * 0.7f;
        float offset = 10f;

        float spawnY = Random.Range(
            topY + offset,
            topY + offset - spawnRange
        );

        GameObject obj = Instantiate(enemyShipPrefab, new Vector3(x, spawnY, 0f), Quaternion.identity);

        EnemyShip ship = obj.GetComponent<EnemyShip>();
        ship.Init(bottomY, null);
        ship.SetLevel5Spawner(this);

        ships.Add(ship);
    }

    public void OnShipDestroyed()
    {
        shipsDestroyed++;

        if (!stopSpawning)
        {
            SpawnSingleShip();
        }
    }

    void HandleZigZagSpawn()
    {
        if (stopSpawning) return;

        zigzagTimer += Time.deltaTime;

        if (zigzagTimer >= zigzagInterval)
        {
            zigzagTimer = 0f;

            SpawnZigZag();

            zigzagIndex++;

            if (zigzagIndex >= 3)
            {
                zigzagIndex = 0;
                ShuffleZigZagOrder();
            }
        }
    }

    void SpawnZigZag()
    {
        float x = GetLanePosition(zigzagOrder[zigzagIndex]);

        Vector3 spawnPos = new Vector3(x, bottomY - 2, 0f);

        Instantiate(zigzagPrefab, spawnPos, Quaternion.identity);
    }

    float GetLanePosition(int lane)
    {
        if (lane == 0) return leftLane;
        if (lane == 1) return centerLane;
        return rightLane;
    }

    void ShuffleZigZagOrder()
    {
        zigzagOrder[0] = 0;
        zigzagOrder[1] = 1;
        zigzagOrder[2] = 2;

        for (int i = 0; i < zigzagOrder.Length; i++)
        {
            int rand = Random.Range(0, zigzagOrder.Length);
            int temp = zigzagOrder[i];
            zigzagOrder[i] = zigzagOrder[rand];
            zigzagOrder[rand] = temp;
        }
    }
}