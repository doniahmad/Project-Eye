using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseItem
{
    public static event EventHandler OnActionDoor;
    public ItemObjectSO key;
    public Transform targetPos;
    public bool doorOpened = true;

    private void Start()
    {
        if (key != null)
        {
            InteractCommand = "Gunakan Kunci";
            doorOpened = false;
        }
    }

    public override void Interact(PlayerController player)
    {
        if (key == null)
        {
            if (PhaseManager.Instance.phase != PhaseManager.Phase.Tutorial && PhaseManager.Instance.phase != PhaseManager.Phase.PhaseHypermetropia)
            {
                MovePlayer(player);
                OnActionDoor?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                DialogueManager.Instance.StartDialogue(new Dialogue
                {
                    dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku memiliki tugas yang harus dikerjakan."}
            }
                });
            }
        }
        else
        {
            if (PhaseManager.Instance.phase != PhaseManager.Phase.PhaseCataract && PhaseManager.Instance.phase != PhaseManager.Phase.PhaseHypermetropia && PhaseManager.Instance.phase != PhaseManager.Phase.Tutorial)
            {
                if (player.GetPlayerInventory().GetSelectedInventoryItem() == key)
                {
                    player.GetPlayerInventory().RemoveItem(player.GetPlayerInventory().GetSelectedInventoryItem());
                    key = null;
                    doorOpened = true;
                    InteractCommand = "Masuk Gudang";
                    Debug.Log("Pintu terbuka");
                    NotificationUI.Instance.TriggerNotification("Pintu Terbuka");
                }
                else if (player.GetPlayerInventory().GetSelectedInventoryItem() != null && player.GetPlayerInventory().GetSelectedInventoryItem() != key)
                {
                    DialogueManager.Instance.StartDialogue(new Dialogue
                    {
                        dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Ini bukan kunci yang benar."}
                }
                    });
                }
                else
                {
                    DialogueManager.Instance.StartDialogue(new Dialogue
                    {
                        dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Seingatku aku menyimpan kunci di suatu lemari."}
                }
                    });
                }
            }
            else
            {
                DialogueManager.Instance.StartDialogue(new Dialogue
                {
                    dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku memiliki tugas yang harus dikerjakan."}
            }
                });
            }
        }
    }

    private void MovePlayer(PlayerController player)
    {
        player.GetPlayerMotor().ChangePlayerPosition(targetPos);
    }

}
