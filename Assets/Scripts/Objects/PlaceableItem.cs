using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItem : BaseItem
{
    public static event EventHandler<DropItem> OnActionDropItem;
    public class DropItem
    {
        public Vector3 placementPosition;
    }

    public static void ResetStaticData()
    {
        OnActionDropItem = null;
    }

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
            Instantiate(itemObjectSO.prefab, placementPosition, itemObjectSO.onPlacementPrefab.rotation);
            //Instantiate(itemObjectSO.prefab, placementPosition, itemObjectSO.prefab.rotation);
            playerInventory.RemoveItem(itemObjectSO);
            player.GetPlayerInteract().SetSelectedObject(null);
            OnActionDropItem?.Invoke(this, new DropItem { placementPosition = this.placementPosition });
        }
    }
}
