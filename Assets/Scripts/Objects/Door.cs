using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseItem
{
    public ItemObjectSO key;
    public Transform targetPos;

    private void Start()
    {
        if (key != null)
        {
            InteractCommand = "Use Key";
        }
    }

    public override void Interact(PlayerController player)
    {
        if (key == null)
        {
            MovePlayer(player);
        }
        else
        {
            if (player.GetPlayerInventory().GetSelectedInventoryItem() == key)
            {
                key = null;
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
