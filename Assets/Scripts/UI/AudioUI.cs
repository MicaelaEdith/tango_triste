using UnityEngine;

public class AudioUI : MonoBehaviour
{
    public void Toggle(string channel)
    {
        if (AudioManager.Instance == null) return;

        if (channel == "music")
            AudioManager.Instance.ToggleMusic();
        else if (channel == "sfx")
            AudioManager.Instance.ToggleSFX();
    }
}