using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CraftingItem : BaseItem
{
    [SerializeField] private List<Transform> itemPosition;

    private ItemObjectSO itemObjectSO;
    private PlayerInventory playerInventory;
    private Transform targetSlot;


    public override void Interact(PlayerController player)
    {
        playerInventory = player.GetPlayerInventory();
        itemObjectSO = playerInventory.GetSelectedInventoryItem();
        targetSlot = EmptySlot();
        UIManager.Instance.HideOverlay();

        if (itemObjectSO != null)
        {
            Instantiate(itemObjectSO.prefab, targetSlot.position, Quaternion.identity, targetSlot);
            playerInventory.RemoveItem(itemObjectSO);
            player.GetPlayerInteract().SetSelectedObject(null);
        }

    }

    // Get children that equal with selected object
    private Transform EmptySlot()
    {
        foreach (Transform slot in itemPosition)
        {
            if (slot.childCount == 0)
            {
                return slot;
            }

        }
        return null;
    }
}
