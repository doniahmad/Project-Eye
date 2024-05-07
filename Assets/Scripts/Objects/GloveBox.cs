using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveBox : BaseItem
{
    private void Start()
    {
        InteractCommand = "Use Glove";
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() == PlayerStatus.Status.Clean)
        {
            player.SetPlayerStatus(PlayerStatus.Status.CleanGloved);
        }
        else
        {

        }
    }
}
