using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;

    // Normal color = #696969
    public Color normalColor = new Color32(105, 105, 105, 255);

    // Hover color = White
    public Color hoverColor = Color.white;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor;
    }
}