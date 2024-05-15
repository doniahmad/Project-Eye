using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    //public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}