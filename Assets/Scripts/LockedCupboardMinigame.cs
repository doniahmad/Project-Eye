using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCupboardMinigame : MonoBehaviour
{
    public List<LockedCupboardDoor> cupboardDoor;
    public Transform cupboardStuff;
    public LockedCupboardUI lockedCupboardUI;
    public bool isOpen = false;

    private void Start()
    {
        lockedCupboardUI.OnCodeSuccess += LockedCupboardUI_OnCodeSuccess;
        foreach (LockedCupboardDoor doorItem in cupboardDoor)
        {
            doorItem.OnCloseDoor += LockedCupboardDoor_OnCloseDoor;
        }
    }

    private void LockedCupboardDoor_OnCloseDoor(object sender, EventArgs e)
    {
        if (isOpen)
        {
            CloseDoor();
        }
    }

    private void LockedCupboardUI_OnCodeSuccess(object sender, EventArgs e)
    {
        if (!isOpen)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        foreach (LockedCupboardDoor door in cupboardDoor)
        {
            isOpen = true;
            door.OpenDoor();
        }
    }

    public void CloseDoor()
    {
        foreach (LockedCupboardDoor door in cupboardDoor)
        {
            isOpen = false;
            door.CloseDoor();
        }
    }
}
