using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionObject;
    
    private Animator explosionAnimator;

    void Awake()
    {
        explosionAnimator = explosionObject.GetComponent<Animator>();
        explosionObject.SetActive(false);
    }

    public void PlayExplosion()
    {
        explosionObject.SetActive(true);
        explosionAnimator.Play("Explosion", 0, 0f);

        Invoke(nameof(HideExplosion), 1f);
    }

    void HideExplosion()
    {
        explosionObject.SetActive(false);
    }
}