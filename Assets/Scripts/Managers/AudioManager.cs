using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip menuMusic;
    public AudioClip mainMusic;

    public AudioClip[] sfxClips;

    public enum SFXType
    {
        MeteorExplosion,
        GarbagePickup,
        LevelUp,
        EnemyDeath,
        EnemyShoot,
        PlayerShoot,
        Win,
        GameOver,
        ElectricHit
    }

    public static bool isMusicMuted;
    public static bool isSfxMuted;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        isMusicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        isSfxMuted = PlayerPrefs.GetInt("SfxMuted", 0) == 1;

        musicSource.mute = isMusicMuted;
        sfxSource.mute = isSfxMuted;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleMusicByScene(scene.name);
    }

    void HandleMusicByScene(string sceneName)
    {
        AudioClip targetClip = null;

        if (sceneName == "Main")
            targetClip = mainMusic;
        else
            targetClip = menuMusic;

        if (musicSource.clip == targetClip) return;

        musicSource.clip = targetClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        musicSource.mute = isMusicMuted;
        PlayerPrefs.SetInt("MusicMuted", isMusicMuted ? 1 : 0);
    }

    public void ToggleSFX()
    {
        isSfxMuted = !isSfxMuted;
        sfxSource.mute = isSfxMuted;
        PlayerPrefs.SetInt("SfxMuted", isSfxMuted ? 1 : 0);
    }

    public void PlaySFX(SFXType type)
    {
        if (isSfxMuted) return;

        int index = (int)type;

        if (index >= 0 && index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }
}