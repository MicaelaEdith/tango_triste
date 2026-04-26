using UnityEngine;
using UnityEngine.UI;

public class GarbageBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Update()
    {
        slider.value = GameManager.garbage;
    }
}