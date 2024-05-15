using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private event EventHandler OnStateChanged;

    public enum State
    {
        WaitingToStart,
        ApplyingPhase,
        GamePlaying,
        OnPause,
        GameOver,
    }

    public static GameManager Instance { get; set; }

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
        state = State.ApplyingPhase;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
        phaseManager.OnPhaseChanged += PhaseManager_OnPhaseChanged;
    }

    private void PhaseManager_OnPhaseChanged(object sender, EventArgs e)
    {
        ChangeState(State.ApplyingPhase);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        switch (state)
        {
            case State.ApplyingPhase:
                InputManager.Instance.OnDisable();
                ApplyPhase();
                break;
            case State.GamePlaying:
                InputManager.Instance.OnEnable();
                break;
            case State.OnPause:
                InputManager.Instance.OnDisable();
                break;
            case State.GameOver:
                InputManager.Instance.OnDisable();
                break;
            case State.WaitingToStart:
                break;
        }
    }

    public void ApplyPhase()
    {
        switch (phaseManager.phase)
        {
            case PhaseManager.Phase.Tutorial:
                // CutsceneManager.Instance.StartCutScene();
                player.SetPlayerStatus(PlayerStatus.Status.Clean);
                taskManager.SetListTaskSO(phaseManager.tutorialTask.listTaskSO);
                StartGameplay();
                break;
            case PhaseManager.Phase.PhaseHypermetropia:
                CutsceneManager.Instance.StartCutScene();
                player.SetPlayerStatus(PlayerStatus.Status.DirtyGloved);
                WhiteBoard.Instance.SetListTaskSO(phaseManager.hypermetropiaTask);
                StartGameplay();
                break;
        }
    }

    public void ChangeState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartGameplay()
    {
        state = State.GamePlaying;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
