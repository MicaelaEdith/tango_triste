using UnityEngine;

public class Level6 : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;

    void Start()
    {
        float spawnY = Camera.main.orthographicSize + 2f;

        Instantiate(bossPrefab, new Vector3(0f, spawnY, 0f), Quaternion.identity);
    }
}