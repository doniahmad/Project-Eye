using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumehoodUI : MonoBehaviour
{
    public static FumehoodUI Instance { get; set; }

    public GameObject container;
    public FumeCupboard fumeCupboard;
    [SerializeField] private InteractUI interactUI;
    [Header("Inventory Side")]
    public List<ItemUI> invetorySlot = new List<ItemUI>();
    public PlayerInventory playerInventory;
    private List<ItemObjectSO> listStoredItem;
    [Header("Baut Slot")]
    public List<BautSlot> listBaut;

    private void Start()
    {
        Instance = this;
        Hide();
        playerInventory.OnItemStored += PlayerInventory_OnItemStored;
        playerInventory.OnItemRemoved += PlayerInventory_OnItemRemoved;
    }

    public void CheckBaut()
    {
        // Check all baut
        bool allBautFilled = true;
        Debug.Log("Insert New Baut");
        foreach (BautSlot baut in listBaut)
        {
            if (baut.itemObjectSO == null)
            {
                allBautFilled = false;
                break;
            }
        }

        if (allBautFilled)
        {
            Debug.Log("Fumehood Solved");
            fumeCupboard.isSolved = true;
            NotificationUI.Instance.TriggerNotification("Pintu Diperbaiki");
            fumeCupboard.SetSolved();
            Hide();
        }
    }

    private void PlayerInventory_OnItemRemoved(object sender, PlayerInventory.ItemEventArgs e)
    {
        RemoveItemInventory(e.itemObjectSO);
    }

    private void PlayerInventory_OnItemStored(object sender, PlayerInventory.ItemEventArgs e)
    {
        AddItemInventory(e.itemObjectSO);
    }

    // Show Inventory Item 
    public void ShowInventoryItem()
    {
        for (int i = 0; i < invetorySlot.Count; i++)
        {
            if (listStoredItem[i] == null)
            {
                invetorySlot[i].UpdateItem(null);
            }
            else
            {
                invetorySlot[i].UpdateItem(listStoredItem[i]);
            }
        }
    }

    public void AddItemInventory(ItemObjectSO itemObjectSO)
    {
        ItemUI emptySlot = invetorySlot.Find(i => i.itemObjectSO == null);
        if (emptySlot != null)
        {
            emptySlot.UpdateItem(itemObjectSO);
        }
    }

    public void RemoveItemInventory(ItemObjectSO itemObjectSO)
    {
        ItemUI slotToRemove = invetorySlot.Find(i => i.itemObjectSO == itemObjectSO);
        if (slotToRemove != null)
        {
            slotToRemove.ClearItem();
        }
    }

    public void Hide()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.EnableControl();
        }
        UIManager.Instance.ShowOverlay();
        interactUI.onNewDisplay = false;
        container.SetActive(false);
    }

    public void Show()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.DisableControl();
        }
        UIManager.Instance.HideOverlay();
        container.SetActive(true);
        interactUI.onNewDisplay = true;
    }
}
