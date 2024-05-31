using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WhiteBoard : BaseItem
{
    public static WhiteBoard Instance { get; set; }

    public TaskManager taskManager;
    private ListTaskSOs listTaskSOs;
    private ListTaskSOs currentTaskSOs;
    public bool isWrited = false;

    [SerializeField] private GameObject TutorialWhiteboardLine;
    [SerializeField] private GameObject HypermetropiaWhiteboardLine;
    [SerializeField] private GameObject CataractWhiteboardLine;
    [SerializeField] private GameObject MonocromacyWhiteboardLine;
    // [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private CinemachineVirtualCamera boardCam;

    private bool isOnLook;

    private void Start()
    {
        Instance = this;
        InteractCommand = "Lihat";
    }

    public override void Interact(PlayerController player)
    {
        if (!isOnLook)
        {
            isOnLook = true;
            InteractCommand = "Berhenti Melihat";
            boardCam.Priority = 20;
            player.DisableMotion();
        }
        else
        {
            isOnLook = false;
            InteractCommand = "Lihat";
            boardCam.Priority = 0;
            player.EnableMotion();
        }
        if (player.GetPlayerStatus() == PlayerStatus.Status.AfterReadBook || Book.Instance.IsReaded)
        {
            switch (PhaseManager.Instance.phase)
            {
                case PhaseManager.Phase.Tutorial:
                    TutorialWhiteboardLine.SetActive(true);
                    break;
                case PhaseManager.Phase.PhaseHypermetropia:
                    // TutorialWhiteboardLine.gameObject.GetComponentInChildren<GameObject>().SetActive(true);
                    taskManager.SetListTaskSO(listTaskSOs.listTaskSO);
                    currentTaskSOs = listTaskSOs;
                    HypermetropiaWhiteboardLine.SetActive(true);
                    break;
                case PhaseManager.Phase.PhaseCataract:

                    taskManager.SetListTaskSO(listTaskSOs.listTaskSO);
                    currentTaskSOs = listTaskSOs;
                    CataractWhiteboardLine.SetActive(true);

                    break;
                case PhaseManager.Phase.PhaseMonochromacy:

                    taskManager.SetListTaskSO(listTaskSOs.listTaskSO);
                    currentTaskSOs = listTaskSOs;
                    MonocromacyWhiteboardLine.SetActive(true);

                    break;
                case PhaseManager.Phase.PhaseBlind:
                    break;
            }
            if (!isWrited)
            {
                player.SetPlayerStatus(PlayerStatus.Status.AfterWritingRecipe);
                isWrited = true;
            }
        }
        else
        {

        }
    }

    public void SetListTaskSO(ListTaskSOs listTaskSOs)
    {
        Debug.Log("New list on whiteboard");
        this.listTaskSOs = listTaskSOs;
        isWrited = false;
    }
}
