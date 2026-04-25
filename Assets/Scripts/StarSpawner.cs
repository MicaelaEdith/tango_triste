using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject starPrefab;
    [SerializeField]
    private int starsPerLayer = 50;
    [SerializeField]
    private float screenWidth = 10f;
    [SerializeField]
    private float bottomLimit = -6f;

    private Color color_1 = new Color(1f, 1f, 1f, 1f);
    private Color color_2 = new Color(1f, 0.95f, 0.85f, 0.40f);
    private Color color_3 = new Color(1f, 0.9f, 0.7f, 0.08f);

    void Start()
    {
        SpawnLayer(1f, color_1, 50f);
        SpawnLayer(0.5f, color_2, 50f);
        SpawnLayer(0.05f, color_3, 50f);
    }

    void SpawnLayer(float speed, Color color, float spawnHeight)
    {
        for (int i = 0; i < starsPerLayer; i++)
        {
            float x = Random.Range(-screenWidth, screenWidth);

            float y = Random.Range(bottomLimit, bottomLimit + spawnHeight);

            GameObject starObj = Instantiate(starPrefab, new Vector3(x, y, 0), Quaternion.identity);

            Star star = starObj.GetComponent<Star>();

            star.Init(speed, bottomLimit, 6f, 10f, color);
        }
    }
}