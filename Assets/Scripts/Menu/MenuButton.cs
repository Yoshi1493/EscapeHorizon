using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    Button thisButton;

    void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        thisButton.OnPointerEnter(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        thisButton.OnPointerClick(eventData);
    }
}