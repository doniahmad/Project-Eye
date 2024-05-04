using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public ItemObjectSO itemObjectSO;
    [Header("Icon Setting")]
    public RectTransform rectTransform;
    public Image itemSprite;
    private RectTransform initialRectTransform;

    private void Start()
    {
        initialRectTransform = rectTransform;
    }

    public void UpdateItem(ItemObjectSO _itemObjectSO)
    {
        itemObjectSO = _itemObjectSO;
        if (itemObjectSO != null)
        {
            itemName.text = itemObjectSO.name;
            itemSprite.sprite = itemObjectSO.sprite;
            itemSprite.color = new Color(255, 255, 255, 100);
            FitToParent();
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
        itemSprite.color = new Color(255, 255, 255, 0);
        rectTransform = initialRectTransform;
    }

    private void FitToParent()
    {
        if (rectTransform == null || itemSprite == null)
            return;

        float containerWidth = rectTransform.GetComponent<RectTransform>().rect.width;
        float containerHeight = rectTransform.GetComponent<RectTransform>().rect.height;

        float imageWidth = itemSprite.sprite.rect.width;
        float imageHeight = itemSprite.sprite.rect.height;

        float scaleFactor = Mathf.Min(containerWidth / imageWidth, containerHeight / imageHeight);

        rectTransform.sizeDelta = new Vector2(imageWidth * scaleFactor, imageHeight * scaleFactor);
    }
}
