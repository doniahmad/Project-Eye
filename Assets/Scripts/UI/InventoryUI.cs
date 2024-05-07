using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<ItemUI> itemUI = new List<ItemUI>();
    public PlayerInventory playerInventory;
    public Sprite onSelectInventory;

    private void Awake()
    {
        foreach (ItemUI item in itemUI)
        {
            Button slotButton = item.gameObject.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(() =>
                {
                    playerInventory.SetSelectedInventoryItem(item.itemObjectSO);
                });
            }
            else
            {
                Debug.LogWarning("Button component not found for item: " + item.itemObjectSO);
            }
        }
    }

    public void AddItem(ItemObjectSO itemObjectSO)
    {
        ItemUI emptySlot = itemUI.Find(i => i.itemObjectSO == null);
        if (emptySlot != null)
        {
            emptySlot.UpdateItem(itemObjectSO);
        }
    }

    public void RemoveItem(ItemObjectSO itemObjectSO)
    {
        ItemUI slotToRemove = itemUI.Find(i => i.itemObjectSO == itemObjectSO);
        if (slotToRemove != null)
        {
            slotToRemove.ClearItem();
        }
    }

}
