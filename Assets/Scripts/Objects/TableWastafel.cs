using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableWastafel : BaseItem
{
    public static event EventHandler OnOpenWashtafel;

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() != PlayerStatus.Status.Clean && player.GetPlayerStatus() != PlayerStatus.Status.CleanGloved)
        {
            OnOpenWashtafel?.Invoke(this, EventArgs.Empty);
            player.SetPlayerStatus(PlayerStatus.Status.Clean);
        }
        else if (player.GetPlayerStatus() == PlayerStatus.Status.DirtyGloved)
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku harus membuang sarung tanganku terlebih dahulu"}
            }
            });
        }
        else
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku tidak perlu menggunakan ini"}
            }
            });
        }
    }
}
