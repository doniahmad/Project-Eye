using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartDialogEndingAnimation : MonoBehaviour
{
    public GameObject dialogPopup;
    public Animator dialogAnim;
    public TextMeshProUGUI dialogText;

    private void Start()
    {

        if (dialogAnim != null && dialogText != null)
        {
            Debug.Log("Animator and TextMeshPro components are assigned correctly.");
        }
    }

    public void DisplayDialog()
    {
        dialogPopup.SetActive(true);
        StartCoroutine(StartDialogSequence());
    }

    private IEnumerator StartDialogSequence()
    {
        yield return StartCoroutine(StartSentence("Kenapa semuanya hitam", 2f));
        yield return StartCoroutine(StartSentence("Kepalaku pusing", 2f));
        yield return StartCoroutine(StartSentence("Aku yakin mataku sudah kubuka", 2f));
        yield return StartCoroutine(StartSentence("Apakah ini yang dirasakan Yusuf", 2f));
        yield return StartCoroutine(StartSentence("Sepi", 2f));
        yield return StartCoroutine(StartSentence("Sedih", 2f));
        yield return StartCoroutine(StartSentence("Sunyi", 2f));
        yield return StartCoroutine(StartSentence("Bodoh Sekali Aku", 3f));
        yield return StartCoroutine(StartSentence("Dengan angkuh berkata bahwa buta itu keberuntung", 3f));
        yield return StartCoroutine(StartSentence("Tuhan", 4f));
        yield return StartCoroutine(StartSentence("Jika memang ini balasan dari perbuatanku", 2f));
        yield return StartCoroutine(StartSentence("Aku menerimanya dengan ikhlas", 2f));
        yield return StartCoroutine(StartSentence("Sungguh", 3f));
        yield return StartCoroutine(StartSentence("Aku telah mendustakan ciptaanmu", 2f));
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(StartSentence("Aku merasa melihat sebuah cahaya", 2f));
        yield return StartCoroutine(StartSentence("Apakah mataku mulai membaik", 2f));
        // Add more sentences as needed
    }

    IEnumerator StartSentence(string dialogue, float duration)
    {
        dialogText.text = dialogue;
        dialogAnim.Play("DialogueFadeIn");
        Debug.Log("Playing DialogueFadeIn animation.");

        yield return new WaitForSeconds(dialogAnim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(duration);

        dialogAnim.Play("DialogueFadeOut");
        Debug.Log("Playing DialogueFadeOut animation.");

        yield return new WaitForSeconds(dialogAnim.GetCurrentAnimatorStateInfo(0).length + 1);
    }
}
