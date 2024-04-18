using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemRecipeSO : ScriptableObject
{
    public List<ItemObjectSO> listItems;
    public ItemObjectSO outputItem;
    public string recipeName;
}
