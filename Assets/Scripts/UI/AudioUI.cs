using UnityEngine;

public class AudioUI : MonoBehaviour
{
    public void ToggleMusic()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ToggleSFX();
    }
}