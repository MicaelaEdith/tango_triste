using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Ui_panel;
    [SerializeField]
    private GameObject Chad;

    public static float SpeedMultiplier = 1f;

    public static float HorizontalDirection = 0f;
    public static float HorizontalInfluence = 0.5f;

    public static string ChadText = "HOLA GUAPO! Estoy funcionando!";

    public static int Level = 1;

    public static bool IsPaused = false;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;

        if (pausePanel != null){
            
            pausePanel.SetActive(true);
            Player.SetActive(false);
            Ui_panel.SetActive(false);
            Chad.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;

        if (pausePanel != null){

            pausePanel.SetActive(false);
            Player.SetActive(true);
            Ui_panel.SetActive(true);
            Chad.SetActive(true);
        }
    }
}