using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDeviceItem : BaseItem
{
    [SerializeField] private CraftingUI craftingUI;
    public List<MeshRenderer> slotMeshRenderer;
    public MeshRenderer craftedMeshRenderer;

    public override void Interact(PlayerController player)
    {
        craftingUI.Show();
        craftingUI.SetPlayerControl(player);
        player.DisableControl();
    }
}
