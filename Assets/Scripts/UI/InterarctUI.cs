using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public GameObject container;
    public PlayerInteract playerInteract;
    public TextMeshProUGUI interactText;
    private BaseItem selectedGameObject;
    public bool onNewDisplay = false;

    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        selectedGameObject = playerInteract.GetSelectedObject();
        if (selectedGameObject != null && !onNewDisplay)
        {
            interactText.text = "[" + InputManager.Instance.GetBindingText(InputManager.Binding.Interact) + "] " + selectedGameObject.InteractCommand;
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
