using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event EventHandler<ItemEventArgs> OnItemStored;
    public event EventHandler<ItemEventArgs> OnItemRemoved;
    public class ItemEventArgs : EventArgs
    {
        public ItemObjectSO itemObjectSO;
    }

    public InventoryUI inventoryUI;
    public Transform showItemPosition;
    public int maxSlot = 2;
    private List<ItemObjectSO> listChemicalObjectSOs;
    private ItemObjectSO selectedItem;
    private Transform instantiatedObject;

    private void Awake()
    {
        listChemicalObjectSOs = new List<ItemObjectSO>();
    }

    private void Start()
    {
        InputManager.Instance.OnInventory1Action += InputManager_OnInventory1Action;
        InputManager.Instance.OnInventory2Action += InputManager_OnInventory2Action;
    }

    private void InputManager_OnInventory2Action(object sender, EventArgs e)
    {
        SetSelectedInventoryItem(inventoryUI.itemUI[1].itemObjectSO);
    }

    private void InputManager_OnInventory1Action(object sender, EventArgs e)
    {
        SetSelectedInventoryItem(inventoryUI.itemUI[0].itemObjectSO);
    }

    public bool TryStoreItem(ItemObjectSO itemObjectSO)
    {
        if (listChemicalObjectSOs.Count < maxSlot && itemObjectSO != null)
        {
            listChemicalObjectSOs.Add(itemObjectSO);
            inventoryUI.AddItem(itemObjectSO);
            SetSelectedInventoryItem(itemObjectSO);
            OnItemStored?.Invoke(this, new ItemEventArgs { itemObjectSO = itemObjectSO });
            return true;
        }
        return false;
    }

    public List<ItemObjectSO> GetStoredItem()
    {
        return listChemicalObjectSOs;
    }

    public void RemoveItem(ItemObjectSO itemObjectSO)
    {
        listChemicalObjectSOs.Remove(itemObjectSO);
        inventoryUI.RemoveItem(itemObjectSO);
        SetSelectedInventoryItem(null);
        OnItemRemoved?.Invoke(this, new ItemEventArgs { itemObjectSO = itemObjectSO });
    }

    public void SetSelectedInventoryItem(ItemObjectSO itemObjectSO)
    {
        selectedItem = itemObjectSO;
        ShowItem();
    }

    private void ShowItem()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject.gameObject);
        }

        if (selectedItem != null)
        {
            instantiatedObject = Instantiate(selectedItem.prefab, showItemPosition.position, showItemPosition.rotation, showItemPosition);
            instantiatedObject.localScale = Vector3.one;
        }
    }

    public void ClearStorage()
    {
        foreach (ItemObjectSO item in listChemicalObjectSOs)
        {
            inventoryUI.RemoveItem(item);
            SetSelectedInventoryItem(null);
            OnItemRemoved?.Invoke(this, new ItemEventArgs { itemObjectSO = item });
        }

        listChemicalObjectSOs.Clear();

        SetSelectedInventoryItem(null);

        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject.gameObject);
            instantiatedObject = null;
        }
    }

    public ItemObjectSO GetSelectedInventoryItem()
    {
        return selectedItem;
    }

}
