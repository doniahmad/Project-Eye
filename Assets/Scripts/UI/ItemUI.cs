using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemName;
    public ItemObjectSO itemObjectSO;

    public void UpdateItem(ItemObjectSO _itemObjectSO)
    {
        itemObjectSO = _itemObjectSO;
        if (itemObjectSO != null)
        {
            itemName.text = itemObjectSO.name;
            itemSprite.sprite = itemObjectSO.sprite;
            gameObject.SetActive(true);
        }
        else
        {
            ClearItem();
        }
    }

    public void ClearItem()
    {
        itemObjectSO = null;
        itemName.text = "";
        itemSprite.sprite = null;
    }
}
