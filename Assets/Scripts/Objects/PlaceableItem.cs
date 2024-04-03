using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItem : BaseObject
{
    // public event EventHandler OnPlaceItem;
    // public class OnPlaceItemEventArgs : EventArgs
    // {
    //     public ItemObjectSO itemObjectSO;
    // }

    private ItemObjectSO itemObjectSO;
    private Vector3 placementPosition;
    private PlayerInventory playerInventory;

    public override void Interact(PlayerController player)
    {
        playerInventory = player.GetPlayerInventory();
        itemObjectSO = playerInventory.GetSelectedInventoryItem();
        placementPosition = player.GetPlayerInteract().GetPlacementPosition();

        Instantiate(itemObjectSO.prefab, placementPosition, Quaternion.identity);
        playerInventory.RemoveItem(itemObjectSO);
        player.GetPlayerInteract().SetSelectedObject(null);
    }
}
