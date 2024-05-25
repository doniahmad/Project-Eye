using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableWastafel : BaseItem
{
    public static event EventHandler OnOpenWashtafel;

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() != PlayerStatus.Status.Clean || player.GetPlayerStatus() != PlayerStatus.Status.CleanGloved)
        {
            OnOpenWashtafel?.Invoke(this, EventArgs.Empty);
            player.SetPlayerStatus(PlayerStatus.Status.Clean);
        }
        else
        {
            Debug.Log("Hand Not Dirty");
        }
    }
}
