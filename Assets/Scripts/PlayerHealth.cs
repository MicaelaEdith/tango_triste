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

    private int currentHealth;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();

        if (explosion != null)
            explosion.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateUI();
        PlayExplosion();

        if (currentHealth == 0)
        {
            Die();
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
        isDead = true;
        Debug.Log("Player murió");

        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        if (lblHealth != null)
            lblHealth.text = $"Salud - {currentHealth}";
    }
}