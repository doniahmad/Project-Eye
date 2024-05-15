using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : BaseItem
{
    public static WhiteBoard Instance { get; set; }

    public TaskManager taskManager;
    private ListTaskSOs listTaskSOs;
    private ListTaskSOs currentTaskSOs;

    private void Start()
    {
        Instance = this;
    }

    public override void Interact(PlayerController player)
    {
        if (player.GetPlayerStatus() == PlayerStatus.Status.AfterReadBook)
        {
            switch (PhaseManager.Instance.phase)
            {
                case PhaseManager.Phase.Tutorial:
                    break;
                case PhaseManager.Phase.PhaseHypermetropia:
                    if (currentTaskSOs != listTaskSOs)
                    {
                        taskManager.SetListTaskSO(listTaskSOs.listTaskSO);
                        currentTaskSOs = listTaskSOs;
                    }
                    break;
                case PhaseManager.Phase.PhaseCataract:
                    break;
                case PhaseManager.Phase.PhaseMonochromacy:
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
