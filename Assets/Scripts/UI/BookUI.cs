using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    public enum Bab
    {
        Normal,
        Hypermetropia,
        Cataract,
        Monocromacy
    }

    public GameObject container;
    public Image bookRenderer;
    public Button nextButton;
    public Button prevButton;
    public Button babNormal;
    public Button babHypermetropiaBook;
    public Button babCataractBook;
    public Button babMonocromacyBook;

    [SerializeField] private Book bookBehaviour;
    [SerializeField] private InteractUI interactUI;
    [SerializeField] private List<BookSO> ListBook;
    private BookSO babBook;
    private int page = 0;
    private int readPage = 1;

    private void Start()
    {
        Hide();

        page = 0;
        LoadNewBab(Bab.Normal);

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);

        babNormal.onClick.AddListener(() => LoadNewBab(Bab.Normal));
        babHypermetropiaBook.onClick.AddListener(() => LoadNewBab(Bab.Hypermetropia));
        babCataractBook.onClick.AddListener(() => LoadNewBab(Bab.Cataract));
        babMonocromacyBook.onClick.AddListener(() => LoadNewBab(Bab.Monocromacy));

        PhaseManager.Instance.OnPhaseChanged += PhaseManager_OnPhaseChanged;
    }

    private void PhaseManager_OnPhaseChanged(object sender, EventArgs e)
    {
        readPage = 1;
        switch (PhaseManager.Instance.phase)
        {
            case PhaseManager.Phase.Tutorial:
                LoadNewBab(Bab.Normal);
                break;
            case PhaseManager.Phase.PhaseHypermetropia:
                LoadNewBab(Bab.Hypermetropia);
                break;
            case PhaseManager.Phase.PhaseCataract:
                LoadNewBab(Bab.Cataract);
                break;
            case PhaseManager.Phase.PhaseMonochromacy:
                LoadNewBab(Bab.Monocromacy);
                break;
        }
    }

    private void LoadNewBab(Bab babName)
    {
        babBook = ListBook.Find(e => e.name == babName.ToString());
        page = 0;
        LoadBook();
        bookBehaviour.IsReaded = false;
    }

    private void LoadBook()
    {
        bookRenderer.sprite = babBook.listBook[page];

        if (page == babBook.listBook.Count - 1)
        {
            nextButton.interactable = false;
        }
        else
        {

            nextButton.interactable = true;
        }

        if (page == 0)
        {
            prevButton.interactable = false;
        }
        else
        {

            prevButton.interactable = true;
        }
    }

    private void NextPage()
    {
        if (page < babBook.listBook.Count - 1)
        {
            page++;
            readPage++;
            LoadBook();
        }
    }

    private void PrevPage()
    {
        if (page > 0)
        {
            page--;
            LoadBook();
        }
    }

    public void Hide()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.EnableControl();
        }
        if (readPage >= 3 && !bookBehaviour.IsReaded)
        {
            bookBehaviour.IsReaded = true;
            if (PlayerController.Instance.GetPlayerStatus() != PlayerStatus.Status.AfterWritingRecipe)
            {
                PlayerController.Instance.SetPlayerStatus(PlayerStatus.Status.AfterReadBook);
                if (PhaseManager.Instance.phase != PhaseManager.Phase.Tutorial)
                {
                    DialogueManager.Instance.StartDialogue(new Dialogue
                    {
                        dialogueLines = new List<DialogueLine>{
                new DialogueLine {line = "Aku harus menulis resep di papan tulis."}
            }
                    });
                }
            }
        }
        UIManager.Instance.ShowOverlay();
        interactUI.onNewDisplay = false;
        container.SetActive(false);
    }

    public void Show()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.DisableControl();
        }
        UIManager.Instance.HideOverlay();
        interactUI.onNewDisplay = true;
        container.SetActive(true);
    }

    public int GetReadedPage()
    {
        return readPage;
    }
}
