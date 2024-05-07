using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUIOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiToClose;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Close the UI when the background panel is clicked
        uiToClose.SetActive(false);
    }
}
