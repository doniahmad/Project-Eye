using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public enum CraftingStatus
    {
        Empty,
        Filled,
        Crafting,
        Crafted,
    }

    public event EventHandler<ListStoredItemEventArgs> OnCraftingSlotChanged;
    public class ListStoredItemEventArgs : EventArgs
    {
        public List<ItemObjectSO> listItemInCraftingSlot;
    }

    public CraftingStatus craftingStatus;

    public GameObject container;
    [SerializeField] private InteractUI interactUI;
    private PlayerController playerController;
    [Header("Inventory Side")]
    public List<ItemUI> invetorySlot = new List<ItemUI>();
    private List<ItemObjectSO> listStoredItem;
    public PlayerInventory playerInventory;
    [Header("Crafting Side")]
    private List<ItemUI> craftingSlot = new List<ItemUI>();
    private List<ItemObjectSO> listItemInCraftingSlot = new List<ItemObjectSO>();
    public ItemUI craftedItem;

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
        craftingStatus = CraftingStatus.Empty;
    }

    private void CraftingMinigame_OnSuccessCrafting(object sender, EventArgs e)
    {
        ClearCraftItem();
        Hide();
        if (craftedItem != null && craftedItem.itemObjectSO.prefab != null)
        {
            playerInventory.TryStoreItem(craftedItem.itemObjectSO);
        }
        craftingStatus = CraftingStatus.Crafted;
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
        craftingStatus = CraftingStatus.Filled;

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
        craftingStatus = CraftingStatus.Empty;
    }

    public void Hide()
    {
        if (playerController != null)
        {
            playerController.EnableControl();
        }
        UIManager.Instance.ShowOverlay();
        container.SetActive(false);
        interactUI.onNewDisplay = false;
    }

    public void Show()
    {
        UIManager.Instance.HideOverlay();
        container.SetActive(true);
        interactUI.onNewDisplay = true;
    }

    public void SetPlayerControl(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public List<ItemUI> GetListCraftingSlot()
    {
        return craftingSlot;
    }

    public List<ItemObjectSO> GetListItemInCraftingSlot()
    {
        return listItemInCraftingSlot;
    }
}
