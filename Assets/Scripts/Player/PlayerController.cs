using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject damageSprite1;
    [SerializeField]
    private GameObject damageSprite2;
    [SerializeField]
    private GameObject damageSprite3;
    [SerializeField]
    private GameObject garbageCollector;

    [SerializeField]
    private float speedX = 6f;

    [SerializeField]
    private float fastUpSpeed = 6f;
    [SerializeField]
    private float slowUpSpeed = 4f;
    [SerializeField]
    private float returnSlowSpeed = 3f;
    [SerializeField]
    private float downSpeed = 5f;

    [SerializeField]
    private float starFastMultiplier = 1.8f;
    [SerializeField]
    private float starSlowMultiplier = 0.5f;
    [SerializeField]
    private float starNormalMultiplier = 1f;

    private Vector3 initialPosition;

    private Animator animator;

    private PlayerHealth playerHealth;

    private float initialY;
    private float firstLimitY;
    private float secondLimitY;

    private float minX;
    private float maxX;

    private int sprite1_value = 70;
    private int sprite2_value = 45;
    private int sprite3_value = 25;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        garbageCollector.SetActive(false);

    }

    void Start()
    {
        Camera cam = Camera.main;

        initialPosition = transform.position;
        initialY = transform.position.y;

        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        firstLimitY = initialY + screenHeight / 3f;
        secondLimitY = initialY + (screenHeight * 2f / 3f);

        float leftEdge = cam.transform.position.x - screenWidth / 2f;
        float rightEdge = cam.transform.position.x + screenWidth / 2f;

        float margin = screenWidth / 5f;

        minX = leftEdge + margin;
        maxX = rightEdge - margin;
    }

    void Update()
    {
        float inputX = 0f;
        if (Keyboard.current.aKey.isPressed) inputX = -1;
        else if (Keyboard.current.dKey.isPressed) inputX = 1;

        bool pressingW = Keyboard.current.wKey.isPressed;
        bool pressingS = Keyboard.current.sKey.isPressed;

        float currentY = transform.position.y;

        GameManager.SpeedMultiplier = pressingW ? starFastMultiplier :
                                      pressingS ? starSlowMultiplier :
                                      starNormalMultiplier;

        GameManager.HorizontalDirection = inputX == 0 ? 0 : -inputX;

        float moveY = 0f;

        if (pressingW)
        {
            if (currentY < firstLimitY)
                moveY = fastUpSpeed;
            else if (currentY < secondLimitY)
                moveY = slowUpSpeed;
        }
        else if (pressingS)
        {
            moveY = -downSpeed;
        }
        else if (currentY > initialY)
        {
            moveY = -returnSlowSpeed;
        }

        if (Keyboard.current.eKey.isPressed)
        {
            garbageCollector.SetActive(true);
        }
        else
        {
            garbageCollector.SetActive(false);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (GameManager.garbage >= 5)
            {
                RepairPlayer();
            }
            else
            {
                GameManager.ChadText = "Necesitamos más chatarra para reparar la nave";
            }
        }

        Vector3 movement = new Vector3(inputX * speedX, moveY, 0f);
        transform.Translate(movement * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, initialY, secondLimitY);

        transform.position = new Vector3(clampedX, clampedY, 0f);

        animator.SetBool("isMoving", pressingW);

        if(playerHealth.currentHealth <= sprite1_value) {
            set_sprite();
        }
    }

    public void set_sprite()
    {
        if(playerHealth.currentHealth  <= sprite1_value && playerHealth.currentHealth > sprite2_value)
        {
            damageSprite1.SetActive(true);
            fastUpSpeed = 5f;
            speedX = 5f;
        
        }
        else if(playerHealth.currentHealth  <= sprite2_value && playerHealth.currentHealth  > sprite3_value)
        {
            speedX = 3.5f;
            fastUpSpeed = 3f;
            damageSprite1.SetActive(false);
            damageSprite2.SetActive(true);
        }
        else if(playerHealth.currentHealth  <= sprite3_value)
        {
            speedX = 2f;
            fastUpSpeed = 2f;
            damageSprite2.SetActive(false);
            damageSprite3.SetActive(true);
        }

    }

    public void RepairPlayer()
    {
        fastUpSpeed = 6f;
        speedX = 6f;
        playerHealth.currentHealth = 100;
        damageSprite1.SetActive(false);
        damageSprite2.SetActive(false);
        damageSprite3.SetActive(false);
        GameManager.garbage = 0;
        playerHealth.UpdateUI();
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.LevelUp);
    }

public void ResetPlayer()
    {
        transform.position = initialPosition;

        fastUpSpeed = 6f;
        speedX = 6f;

        playerHealth.currentHealth = 80;
        playerHealth.UpdateUI();

        damageSprite1.SetActive(false);
        damageSprite2.SetActive(false);
        damageSprite3.SetActive(false);

        animator.SetBool("isMoving", false);
    }
}