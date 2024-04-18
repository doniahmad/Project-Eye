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

    private void Awake()
    {
        Instance = this;

        playerInput = GetComponent<InputManager>();
        playerInteract = GetComponent<PlayerInteract>();
        playerInventory = GetComponent<PlayerInventory>();
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        playerAnimation = GetComponent<PlayerAnimation>();
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
}
