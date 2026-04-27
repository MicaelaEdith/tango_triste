using UnityEngine;

public class SpaceGarbage : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 1f;
    [SerializeField]
    private float horizontalSpeed = 0.5f;

    private float bottomLimit;
    private float direction;
    private bool first = true;

    public void Init(float bottomLimit)
    {
        this.bottomLimit = bottomLimit;

        direction = Random.value < 0.5f ? -1f : 1f;

        SetInitialPosition();
    }

    void SetInitialPosition()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        float left = cam.transform.position.x - width / 2f;
        float right = cam.transform.position.x + width / 2f;

        float minX = Mathf.Lerp(left, right, 0.25f);
        float maxX = Mathf.Lerp(left, right, 0.75f);

        float x = Random.Range(minX, maxX);

        float top = cam.transform.position.y + height / 2f;
        float y = top + 1f;

        transform.position = new Vector3(x, y, 0f);
    }

    void Update()
    {
        if (first && GameManager.Level != 1)
        {
            GameManager.ChadText = "Presiona 'E' para recolectar la chatarra";
            first = false;
        }

        float moveX = direction * horizontalSpeed;
        float moveY = -fallSpeed;

        transform.position += new Vector3(moveX, moveY, 0f) * Time.deltaTime;

        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collector"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.GarbagePickup);
            GameManager.garbage++;

            Destroy(gameObject);
        }
    }
}