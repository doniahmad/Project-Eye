using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterarctUI : MonoBehaviour
{
    public GameObject container;
    public PlayerInteract playerInteract;
    public TextMeshProUGUI interactText;
    private BaseObject selectedGameObject;

    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        selectedGameObject = playerInteract.GetSelectedObject();
        Debug.Log(selectedGameObject);
        if (selectedGameObject != null)
        {
            interactText.text = selectedGameObject.name;
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
