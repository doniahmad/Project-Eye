using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueContainer;

    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.05f;

    //public Animator animator;


    void Start()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        Show();
        //animator.Play("show");

        lines.Clear();
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // After typing the sentence, display the next dialogue line
        yield return new WaitForSeconds(1f); // Wait for a short delay before displaying the next sentence
        DisplayNextDialogueLine();
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        Hide();
        //animator.Play("hide");
    }

    public void Show()
    {
        dialogueContainer.SetActive(true);
        PlayerController.Instance.DisableInteraction();
    }

    public void Hide()
    {
        dialogueContainer.SetActive(false);
        PlayerController.Instance.EnableInteraction();
    }
}
