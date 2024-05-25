using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : BaseItem
{
    public static event EventHandler OnActionTakeItem;

    [SerializeField] private ItemObjectSO itemObjectSO;
    public MeshRenderer itemMaterial;

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
        if (player.GetPlayerStatus() != PlayerStatus.Status.Dirty && player.GetPlayerStatus() != PlayerStatus.Status.DirtyGloved)
        {
            PlayerInventory playerInventory = player.GetPlayerInventory();
            if (playerInventory.TryStoreItem(GetItemObjectSO()) == true)
            {
                DestroySelf();
                OnActionTakeItem?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Tanganku harus bersih dan memakai sarung tangan untuk mengambilnya."}
            }
            });
        }
    }

    public Item GetItemObject()
    {
        return this;
    }
}
