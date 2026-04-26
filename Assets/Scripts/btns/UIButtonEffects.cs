using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UIButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private TextMeshProUGUI btnText;

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
        btnText.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        btnText.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        btnText.color = normalColor;
    }
}