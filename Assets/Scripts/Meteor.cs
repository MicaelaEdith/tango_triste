using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float speed;
    private float rotationSpeed;

    private float bottomLimit;
    private float spawnMinY;
    private float spawnMaxY;
    private float leftX;
    private float rightX;

    private int level1_count = 0;

    private SpriteRenderer sr;

    private int hits = 0;
    private float pushOffset = 0f;

    private bool isDead = false;

    [SerializeField]
    private float pushForce = 8f;
    [SerializeField]
    private float pushRecoverySpeed = 5f;

    [SerializeField]
    private GameObject destroyEffect;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    public void Init(
        float speed,
        float rotationSpeed,
        float bottomLimit,
        float spawnMinY,
        float spawnMaxY,
        float leftX,
        float rightX)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.bottomLimit = bottomLimit;
        this.spawnMinY = spawnMinY;
        this.spawnMaxY = spawnMaxY;
        this.leftX = leftX;
        this.rightX = rightX;

        Respawn();
    }

    void Update()
    {
        if (GameManager.Level != 1 && GameManager.Level != 3)
        {
            gameObject.SetActive(false);
            return;
        }
        pushOffset = Mathf.MoveTowards(pushOffset, 0f, pushRecoverySpeed * Time.deltaTime);

        float finalY = -speed + pushOffset;

        transform.position += Vector3.up * finalY * Time.deltaTime;

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (transform.position.y < bottomLimit)
        {
            Respawn();
        }

        Color c = sr.color;
        c.a = GameManager.IsPaused ? 0f : 1f;
        sr.color = c;

        if (GameManager.IsPaused) return;

    }

    void Respawn()
    {
        if(GameManager.Level == 1){
            GameManager.level1_count++;
        } 

        float x = Random.Range(leftX, rightX);
        float y = Random.Range(spawnMinY, spawnMaxY);

        transform.position = new Vector3(x, y, 0f);

        rotationSpeed = Random.Range(-150f, 150f);

        hits = 0;
        pushOffset = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(5);

            DestroyMeteor();

            return;
        }

        if (other.CompareTag("Bullet"))
        {
            if (IsVisibleOnScreen())
            {
                HitByBullet();
            }

            Destroy(other.gameObject);
        }
    }

    void HitByBullet()
    {
        hits++;

        if (hits == 1)
        {
            pushOffset = pushForce;
        }
        else if (hits >= 2)
        {
            DestroyMeteor();
        }
    }

    bool IsVisibleOnScreen()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        float left = cam.transform.position.x - width / 2f;
        float right = cam.transform.position.x + width / 2f;
        float top = cam.transform.position.y + height / 2f;
        float bottom = cam.transform.position.y - height / 2f;

        Vector3 pos = transform.position;

        return pos.x > left && pos.x < right && pos.y > bottom && pos.y < top;
    }

    void DestroyMeteor()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.MeteorExplosion);
        if (isDead) return;
        isDead = true;

        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        Invoke(nameof(RespawnSafe), 0.05f);
    }

    void RespawnSafe()
    {
        isDead = false;
        Respawn();
    }
}