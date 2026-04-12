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

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        initialY = transform.position.y;

        float screenHeight = Camera.main.orthographicSize * 2f;

        firstLimitY = initialY + screenHeight / 3f;
        secondLimitY = initialY + (screenHeight * 2f / 3f);
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.aKey.isPressed) moveX = -1;
        if (Keyboard.current.dKey.isPressed) moveX = 1;

        bool pressingW = Keyboard.current.wKey.isPressed;
        bool pressingS = Keyboard.current.sKey.isPressed;

        float currentY = transform.position.y;

        if (pressingW)
        {
            GameManager.SpeedMultiplier = starFastMultiplier;
        }
        else if (pressingS)
        {
            GameManager.SpeedMultiplier = starSlowMultiplier;
        }
        else
        {
            GameManager.SpeedMultiplier = starNormalMultiplier;
        }

        if (pressingW)
        {
            if (currentY < firstLimitY)
                moveY = fastUpSpeed;
            else if (currentY < secondLimitY)
                moveY = slowUpSpeed;
            else
                moveY = 0f;
        }
        else if (pressingS)
        {
            moveY = -downSpeed;
        }
        else
        {
            if (currentY > initialY)
                moveY = -returnSlowSpeed;
            else
                moveY = 0f;
        }

        Vector3 movement = new Vector3(moveX * speedX, moveY, 0f);
        transform.Translate(movement * Time.deltaTime);

        float clampedY = Mathf.Clamp(transform.position.y, initialY, secondLimitY);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);

        animator.SetBool("isMoving", pressingW);
    }
}