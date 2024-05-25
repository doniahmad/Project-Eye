using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashbin : BaseItem
{

    private void Start()
    {
        InteractCommand = "Trash Glove";
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() == PlayerStatus.Status.DirtyGloved)
        {
            player.SetPlayerStatus(PlayerStatus.Status.Dirty);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku seharusnya tidka menyentuh tempat sampah jika tidak perlu."}
            }
            });
        }
    }
}
