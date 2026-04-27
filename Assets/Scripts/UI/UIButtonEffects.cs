using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UIButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private TextMeshProUGUI btnText;

    [SerializeField]
    private string btnName;

    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color hoverColor = Color.yellow;
    [SerializeField]
    private Color pressedColor = Color.gray;


    public void OnPointerEnter(PointerEventData eventData)
    {
        btnText.color = hoverColor;
    }

public void OnPointerExit(PointerEventData eventData)
{
    if (btnName == "music")
    {
        if (AudioManager.isMusicMuted) btnText.color = pressedColor;
        else btnText.color = normalColor;
    }
    else if (btnName == "sfx")
    {
        if (AudioManager.isSfxMuted) btnText.color = pressedColor;
        else btnText.color = normalColor;
    }
    else
    {
        btnText.color = normalColor;
    }
}

    public void OnPointerDown(PointerEventData eventData)
    {
        btnText.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (btnName == "")
        btnText.color = normalColor;
    }

}