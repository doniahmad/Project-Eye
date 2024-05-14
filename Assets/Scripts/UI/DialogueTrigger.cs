using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Trigger()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

}
