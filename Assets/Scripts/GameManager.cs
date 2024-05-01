using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private event EventHandler OnStateChanged;

    private enum State
    {
        WaitingToStart,
        GamePlaying,
        GameOver,
    }

    public static GameManager Instance { get; private set; }

    [Header("Manager")]
    public PlayerController player;
    public TaskManager taskManager;

    private State state;
    private PhaseManager phaseManager;
    private float time = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        phaseManager = PhaseManager.Instance;
        OnStateChanged += GameManager_OnStateChanged;
        state = State.GamePlaying;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        ApplyPhase();
    }

    private void ApplyPhase()
    {
        switch (phaseManager.phase)
        {
            case PhaseManager.Phase.Tutorial:
                player.SetPlayerStatus(PlayerStatus.Status.Clean);
                taskManager.SetListTaskSO(phaseManager.tutorialTask.listTaskSO);
                break;
        }
    }
}
