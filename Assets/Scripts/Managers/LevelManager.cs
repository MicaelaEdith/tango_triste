using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private MeteorSpawner meteorSpawner;
    [SerializeField]
    private EnemyShipSpawner enemyShipSpawner;
    [SerializeField]
    private SpriteRenderer background;
    [SerializeField]
    private GameObject levelUpUI;
    [SerializeField]
    private TextMeshProUGUI lblLevel;

    private int currentLevel;


    private bool isTransitioning = false;
    private float transitionTimer = 0f;
    private float flashInterval = 0.1f;
    private int flashCount = 0;
    private int maxFlashes = 8;
    private int nextLevel;
    private Color originalColor;

    void Start()
    {
        currentLevel = GameManager.Level;
        originalColor = background.color;

        StartLevel(currentLevel);
    }

    void Update()
    {
        if (isTransitioning)
        {
            HandleTransition();
            return;
        }

        CheckLevelProgress();
    }

    public void StartLevel(int level)
    {
        currentLevel = level;
        lblLevel.text = "Nivel - "+currentLevel;

        switch (level)
        {
            case 1:
                SetupLevel1();
                break;
            case 2:
                SetupLevel2();
                break;
            case 3:
                SetupLevel3();
                break;
        }
    }

    void CheckLevelProgress()
    {
        currentLevel = GameManager.Level;
        if (currentLevel == 1 && GameManager.level1_count >= 35)
        {
            GameManager.ChadText = "Hey Guapo! Tenemos que recolectar chatarra para reparar la nave";
            StartTransition(2);
        }

        if (currentLevel == 2)
        {
            if (GameManager.garbage >= 5)
            {
                GameManager.ChadText = "Ya tenemos suficiente. Ahora presiona R para reparar la nave";
            }

            if (GameManager.garbage == 0)
            {
                StartTransition(3);
            }
        }
        if (GameManager.Level == 3){
            Debug.Log("conteo enemigos eliminados: "+ GameManager.level3_count);
            // Ajustar cantidad de naves a eliminar para nivel 4
        }
    }


    void StartTransition(int level)
    {
        isTransitioning = true;
        transitionTimer = 0f;
        flashCount = 0;
        nextLevel = level;

        levelUpUI.SetActive(true);
    }

    void HandleTransition()
    {
        transitionTimer += Time.deltaTime;

        if (transitionTimer >= flashInterval)
        {
            transitionTimer = 0f;

            background.color = (flashCount % 2 == 0) ? Color.white : Color.black;

            flashCount++;

            if (flashCount >= maxFlashes)
            {
                EndTransition();
            }
        }
    }

    void EndTransition()
    {
        isTransitioning = false;

        background.color = originalColor;
        levelUpUI.SetActive(false);

        GameManager.Level = nextLevel;
        currentLevel = nextLevel;

        StartLevel(nextLevel);
        //  seguir desde acá
        levelUpUI.SetActive(false);
    }


    void SetupLevel1()
    {
        meteorSpawner.gameObject.SetActive(true);
    }

    void SetupLevel2()
    {
        meteorSpawner.gameObject.SetActive(false);
    }

    void SetupLevel3()
    {
        enemyShipSpawner.gameObject.SetActive(true);
    }
}