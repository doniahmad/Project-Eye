using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public VisualEffectManager visualEffectManager;
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

    public void DisableMotion()
    {
        playerInput.StopMotion();
    }

    public void EnableMotion()
    {
        playerInput.StartMotion();
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
        switch (status)
        {
            case PlayerStatus.Status.DirtyGloved:
                NotificationUI.Instance.TriggerNotification("Sarung Tangan Kotor");
                break;
            case PlayerStatus.Status.Dirty:
                NotificationUI.Instance.TriggerNotification("Tangan Kotor");
                break;
            case PlayerStatus.Status.CleanGloved:
                NotificationUI.Instance.TriggerNotification("Sarung Tangan Terpasang");
                break;
            case PlayerStatus.Status.Clean:
                NotificationUI.Instance.TriggerNotification("Tangan Bersih");
                break;
            case PlayerStatus.Status.AfterReadBook:
                NotificationUI.Instance.TriggerNotification("Selesai Membaca");
                break;
            case PlayerStatus.Status.AfterWritingRecipe:
                NotificationUI.Instance.TriggerNotification("Resep Baru Ditemukan");
                break;
        }

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
        visualEffectManager.ApplyHypermetropia();
    }

    public void SetCataract()
    {
        visualEffectManager.ApplyCataract();
    }

    public void SetMonochromacy()
    {
        visualEffectManager.ApplyMonochromacy();
    }
}
