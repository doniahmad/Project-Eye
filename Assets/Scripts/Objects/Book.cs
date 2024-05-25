using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : BaseItem
{
    public static Book Instance { get; private set; }
    public BookUI bookUI;
    public bool IsReaded;

    private void Start()
    {
        Instance = this;
        IsReaded = false;
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() != PlayerStatus.Status.Dirty && player.GetPlayerStatus() != PlayerStatus.Status.DirtyGloved)
        {
            bookUI.Show();
        }
        else
        {
            DialogueManager.Instance.StartDialogue(new Dialogue
            {
                dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku masih kotor, aku tidak boleh menyentuh buku ini."}
                }
            });
        }
    }


}
