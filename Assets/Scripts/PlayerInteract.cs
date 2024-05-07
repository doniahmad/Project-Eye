using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float rayDistance = 2;
    public LayerMask interactableLayer;
    private BaseItem selectedObject;
    private PlayerInventory playerInventory;
    private Item showedItem;
    private Vector3 currentHitPoint;
    private bool onPlaceableArea;

    private void Start()
    {
        playerInventory = PlayerController.Instance.GetPlayerInventory();
        InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
    }

    private void InputManager_OnInteractAction(object sender, EventArgs e)
    {
        OnInteractAction();
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void OnInteractAction()
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
                            SetSelectedObject(null);
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
                    ShowItemPlacement(hit.point);

                }
                else
                {
                    HideItemPlacement();
                }
            }
            else
            {
                SetSelectedObject(null);
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
            showedItem = Instantiate(selectedItem.onPlacementPrefab, pos, selectedItem.onPlacementPrefab.rotation).GetComponent<Item>();
        }
        else if (showedItem.GetItemObjectSO() != selectedItem)
        {
            HideItemPlacement();
            showedItem = Instantiate(selectedItem.onPlacementPrefab, pos, selectedItem.onPlacementPrefab.rotation).GetComponent<Item>();
        }
        else
        {
            showedItem.transform.position = Vector3.Lerp(showedItem.transform.position, pos, 20 * Time.deltaTime);
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
        HideItemPlacement();
    }

    public BaseItem GetSelectedObject()
    {
        return selectedObject;
    }

    public Vector3 GetPlacementPosition()
    {
        return showedItem.transform.position;
    }

}
