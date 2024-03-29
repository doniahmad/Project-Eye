using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    public float rayDistance = 2;
    public LayerMask interactableLayer;

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
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            float distanceToTarget = (hit.transform.position - transform.position).magnitude;
            Debug.Log("distance target : " + distanceToTarget);
            if (hit.transform.TryGetComponent(out BaseObject baseObject) && distanceToTarget < rayDistance)
            {
                if (baseObject != selectedObject)
                {
                    SetSelectedObject(baseObject);
                }
            }
            else
            {
                SetSelectedObject(null);
            }
        }
    }

    private void SetSelectedObject(BaseObject baseObject)
    {
        this.selectedObject = baseObject;
    }

    public BaseObject GetSelectedObject()
    {
        return selectedObject;
    }

}
