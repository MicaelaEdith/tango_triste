using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.UI; 

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

    public TextMeshProUGUI btn_text; 
    
    public static float SpeedMultiplier = 1f;

    public static float HorizontalDirection = 0f;
    public static float HorizontalInfluence = 0.5f;
    public static bool IsPaused = false;

    public static string ChadText = "";
    public static int garbage = 1;
    public static int Level = 1;
    public static int level1_count = 0;
    public static int level3_count = 0;
    public static bool gameOver = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }

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

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            Player.SetActive(true);
            Ui_panel.SetActive(true);
            Chad.SetActive(true);

            if (gameOver)
            {
                RestartGame();
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        garbage = 0;
        btn_text.text = "Reintentar";
        PauseGame();
    }

    public void RestartGame()
    {
        Level = 1;
        garbage = 0;
        level1_count = 0;
        level3_count = 0;

        SpeedMultiplier = 1f;
        HorizontalDirection = 0f;

        ChadText = "";
        gameOver = false;

        Time.timeScale = 1f;
        IsPaused = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}