using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICK");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("HOVER");
    }
}