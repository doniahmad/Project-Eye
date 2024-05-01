using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance { get; private set; }

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

    private void Awake()
    {
        Instance = this;
        phase = Phase.Tutorial;
    }

    public void ChangePhase(Phase phase)
    {
        this.phase = phase;
        OnPhaseChanged?.Invoke(this, EventArgs.Empty);
    }
}
