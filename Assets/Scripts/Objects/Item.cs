using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : BaseItem
{
    [SerializeField] private ItemObjectSO itemObjectSO;

    public ItemObjectSO GetItemObjectSO()
    {
        return itemObjectSO;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() != PlayerStatus.Status.Dirty || player.GetPlayerStatus() != PlayerStatus.Status.DirtyGloved)
        {
            PlayerInventory playerInventory = player.GetPlayerInventory();
            if (playerInventory.TryStoreItem(GetItemObjectSO()) == true)
            {
                DestroySelf();
            }
        }
    }

    public Item GetItemObject()
    {
        return this;
    }
}
