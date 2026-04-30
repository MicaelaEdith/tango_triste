using UnityEngine;

public class Level5Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private GameObject zigzagPrefab;

    private int totalShips = 50;
    private int shipsDestroyed = 0;

    private int[] zigzagTriggers = { 10, 40, 75};
    private bool[] zigzagSpawned;

    private int meteorCount = 8;

    private float topSpawnY;
    private float bottomLimit;
    private float leftX;
    private float rightX;

    void Start()
    {
        zigzagSpawned = new bool[zigzagTriggers.Length];

        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        topSpawnY = cam.transform.position.y + height / 2f + 2f;
        bottomLimit = cam.transform.position.y - height / 2f - 2f;

        leftX = cam.transform.position.x - width / 2f;
        rightX = cam.transform.position.x + width / 2f;

        if (GameManager.Level == 5)
        {
            SpawnShips();
            SpawnMeteors();

            // meté esto en el update y manejalo con una bandera cuando llames a level 5 que si lo hacés bien no llegas, bobita ¬¬
        }
    }

    void Update()
    {
        CheckZigZagSpawns();
        CheckLevelComplete();
    }

    void SpawnShips()
    {
        for (int i = 0; i < totalShips; i++)
        {
            float x = Random.Range(leftX, rightX);
            float y = topSpawnY + Random.Range(0f, 30f);

            GameObject ship = Instantiate(enemyShipPrefab, new Vector3(x, y, 0f), Quaternion.identity);

            EnemyShip es = ship.GetComponent<EnemyShip>();

            if (es != null)
            {
                es.Init(bottomLimit, null);
                es.SetLevel5Spawner(this);
            }
        }
    }

    void SpawnMeteors()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            GameObject m = Instantiate(meteorPrefab);

            Meteor meteor = m.GetComponent<Meteor>();
            if (meteor != null)
            {
                meteor.Init(
                    Random.Range(1f, 3f),
                    Random.Range(-100f, 100f),
                    bottomLimit,
                    topSpawnY,
                    topSpawnY + 10f,
                    leftX,
                    rightX
                );
            }
        }
    }

    public void OnShipDestroyed()
    {
        shipsDestroyed++;
    }

    void CheckZigZagSpawns()
    {
        for (int i = 0; i < zigzagTriggers.Length; i++)
        {
            if (!zigzagSpawned[i] && shipsDestroyed >= zigzagTriggers[i])
            {
                zigzagSpawned[i] = true;
                SpawnZigZag();
            }
        }
    }

    void SpawnZigZag()
    {
        float x = Random.Range(leftX, rightX);
        float y = bottomLimit - 2f;

        Instantiate(zigzagPrefab, new Vector3(x, y, 0f), Quaternion.identity);
    }

    void CheckLevelComplete()
    {
        if (shipsDestroyed >= totalShips)
        {
            Debug.Log("Nivel 5 completado");
            GameManager.Level = 6;
        }
    }

}