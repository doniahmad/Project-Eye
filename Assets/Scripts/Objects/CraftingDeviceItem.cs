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
        if (player.GetPlayerStatus() != PlayerStatus.Status.Dirty && player.GetPlayerStatus() != PlayerStatus.Status.DirtyGloved)
        {
            craftingUI.Show();
            craftingUI.SetPlayerControl(player);
            player.DisableControl();
        }
        else
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Tanganku belum steril, Aku harus menggunakan sarung tangan dan bersih!"}
            }
            });
        }
    }

    public void ResetMaterial()
    {
        Material[] emptyMaterials = new Material[0];
        foreach (MeshRenderer slot in slotMeshRenderer)
        {
            slot.materials = emptyMaterials;
        }
        if (craftedMeshRenderer.materials != null)
        {
            craftedMeshRenderer.materials = emptyMaterials;
        }
    }
}
