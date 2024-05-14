using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance { get; set; }

    public event EventHandler OnPhaseChanged;

    public enum Phase
    {
        Tutorial,
        PhaseHypermetropia,
        PhaseCataract,
        PhaseMonochromacy,
        PhaseBlind
    }

    public Phase phase;

    [Header("Tasks")]
    public ListTaskSOs tutorialTask;
    public ListTaskSOs hypermetropiaTask;
    public ListTaskSOs cataractTask;
    public ListTaskSOs monochromacyTask;
    public ListTaskSOs blindTask;

    private int currentPhase = 1;

    private void Awake()
    {
        Instance = this;
        // phase = Phase.PhaseHypermetropia;
    }

    public void ChangePhase()
    {
        currentPhase++;
        Debug.Log("Change Phase");
        switch (currentPhase)
        {
            case 1:
                phase = Phase.Tutorial;
                break;
            case 2:
                phase = Phase.PhaseHypermetropia;
                break;
            case 3:
                phase = Phase.PhaseCataract;
                break;
            case 4:
                phase = Phase.PhaseMonochromacy;
                break;
        }
        OnPhaseChanged?.Invoke(this, EventArgs.Empty);
    }

}
