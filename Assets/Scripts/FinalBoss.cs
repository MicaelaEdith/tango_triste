using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField]
    private float enterSpeed = 2f;
    
    [SerializeField]
    private float targetY = 2f;

    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 35f;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform[] shooters;
    [SerializeField]
    private float minShootInterval = 0.05f;
    [SerializeField]
    private float maxShootInterval = 0.5f;

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private Sprite spriteDamage1;
    [SerializeField]
    private Sprite spriteDamage2;
    [SerializeField]
    private Sprite spriteDamage3; 
    [SerializeField] private
    ParticleSystem hitParticles;

    private SpriteRenderer sr;

    private int currentHealth;
    
    private bool isInPosition = false;

    private float shootTimer;
    private float currentShootInterval;

    private float leftLimit;
    private float rightLimit;
    private float direction = 1f;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        Camera cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        float center = cam.transform.position.x;
        float range = width * 0.6f;
        leftLimit = center - range / 2f;
        rightLimit = center + range / 2f;

        SetRandomShootInterval();
    }

    void Update()
    {
        if (GameManager.Level == 6){
            if (isDead) return;

            if (isInPosition)
            {
                HandleMovement();
                HandleShooting();
            }else
            {
                HandleEntry();
            }
        }
    }

    void HandleEntry()
    {
        if (isInPosition) return;

        transform.position += Vector3.down * enterSpeed * Time.deltaTime;

        if (transform.position.y <= targetY)
        {
            transform.position = new Vector3(transform.position.x, targetY, 0f);
            isInPosition = true;
        }
    }
    
    void HandleMovement()
    {
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        if (transform.position.x <= leftLimit)
            direction = 1f;
        else if (transform.position.x >= rightLimit)
            direction = -1f;

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }


    void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= currentShootInterval)
        {
            shootTimer = 0f;

            ShootPattern();

            SetRandomShootInterval();
        }
    }

    void SetRandomShootInterval()
    {
        currentShootInterval = Random.Range(minShootInterval, maxShootInterval);
    }


    void ShootPattern()
    {
        int pattern = Random.Range(0, 2);

        switch (pattern)
        {
            case 0:
                ShootAll();
                break;

            case 1:
                ShootAlternate();
                break;
        }
    }

    void ShootAll()
    {
        foreach (Transform shooter in shooters)
        {
            Instantiate(bulletPrefab, shooter.position, shooter.rotation);
        }
    }

    void ShootAlternate()
    {
        int index = Random.Range(0, shooters.Length);
        Instantiate(bulletPrefab, shooters[index].position, shooters[index].rotation);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateSpriteByHealth();

        if (hitParticles != null)
        {
            hitParticles.Play();

            if(currentHealth >= 51)
            {
                sr.color = Color.gray;

            }
            if(currentHealth <= 50)
            {
                sr.color = Color.orange;

            }
            if(currentHealth <= 25)
            {
                sr.color = Color.red;

            }
            Invoke(nameof(ResetColor), 0.1f);
        }


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ResetColor()
    {
        sr.color = Color.white;
    }

    void UpdateSpriteByHealth()
    {
        if (currentHealth <= 25)
        {
            sr.sprite = spriteDamage3;
        }
        else if (currentHealth <= 50)
        {
            sr.sprite = spriteDamage2;
        }
        else if (currentHealth <= 75)
        {
            sr.sprite = spriteDamage1;
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("final");
        GameManager.you_win = true;

        gameObject.SetActive(false);
    }
}