using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private int damage = 4;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int random = Random.Range(1, 5);

        if (random == 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.EnemyShoot);
        }
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        CheckOffScreen();


        Color c = sr.color;
        c.a = GameManager.IsPaused ? 0f : 1f;
        sr.color = c;

        if (GameManager.IsPaused) return;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void CheckOffScreen()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        float left = cam.transform.position.x - width / 2f;
        float right = cam.transform.position.x + width / 2f;
        float top = cam.transform.position.y + height / 2f;
        float bottom = cam.transform.position.y - height / 2f;

        Vector3 pos = transform.position;

        if (pos.x < left || pos.x > right || pos.y > top || pos.y < bottom)
        {
            Destroy(gameObject);
        }
    }
}