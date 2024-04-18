using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterarctUI : MonoBehaviour
{
    public GameObject container;
    public PlayerInteract playerInteract;
    public TextMeshProUGUI interactText;
    private BaseItem selectedGameObject;

    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        selectedGameObject = playerInteract.GetSelectedObject();
        if (selectedGameObject != null)
        {
            interactText.text = "[F] " + selectedGameObject.InteractCommand;
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        container.SetActive(true);
    }

    private void Hide()
    {
        container.SetActive(false);
    }
}
