using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseItem
{
    public static event EventHandler OnActionDoor;
    public ItemObjectSO key;
    public Transform targetPos;
    public bool doorOpened = true;

    private void Start()
    {
        if (key != null)
        {
            InteractCommand = "Use Key";
            doorOpened = false;
        }
    }

    public override void Interact(PlayerController player)
    {
        if (key == null)
        {
            MovePlayer(player);
            OnActionDoor?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            if (player.GetPlayerInventory().GetSelectedInventoryItem() == key)
            {
                player.GetPlayerInventory().RemoveItem(player.GetPlayerInventory().GetSelectedInventoryItem());
                key = null;
                doorOpened = true;
                InteractCommand = "Masuk Gudang";
                Debug.Log("Pintu terbuka");
            }
            else
            {
                Debug.Log("Key salah");
            }
        }
    }

    private void MovePlayer(PlayerController player)
    {
        player.GetPlayerMotor().ChangePlayerPosition(targetPos);
    }

}
