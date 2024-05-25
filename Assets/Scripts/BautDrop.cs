using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BautDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private BautSlot bautSlot;

    private void Start()
    {
        bautSlot = GetComponent<BautSlot>();
    }
    public void OnDrop(PointerEventData eventData)
    {

        if (
            bautSlot.itemObjectSO == null)
        {
            GameObject droppedItem = eventData.pointerDrag;
            if (droppedItem != null)
            {
                ItemUI itemObject = droppedItem.GetComponent<ItemUI>();
                if (itemObject != null && itemObject.itemObjectSO != null) // Check if itemObject and itemObject.itemObjectSO are not null
                {
                    if (itemObject.itemObjectSO.objectName == "Baut")
                    {
                        bautSlot.InsertBaut(itemObject.itemObjectSO);
                        inventory.RemoveItem(itemObject.itemObjectSO);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Slot is not empty: " + gameObject.name);
        }
    }
}
