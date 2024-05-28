using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpause;

    public enum State
    {
        WaitingToStart,
        ApplyingPhase,
        GamePlaying,
        GameOver,
    }

    public static GameManager Instance { get; set; }


    [Header("Manager")]
    public PlayerController player;
    public TaskManager taskManager;
    public CutsceneManager cutsceneManager;

    private State state;
    private PhaseManager phaseManager;
    private bool isGamePaused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        phaseManager = PhaseManager.Instance;
        OnStateChanged += GameManager_OnStateChanged;
        // state = State.ApplyingPhase;
        // OnStateChanged?.Invoke(this, EventArgs.Empty);
        InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;
        phaseManager.OnPhaseChanged += PhaseManager_OnPhaseChanged;
    }

    private void InputManager_OnPauseAction(object sender, EventArgs e)
    {
        if (IsGamePlaying())
        {
            TogglePauseGame();
        }
    }

    private void PhaseManager_OnPhaseChanged(object sender, EventArgs e)
    {
        ChangeState(State.ApplyingPhase);
        OnStateChanged?.Invoke(this, EventArgs.Empty);
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
                player.SetPlayerStatus(PlayerStatus.Status.Clean);
                taskManager.SetListTaskSO(phaseManager.tutorialTask.listTaskSO);
                StartGameplay();
                break;
            case PhaseManager.Phase.PhaseHypermetropia:
                cutsceneManager.StartCutScene();
                player.SetPlayerStatus(PlayerStatus.Status.DirtyGloved);
                WhiteBoard.Instance.SetListTaskSO(phaseManager.hypermetropiaTask);
                StartGameplay();
                break;
            case PhaseManager.Phase.PhaseCataract:
                cutsceneManager.StartCutScene();
                player.SetPlayerStatus(PlayerStatus.Status.DirtyGloved);
                WhiteBoard.Instance.SetListTaskSO(phaseManager.cataractTask);
                StartGameplay();
                break;
            case PhaseManager.Phase.PhaseMonochromacy:
                cutsceneManager.StartCutScene();
                player.SetPlayerStatus(PlayerStatus.Status.DirtyGloved);
                WhiteBoard.Instance.SetListTaskSO(phaseManager.monochromacyTask);
                StartGameplay();
                Debug.Log("Applying Monocromacy");
                break;
            case PhaseManager.Phase.PhaseBlind:
                cutsceneManager.StartCutScene();
                player.SetPlayerStatus(PlayerStatus.Status.DirtyGloved);
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

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpause?.Invoke(this, EventArgs.Empty);
        }
    }
}
