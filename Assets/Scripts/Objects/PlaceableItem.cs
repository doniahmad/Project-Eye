using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItem : BaseItem
{
    private ItemObjectSO itemObjectSO;
    private Vector3 placementPosition;
    private PlayerInventory playerInventory;

    public override void Interact(PlayerController player)
    {
        playerInventory = player.GetPlayerInventory();
        itemObjectSO = playerInventory.GetSelectedInventoryItem();
        placementPosition = player.GetPlayerInteract().GetPlacementPosition();

        if (itemObjectSO != null)
        {
            Instantiate(itemObjectSO.prefab, placementPosition, itemObjectSO.prefab.rotation);
            playerInventory.RemoveItem(itemObjectSO);
            player.GetPlayerInteract().SetSelectedObject(null);
        }
    }
}
