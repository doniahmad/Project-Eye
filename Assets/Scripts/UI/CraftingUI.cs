using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public CraftingDeviceItem craftingDevice;
    public GameObject container;
    public GameObject craftingMinigameUI;
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
        ItemRecipeManager.Instance.OnRecipeFound += ItemRecipeManager_OnItemFound;
        ItemRecipeManager.Instance.OnRecipeNotFound += ItemRecipeManager_OnItemNotFound;

        OnCraftingSlotChanged += CraftingUI_OnCraftingSlotChanged;
    }

    private void CraftingUI_OnCraftingSlotChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < craftingSlot.Count; i++)
        {
            if (listItemInCraftingSlot[i].isiMaterial != null)
            {
                craftingDevice.slotMeshRenderer[i].material = listItemInCraftingSlot[i].isiMaterial;
            }
        }
    }

    private void ItemRecipeManager_OnItemFound(object sender, EventArgs e)
    {
        if (craftedItem.itemObjectSO.isiMaterial != null)
        {
            craftingDevice.craftedMeshRenderer.material = craftedItem.itemObjectSO.isiMaterial;
        }
    }

    private void ItemRecipeManager_OnItemNotFound(object sender, EventArgs e)
    {
        NotificationUI.Instance.TriggerNotification("Ramuan Tidak Ditemukan");
        ClearCraftItem();
        Hide();
        CraftingMinigame.Instance.ResetCraftingMinigame();
        craftingDevice.ResetMaterial();
        // playerController.SetPlayerStatus(PlayerStatus.Status.DirtyGloved);
        craftingStatus = CraftingStatus.Empty;
    }

    private void CraftingMinigame_OnFailedCrafting(object sender, EventArgs e)
    {
        NotificationUI.Instance.TriggerNotification("Ramuan Gagal");
        ClearCraftItem();
        Hide();
        CraftingMinigame.Instance.ResetCraftingMinigame();
        craftingDevice.ResetMaterial();
        // playerController.SetPlayerStatus(PlayerStatus.Status.DirtyGloved);
        craftingStatus = CraftingStatus.Empty;
    }

    private void CraftingMinigame_OnSuccessCrafting(object sender, EventArgs e)
    {
        craftingStatus = CraftingStatus.Crafted;
        if (craftedItem != null)
        {
            if (playerInventory.TryStoreItem(craftedItem.itemObjectSO))
            {
                NotificationUI.Instance.TriggerNotification("Ramuan Berhasil");
                ClearCraftItem();
                Hide();
                CraftingMinigame.Instance.ResetCraftingMinigame();
                craftingDevice.ResetMaterial();
            }
        }
        craftingStatus = CraftingStatus.Crafted;
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

        StartCoroutine(InvokeWithDelay());
    }

    public void RemoveCraftItem(ItemUI itemUI)
    {
        listItemInCraftingSlot.Remove(itemUI.itemObjectSO);
        itemUI.ClearItem();
        craftingSlot.Remove(itemUI);

        StartCoroutine(InvokeWithDelay());
    }

    private IEnumerator InvokeWithDelay()
    {
        yield return new WaitForSeconds(0.2f); // Adjust the delay duration as needed
        OnCraftingSlotChanged?.Invoke(this, new ListStoredItemEventArgs { listItemInCraftingSlot = listItemInCraftingSlot });
    }

    public void ClearCraftItem()
    {
        for (int i = 0; i < craftingSlot.Count; i++)
        {
            craftingSlot[i].ClearItem();
        }

        craftingSlot.Clear();
        listItemInCraftingSlot.Clear();
        craftedItem.ClearItem();
        craftingStatus = CraftingStatus.Empty;
    }

    public void Hide()
    {
        if (playerController != null)
        {
            playerController.EnableControl();
        }
        UIManager.Instance.ShowOverlay();
        interactUI.onNewDisplay = false;
        container.SetActive(false);
    }

    public void Show()
    {
        UIManager.Instance.HideOverlay();
        container.SetActive(true);
        interactUI.onNewDisplay = true;
    }

    // public void HideCraftingUI()
    // {
    //     craftingMinigameUI.SetActive(false);
    // }

    // public void ShowCraftingUI()
    // {
    //     craftingMinigameUI.SetActive(true);
    // }

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
