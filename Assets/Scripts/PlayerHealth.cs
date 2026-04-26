using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private TextMeshProUGUI lblHealth;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject lblGameOver;

    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
        lblGameOver.SetActive(false);

        if (explosion != null)
            explosion.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateUI();
        PlayExplosion();

        if (currentHealth == 0)
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
        currentHealth = maxHealth;
        FindAnyObjectByType<GameManager>().GameOver();
        gameObject.SetActive(false);
        lblGameOver.SetActive(true);
    }

    void UpdateUI()
    {
        if (lblHealth != null)
            lblHealth.text = $"Salud - {currentHealth}";
    }
}