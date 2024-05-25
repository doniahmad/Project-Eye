using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : BaseItem
{
    [SerializeField] private ItemObjectSO itemObjectSO;

    public ItemObjectSO GetItemObjectSO()
    {
        return itemObjectSO;
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() != PlayerStatus.Status.Dirty && player.GetPlayerStatus() != PlayerStatus.Status.DirtyGloved)
        {
            PlayerInventory playerInventory = player.GetPlayerInventory();

            if (playerInventory.TryStoreItem(GetItemObjectSO()))
            {
                Debug.Log("Success Get " + itemObjectSO.objectName);
            };
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
        return itemObjectSO.prefab.GetComponent<Item>();
    }
}
