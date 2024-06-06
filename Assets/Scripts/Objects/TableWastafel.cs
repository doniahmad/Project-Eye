using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableWastafel : BaseItem
{
    public static event EventHandler OnOpenWashtafel;
    public GameObject vfxWaterfall;

    public static void ResetStaticData()
    {
        OnOpenWashtafel = null;
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() == PlayerStatus.Status.DirtyGloved)
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku harus membuang sarung tanganku terlebih dahulu"}
            }
            });
        }
        else if (player.GetPlayerStatus() != PlayerStatus.Status.Clean && player.GetPlayerStatus() != PlayerStatus.Status.CleanGloved)
        {
            StartCoroutine(StartWaterfall());
            OnOpenWashtafel?.Invoke(this, EventArgs.Empty);
            player.SetPlayerStatus(PlayerStatus.Status.Clean);
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

    IEnumerator StartWaterfall()
    {
        vfxWaterfall.SetActive(true);
        yield return new WaitForSeconds(3f);
        vfxWaterfall.SetActive(false);
    }
}
