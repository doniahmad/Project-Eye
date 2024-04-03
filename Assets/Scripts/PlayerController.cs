using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private PlayerInteract playerInteract;
    private PlayerInventory playerInventory;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;

    private void Awake()
    {
        Instance = this;

        playerInteract = GetComponent<PlayerInteract>();
        playerInventory = GetComponent<PlayerInventory>();
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
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
}
