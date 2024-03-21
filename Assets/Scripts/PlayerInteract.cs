using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    public float rayDistance = 10;
    public LayerMask interactableLayer;
    public TextMeshProUGUI interactText;

    private BaseObject selectedObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There Is More Than One Player Instance.");
        }
        Instance = this;
    }

    private void Update()
    {
        HandleInteraction();
    }

    public void OnInteractAction()
    {
        if (selectedObject != null)
        {
            selectedObject.Interact(this);
        }
    }

    private void HandleInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            if (hit.transform.TryGetComponent(out BaseObject baseObject))
            {
                if (baseObject != selectedObject)
                {
                    SetSelectedObject(baseObject);
                    interactText.gameObject.SetActive(true);
                    interactText.text = baseObject.name;
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);
                SetSelectedObject(null);
            }
        }
    }

    private void SetSelectedObject(BaseObject baseObject)
    {
        this.selectedObject = baseObject;
    }

}
