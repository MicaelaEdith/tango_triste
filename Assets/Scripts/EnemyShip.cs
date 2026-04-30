using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 0.2f;
    [SerializeField]
    private float rotationSpeed = 25f;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shooter;
    [SerializeField]
    private float shootInterval = 2f;

    [SerializeField]
    private float deathSpinSpeed = 600f;
    [SerializeField]
    private float shrinkSpeed = 2f;

    private float shootTimer;
    private float bottomLimit;
    private float rotationDirection;

    private SpriteRenderer sr;

    private EnemyShipSpawner spawner;
    private Level5Spawner level5Spawner;

    private bool isDying = false;

    public void Init(float bottomLimit, EnemyShipSpawner spawner)
    {
        this.bottomLimit = bottomLimit;
        this.spawner = spawner;

        rotationDirection = Random.value < 0.5f ? -1f : 1f;
        shootTimer = Random.Range(0f, shootInterval);
        sr = GetComponent<SpriteRenderer>();
    } 
    

    void Update()
    {
        if (isDying)
        {
            HandleDeathAnimation();
            return;
        }

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, rotationSpeed * rotationDirection * Time.deltaTime);

        HandleShooting();

        if (transform.position.y < bottomLimit)
        {
            DestroyShip();
        }

        Color c = sr.color;
        c.a = GameManager.IsPaused ? 0f : 1f;
        sr.color = c;

        if (GameManager.IsPaused) return;
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || shooter == null) return;

        Instantiate(bulletPrefab, shooter.position, shooter.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.EnemyDeath);
            player.TakeDamage(8);
            StartDeath();
            return;
        }

        if (other.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.EnemyDeath);
            StartDeath();
            Destroy(other.gameObject);
        }
    }

    void StartDeath()
    {
        if (isDying) return;

        isDying = true;

        GetComponent<Collider2D>().enabled = false;

    }

    void HandleDeathAnimation()
    {
        transform.Rotate(0f, 0f, deathSpinSpeed * Time.deltaTime);

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);

        if (transform.localScale.x < 0.05f)
        {
            DestroyShip();
        }
    }

    void DestroyShip()
    {
        if (spawner != null)
        {
            spawner.OnShipDestroyed();
        }

        if (level5Spawner != null)
        {
            level5Spawner.OnShipDestroyed();
        }

        Destroy(gameObject);
        GameManager.level3_count++;
    }

    public void SetLevel5Spawner(Level5Spawner spawner)
{
    level5Spawner = spawner;
}
}