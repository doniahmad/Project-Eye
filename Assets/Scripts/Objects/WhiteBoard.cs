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
        if (player.GetPlayerStatus() == PlayerStatus.Status.AfterReadBook)
        {
            switch (PhaseManager.Instance.phase)
            {
                case PhaseManager.Phase.Tutorial:
                    TutorialWhiteboardLine.SetActive(true);
                    break;
                case PhaseManager.Phase.PhaseHypermetropia:
                    if (currentTaskSOs != listTaskSOs)
                    {
                        taskManager.SetListTaskSO(listTaskSOs.listTaskSO);
                        currentTaskSOs = listTaskSOs;
                        HypermetropiaWhiteboardLine.SetActive(true);
                    }
                    break;
                case PhaseManager.Phase.PhaseCataract:
                    if (currentTaskSOs != listTaskSOs)
                    {
                        taskManager.SetListTaskSO(listTaskSOs.listTaskSO);
                        currentTaskSOs = listTaskSOs;
                        CataractWhiteboardLine.SetActive(true);
                    }
                    break;
                case PhaseManager.Phase.PhaseMonochromacy:
                    if (currentTaskSOs != listTaskSOs)
                    {
                        taskManager.SetListTaskSO(listTaskSOs.listTaskSO);
                        currentTaskSOs = listTaskSOs;
                        MonocromacyWhiteboardLine.SetActive(true);
                    }
                    break;
                case PhaseManager.Phase.PhaseBlind:
                    break;
            }
            player.SetPlayerStatus(PlayerStatus.Status.AfterWritingRecipe);
        }
        else
        {
            Debug.Log("Not Able to Use White Board");
        }
    }

    public void SetListTaskSO(ListTaskSOs listTaskSOs)
    {
        this.listTaskSOs = listTaskSOs;
    }
}
