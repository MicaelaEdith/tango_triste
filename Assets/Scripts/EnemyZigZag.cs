using UnityEngine;

public class EnemyZigZag : MonoBehaviour
{
    private float verticalSpeed = 0.4f;
    private float horizontalAmplitude = 1.2f;
    private float horizontalFrequency = 1f;

    private float rotationSpeed = 30f;

    private int damage = 2;

    private float startX;

    private SpriteRenderer sr;

    private bool isFlashing = false;
    private float flashTimer = 0f;
    private float flashDuration = 0.3f;
    private float flashSpeed = 20f;

    void Start()
    {
        startX = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
        HandleFlash();
    }

    void Movement()
    {
        float moveY = verticalSpeed * Time.deltaTime;

        float offsetX = Mathf.Sin(Time.time * horizontalFrequency) * horizontalAmplitude;
        float newX = startX + offsetX;

        transform.position = new Vector3(newX, transform.position.y + moveY, 0f);

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void HandleFlash()
    {
        if (!isFlashing) return;

        flashTimer += Time.deltaTime;


        float alpha = Mathf.Abs(Mathf.Sin(Time.time * flashSpeed));
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;

        if (flashTimer >= flashDuration)
        {
            isFlashing = false;
            flashTimer = 0f;

            Color resetColor = sr.color;
            resetColor.a = 1f;
            sr.color = resetColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.ElectricHit);

            StartFlash();
        }
    }

    void StartFlash()
    {
        isFlashing = true;
        flashTimer = 0f;
    }
}