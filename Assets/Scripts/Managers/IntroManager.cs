using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class IntroManager : MonoBehaviour
{
    public GameObject[] slides;
    private int currentSlide = 0;

    void Start()
    {
        MostrarSlide(0);
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SiguienteSlide();
        }
    }

    void SiguienteSlide()
    {
        currentSlide++;

        if (currentSlide >= slides.Length)
        {
            SceneManager.LoadScene("Main");
            return;
        }

        MostrarSlide(currentSlide);
    }

    void MostrarSlide(int index)
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == index);
        }
    }
}