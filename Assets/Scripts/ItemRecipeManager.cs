using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRecipeManager : MonoBehaviour
{
    [SerializeField] private CraftingUI craftingUI;
    [SerializeField] private ListItemRecipeSO listItemRecipeSO;
    [SerializeField] private ItemUI displayCraftedItem;
    [SerializeField] private CraftingMinigame craftingMinigame;
    private List<ItemObjectSO> itemInCraftingSlot;

    private void Start()
    {
        craftingUI.OnCraftingSlotChanged += CraftingUI_OnCraftingSlotChanged;
    }

    private void CraftingUI_OnCraftingSlotChanged(object sender, CraftingUI.ListStoredItemEventArgs e)
    {
        itemInCraftingSlot = e.listItemInCraftingSlot;

        foreach (ItemRecipeSO recipe in listItemRecipeSO.listItemRecipeSO)
        {
            bool allItemsFound = true;

            foreach (ItemObjectSO neededItem in recipe.listItems)
            {
                bool foundItem = false;
                foreach (ItemObjectSO itemInSlot in itemInCraftingSlot)
                {
                    if (neededItem == itemInSlot)
                    {
                        foundItem = true;
                        break;
                    }
                }
                if (!foundItem)
                {
                    allItemsFound = false;
                    break;
                }
            }

            if (allItemsFound)
            {
                displayCraftedItem.UpdateItem(recipe.outputItem);
                craftingMinigame.StartCraftingMinigame();
                break;
            }
        }

    }
}
