using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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

    private Animator animator;

    private float initialY;
    private float firstLimitY;
    private float secondLimitY;

    private float minX;
    private float maxX;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Camera cam = Camera.main;

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

        Vector3 movement = new Vector3(inputX * speedX, moveY, 0f);
        transform.Translate(movement * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, initialY, secondLimitY);

        transform.position = new Vector3(clampedX, clampedY, 0f);

        animator.SetBool("isMoving", pressingW);
    }
}