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
    private void Start()
    {
        itemUI = GetComponent<ItemUI>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (itemUI.itemObjectSO == null)
        {

            GameObject droppedItem = eventData.pointerDrag;
            if (droppedItem != null)
            {
                ItemUI itemObject = droppedItem.GetComponent<ItemUI>();
                if (itemObject != null && itemObject.itemObjectSO != null) // Check if itemObject and itemObject.itemObjectSO are not null
                {
                    craftingUI.InsertCraftItem(itemObject.itemObjectSO, itemUI);
                    inventory.RemoveItem(itemObject.itemObjectSO);
                }
            }
        }
        else
        {
            Debug.Log("Slot is not empty: " + gameObject.name);
        }
    }
}
