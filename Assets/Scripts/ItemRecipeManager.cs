using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRecipeManager : MonoBehaviour
{
    public event EventHandler OnRecipeFound;
    public event EventHandler OnRecipeNotFound;
    public static ItemRecipeManager Instance { get; private set; }

    [SerializeField] private CraftingUI craftingUI;
    [SerializeField] private ListItemRecipeSO listItemRecipeSO;
    [SerializeField] private ItemUI displayCraftedItem;
    [SerializeField] private CraftingMinigame craftingMinigame;
    private List<ItemObjectSO> itemInCraftingSlot;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        craftingUI.OnCraftingSlotChanged += CraftingUI_OnCraftingSlotChanged;
    }

    private void CraftingUI_OnCraftingSlotChanged(object sender, CraftingUI.ListStoredItemEventArgs e)
    {
        itemInCraftingSlot = e.listItemInCraftingSlot;
        if (itemInCraftingSlot.Count == 3)
        {
            bool foundRecipe = false;
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
                    craftingUI.craftingStatus = CraftingUI.CraftingStatus.Crafting;
                    foundRecipe = true;
                    OnRecipeFound?.Invoke(this, EventArgs.Empty);
                    break;
                }
            }
            if (foundRecipe == false)
            {
                OnRecipeNotFound?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
