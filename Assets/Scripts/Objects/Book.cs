using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : BaseItem
{
    public static Book Instance { get; private set; }
    public bool IsReaded;

    private void Start()
    {
        Instance = this;
        IsReaded = false;
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() != PlayerStatus.Status.Dirty || player.GetPlayerStatus() != PlayerStatus.Status.DirtyGloved)
        {
            player.SetPlayerStatus(PlayerStatus.Status.AfterReadBook);
        }
        else
        {
            Debug.Log("Not Able to Read Book");
        }
        Debug.Log(player.GetPlayerStatus());
    }
}
