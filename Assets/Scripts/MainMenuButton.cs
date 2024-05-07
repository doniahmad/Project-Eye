using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI buttonText;
    public Button button;

    public Color defaultTextColor;
    public Color selectedTextColor;


    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonText.color = selectedTextColor;
        button.image.color = new Color(255, 255, 255, 255);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonText.color = defaultTextColor;
        button.image.color = new Color(255, 255, 255, 0);
    }
}