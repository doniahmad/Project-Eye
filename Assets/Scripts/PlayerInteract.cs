using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float rayDistance = 2;
    public LayerMask interactableLayer;
    private BaseItem selectedObject;
    private PlayerController playerController;
    private PlayerInventory playerInventory;
    private Transform showedItem;
    private Vector3 currentHitPoint;
    private bool onPlaceableArea;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = playerController.GetPlayerInventory();
    }

    private void Update()
    {
        HandleInteraction();
    }

    public void OnInteractAction()
    {
        if (selectedObject != null)
        {
            selectedObject.Interact(PlayerController.Instance);
        }
    }

    private void HandleInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            // if ray detect BaseObject
            if (hit.transform.TryGetComponent(out BaseItem baseItem))
            {
                // if baseItem is not same with selectedObject
                if (baseItem != selectedObject)
                {
                    // Check if the object tag is PlaceableArea
                    if (baseItem.gameObject.CompareTag("PlaceableArea"))
                    {
                        if (playerInventory.GetSelectedInventoryItem())
                        {
                            onPlaceableArea = true;
                            SetSelectedObject(baseItem);
                        }
                        else
                        {
                            onPlaceableArea = false;
                        }
                    }
                    else
                    {
                        onPlaceableArea = false;
                        SetSelectedObject(baseItem);
                    }
                }

                if (onPlaceableArea)
                {
                    if (hit.point != currentHitPoint)
                    {
                        ShowItemPlacement(hit.point);
                        currentHitPoint = hit.point;
                    }
                }
                else
                {
                    HideItemPlacement();
                }
            }
        }
        else
        {
            HideItemPlacement();
            SetSelectedObject(null);
        }
    }

    private void ShowItemPlacement(Vector3 pos)
    {
        ItemObjectSO selectedItem = playerInventory.GetSelectedInventoryItem();
        if (showedItem == null)
        {
            showedItem = Instantiate(selectedItem.onPlacementPrefab, pos, selectedItem.onPlacementPrefab.rotation);
        }
        else
        {
            showedItem.position = Vector3.Lerp(showedItem.position, pos, 20 * Time.deltaTime);
        }
    }

    private void HideItemPlacement()
    {
        // Sembunyikan objek yang dipilih
        if (showedItem != null)
        {
            Destroy(showedItem.gameObject);
            showedItem = null;
        }
    }

    public void SetSelectedObject(BaseItem baseItem)
    {
        this.selectedObject = baseItem;
    }

    public BaseItem GetSelectedObject()
    {
        return selectedObject;
    }

    public Vector3 GetPlacementPosition()
    {
        return showedItem.position;
    }

}
