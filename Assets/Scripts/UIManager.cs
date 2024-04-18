using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Transform overlayUI;
    public Transform cameraUI;

    private void Awake()
    {
        Instance = this;
    }

    public void HideOverlay()
    {
        overlayUI.gameObject.SetActive(false);
    }

    public void ShowOverlay()
    {
        overlayUI.gameObject.SetActive(true);
    }

    public void HideCamera()
    {
        cameraUI.gameObject.SetActive(false);
    }

    public void ShowCamera()
    {
        cameraUI.gameObject.SetActive(true);
    }
}
