using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : BaseItem
{
    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() == PlayerStatus.Status.AfterReadBook)
        {
            player.SetPlayerStatus(PlayerStatus.Status.AfterWritingRecipe);
        }
        else
        {
            Debug.Log("Not Able to Use White Board");
        }
    }
}
