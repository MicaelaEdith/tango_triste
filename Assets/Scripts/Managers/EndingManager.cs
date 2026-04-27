using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject slide1;
    [SerializeField]
    private GameObject slide2;
    [SerializeField]
    private GameObject slide3;
    [SerializeField]
    private GameObject exitButton;

    private int currentSlide = 1;
    private float timer = 0f;
    private float duration = 3.5f;

    void Start()
    {
        slide1.SetActive(true);
        slide2.SetActive(false);
        slide3.SetActive(false);
        exitButton.SetActive(false);
    }

    void Update()
    {
        if (currentSlide == 3) return;

        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0f;
            currentSlide++;

            if (currentSlide == 2)
            {
                slide1.SetActive(false);
                slide2.SetActive(true);
            }
            else if (currentSlide == 3)
            {
                slide2.SetActive(false);
                slide3.SetActive(true);

                exitButton.SetActive(true);
            }
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}