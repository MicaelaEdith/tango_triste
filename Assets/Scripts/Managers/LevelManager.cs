using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private MeteorSpawner meteorSpawner;
    [SerializeField]
    private EnemyShipSpawner enemyShipSpawner;
    [SerializeField]
    private EnemyZigZagSpawner enemyZigZagSpawner;
    [SerializeField]
    private Level5Spawner level5Spawner;
    
    [SerializeField]
    private SpriteRenderer background;
    [SerializeField]
    private GameObject levelUpUI;
    [SerializeField]
    private TextMeshProUGUI lblLevel;
    [SerializeField]
    private GameObject level4_prefab;

    private int currentLevel;


    private bool isTransitioning = false;
    private float transitionTimer = 0f;
    private float flashInterval = 0.1f;
    private int flashCount = 0;
    private int maxFlashes = 8;
    private int nextLevel;
    private Color originalColor;

    private float level4Timer = 0f;
    private bool level4Checking = false;

    void Start()
    {
        currentLevel = GameManager.Level;
        originalColor = background.color;

        StartLevel(currentLevel);
    }

    void Update()
    {
        currentLevel = GameManager.Level;
        if (isTransitioning)
        {
            HandleTransition();
            return;
        }

        CheckLevelProgress();

        currentLevel = GameManager.Level;

        if (GameManager.Level == 4)
        {
            if (enemyZigZagSpawner.CanEndLevel())
            {
                //StartEnding();
                StartLevel(5);
            }
        }

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
            case 4:
                SetupLevel4();
                break;
            case 5:
                SetupLevel5();
                break;
        }
    }

    void CheckLevelProgress()
    {
        currentLevel = GameManager.Level;
        if (currentLevel == 1 && GameManager.level1_count >= 200)
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
                GameManager.ChadText = "Cuidado! es una zona de naves de guerra abandonadas";
                StartTransition(3);
            }
        }
        if (GameManager.Level == 3){
            if (GameManager.level3_count >= 120){
                GameManager.ChadText =  "All your base are belong to us";
            }
            
            if(GameManager.level3_count >= 140){
            
            StartTransition(4);
            }
        }
    }


    void StartTransition(int level)
    {
        isTransitioning = true;
        transitionTimer = 0f;
        flashCount = 0;
        nextLevel = level;

        AudioManager.Instance.PlaySFX(AudioManager.SFXType.LevelUp);
        levelUpUI.SetActive(true);
        if(GameManager.IsPaused) levelUpUI.SetActive(false);

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
        levelUpUI.SetActive(false);
    }


    void SetupLevel1()
    {
        GameManager.ChadText = "Cuidado, Guapo! Se acercan meteoritos";

        Invoke(nameof(ActivateMeteorSpawner), 5f);
    }

    void SetupLevel2()
    {
        meteorSpawner.gameObject.SetActive(false);
    }

    void SetupLevel3()
    {
        enemyShipSpawner.gameObject.SetActive(true);
    }

    void SetupLevel4()
    {
        GameManager.ChadText = "Nos persigue una energía extraña";
        enemyShipSpawner.gameObject.SetActive(false);
        enemyZigZagSpawner.SetEnemyPrefab(level4_prefab);
        enemyZigZagSpawner.gameObject.SetActive(true);

        level4Timer = 0f;
        level4Checking = true;
    }

    void SetupLevel5()
    {
        GameManager.ChadText = "¡Cuidado! Es otro cúmulo de naves muertas.";
        level5Spawner.gameObject.SetActive(true);
        enemyZigZagSpawner.gameObject.SetActive(false);
    }

    void StartEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
    }

    void ActivateMeteorSpawner()
    {
        GameManager.ChadText = "Intenta dispararles con la tecla espacio";
        meteorSpawner.gameObject.SetActive(true);
    }

}