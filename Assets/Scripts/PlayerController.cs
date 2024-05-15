using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private InputManager playerInput;
    private PlayerInteract playerInteract;
    private PlayerInventory playerInventory;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    private PlayerAnimation playerAnimation;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        Instance = this;

        playerInput = GetComponent<InputManager>();
        playerInteract = GetComponent<PlayerInteract>();
        playerInventory = GetComponent<PlayerInventory>();
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    public PlayerInteract GetPlayerInteract()
    {
        return playerInteract;
    }

    public PlayerInventory GetPlayerInventory()
    {
        return playerInventory;
    }

    public PlayerLook GetPlayerLook()
    {
        return playerLook;
    }

    public PlayerMotor GetPlayerMotor()
    {
        return playerMotor;
    }

    public PlayerAnimation GetPlayerAnimation()
    {
        return playerAnimation;
    }

    public void DisableControl()
    {
        playerInput.OnDisable();
    }

    public void EnableControl()
    {
        playerInput.OnEnable();
    }

    public PlayerStatus.Status GetPlayerStatus()
    {
        return playerStatus.status;
    }

    public void SetPlayerStatus(PlayerStatus.Status status)
    {
        playerStatus.status = status;
    }

    public void StartGameplay()
    {
        GameManager.Instance.StartGameplay();
    }

    public void DisableInteraction()
    {
        playerInteract.enabled = false;
        playerInteract.SetSelectedObject(null);
    }

    public void EnableInteraction()
    {
        playerInteract.enabled = true;
    }

    public void SetHypermetropia()
    {
        VisualEffectManager.Instance.ApplyHypermetropia();
    }

    public void SetCataract()
    {
        VisualEffectManager.Instance.ApplyCataract();
    }

    public void SetMonochromacy()
    {
        VisualEffectManager.Instance.ApplyMonochromacy();
    }
}
