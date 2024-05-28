using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform iconObject;
    private Vector2 startPos;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private ItemUI itemUI;
    private bool isDraggable;
    private Vector2 initialOffset;

    private void Start()
    {
        rectTransform = iconObject.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemUI = GetComponent<ItemUI>();
    }

    // On Begin Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemUI.itemObjectSO != null)
        {
            isDraggable = true;
        }
        else
        {
            isDraggable = false;
        }

        if (!isDraggable) return;

        startPos = rectTransform.anchoredPosition;
        initialOffset = rectTransform.anchoredPosition - eventData.position / canvas.scaleFactor;
        rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    // On Drag Event
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;
        Vector2 newPosition = eventData.position / canvas.scaleFactor + initialOffset;
        rectTransform.anchoredPosition = newPosition;
    }

    // On Drag Event End
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 distanceObject = startPos - rectTransform.anchoredPosition;
        if (distanceObject.magnitude > 0)
        {
            rectTransform.anchoredPosition = startPos;
        }

        rectTransform.localScale = Vector3.one;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
