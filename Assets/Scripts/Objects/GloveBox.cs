using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveBox : BaseItem
{
    public static event EventHandler OnActionUseGlove;

    private void Start()
    {
        InteractCommand = "Use Glove";
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() == PlayerStatus.Status.Clean)
        {
            player.SetPlayerStatus(PlayerStatus.Status.CleanGloved);
            OnActionUseGlove?.Invoke(this, EventArgs.Empty);
        }
        else if (player.GetPlayerStatus() == PlayerStatus.Status.CleanGloved)
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku sudah menggunakan sarung tangan"}
            }
            });
        }
        else
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku perlu mencuci tangan sebelum memakai sarung tangan"}
            }
            });
        }
    }
}
