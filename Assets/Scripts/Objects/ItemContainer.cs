using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : BaseItem
{
    [SerializeField] private ItemObjectSO itemObjectSO;

    public ItemObjectSO GetItemObjectSO()
    {
        return itemObjectSO;
    }

    public override void Interact(PlayerController player)
    {
        PlayerInventory playerInventory = player.GetPlayerInventory();

        if (playerInventory.TryStoreItem(GetItemObjectSO()))
        {
        };
    }

    public Item GetItemObject()
    {
        return itemObjectSO.prefab.GetComponent<Item>();
    }
}
