using UnityEngine;

public class Level6 : MonoBehaviour
{
    [SerializeField]
    private GameObject bossPrefab;
    

    void Start()
    {

        Instantiate(bossPrefab, new Vector3(0f, 7f, 0f), Quaternion.identity);
     

    }


}