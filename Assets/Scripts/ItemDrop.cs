using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private CraftingUI craftingUI;
    private ItemUI itemUI;
    private BautSlot bautSlot;
    private void Start()
    {
        itemUI = GetComponent<ItemUI>();
        bautSlot = GetComponent<BautSlot>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (bautSlot != null)
        {
            Debug.Log("Last Position is on Baut Slot");
        }
        if (itemUI.itemObjectSO == null || bautSlot.itemObjectSO == null)
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
                    else
                    {
                        craftingUI.InsertCraftItem(itemObject.itemObjectSO, itemUI);
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
