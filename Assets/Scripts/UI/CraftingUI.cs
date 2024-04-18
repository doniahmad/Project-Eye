using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public event EventHandler<ListStoredItemEventArgs> OnCraftingSlotChanged;
    public class ListStoredItemEventArgs : EventArgs
    {
        public List<ItemObjectSO> listItemInCraftingSlot;
    }

    public GameObject container;
    private PlayerController playerController;
    [Header("Inventory Side")]
    public List<ItemUI> invetorySlot = new List<ItemUI>();
    private List<ItemObjectSO> listStoredItem;
    public PlayerInventory playerInventory;
    [Header("Crafting Side")]
    private List<ItemUI> craftingSlot = new List<ItemUI>();
    private List<ItemObjectSO> listItemInCraftingSlot = new List<ItemObjectSO>();
    [SerializeField] private ItemUI craftedItem;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        listStoredItem = playerInventory.GetStoredItem();

        playerInventory.OnItemStored += PlayerInventory_OnItemStored;
        playerInventory.OnItemRemoved += PlayerInventory_OnItemRemoved;
        CraftingMinigame.OnSuccessCrafting += CraftingMinigame_OnSuccessCrafting;
        CraftingMinigame.OnFailedCrafting += CraftingMinigame_OnFailedCrafting;
    }

    private void CraftingMinigame_OnFailedCrafting(object sender, EventArgs e)
    {
        ClearCraftItem();
        Hide();
        Debug.Log("Failed Crafting");
    }

    private void CraftingMinigame_OnSuccessCrafting(object sender, EventArgs e)
    {
        ClearCraftItem();
        Hide();
        playerInventory.TryStoreItem(craftedItem.itemObjectSO);
        Debug.Log("Success Crafting " + craftedItem.itemObjectSO.objectName);
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

    public void InsertCraftItem(ItemObjectSO itemObjectSO, ItemUI itemUI)
    {
        itemUI.UpdateItem(itemObjectSO);
        listItemInCraftingSlot.Add(itemObjectSO);
        craftingSlot.Add(itemUI);

        OnCraftingSlotChanged?.Invoke(this, new ListStoredItemEventArgs { listItemInCraftingSlot = listItemInCraftingSlot });
    }

    public void RemoveCraftItem(ItemUI itemUI)
    {
        itemUI.ClearItem();
        listItemInCraftingSlot.Remove(itemUI.itemObjectSO);
        craftingSlot.Remove(itemUI);

        OnCraftingSlotChanged?.Invoke(this, new ListStoredItemEventArgs { listItemInCraftingSlot = listItemInCraftingSlot });
    }

    public void ClearCraftItem()
    {
        for (int i = 0; i < craftingSlot.Count; i++)
        {
            craftingSlot[i].ClearItem();
            listItemInCraftingSlot.Remove(craftingSlot[i].itemObjectSO);
            craftingSlot.Remove(craftingSlot[i]);
        }
    }

    public void Hide()
    {
        if (playerController != null)
        {
            playerController.EnableControl();
            UIManager.Instance.ShowOverlay();
        }
        container.SetActive(false);
    }

    public void Show()
    {
        container.SetActive(true);
    }

    public void SetPlayerControl(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
