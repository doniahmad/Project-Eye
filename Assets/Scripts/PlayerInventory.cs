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

    private void Update()
    {

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

    public ItemObjectSO GetSelectedInventoryItem()
    {
        return selectedItem;
    }

}
