using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event EventHandler<OnItemStoredEventArgs> OnItemStored;
    public class OnItemStoredEventArgs : EventArgs
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
            OnItemStored?.Invoke(this, new OnItemStoredEventArgs { itemObjectSO = itemObjectSO });
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
            selectedItem.prefab.transform.localPosition = Vector3.zero;
            instantiatedObject = Instantiate(selectedItem.prefab, showItemPosition, showItemPosition.parent.gameObject);
        }
    }

    public ItemObjectSO GetSelectedInventoryItem()
    {
        return selectedItem;
    }

}
