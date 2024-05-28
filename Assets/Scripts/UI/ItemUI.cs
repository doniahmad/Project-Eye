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
            gameObject.SetActive(true);
            FitToParent();
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
        // Get parent RectTransform
        RectTransform parentRectTransform = rectTransform.GetComponent<RectTransform>();

        // Get parent's width and height
        float containerWidth = parentRectTransform.rect.width;
        float containerHeight = parentRectTransform.rect.height;

        // Get image's original width and height
        float imageWidth = itemSprite.sprite.rect.width;
        float imageHeight = itemSprite.sprite.rect.height;

        // Calculate scale factor to fit image inside parent
        float scaleFactor = Mathf.Min(containerWidth / imageWidth, containerHeight / imageHeight);

        itemSprite.rectTransform.sizeDelta = new Vector2(imageWidth, imageHeight) * scaleFactor;

    }
}
