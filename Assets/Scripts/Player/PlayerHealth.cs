using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject lblGameOver;
    [SerializeField]
    private Slider healthBar;

    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth - 24;

        healthBar.minValue = 0;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    void Start()
    {
        currentHealth = maxHealth - 24;
        UpdateUI();
        lblGameOver.SetActive(false);

        if (explosion != null)
            explosion.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        UpdateUI();
        PlayExplosion();

        if (currentHealth <= 0)
        {
            Die();
            GameManager.gameOver = true;
        }
    }

    void PlayExplosion()
    {
        if (explosion == null) return;

        explosion.SetActive(true);
        Invoke(nameof(HideExplosion), 0.5f);
    }

    void HideExplosion()
    {
        if (explosion != null)
            explosion.SetActive(false);
    }

    void Die()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.GameOver);
        currentHealth = 80;
        FindAnyObjectByType<GameManager>().GameOver();
        gameObject.SetActive(false);
        lblGameOver.SetActive(true);

    }

    public void UpdateUI()
    {
        healthBar.value = currentHealth;
    }

    public void reset_UI()
    {
        lblGameOver.SetActive(false);
    }
}