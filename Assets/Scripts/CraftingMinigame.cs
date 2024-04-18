using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMinigame : MonoBehaviour
{
    public static event EventHandler OnSuccessCrafting;
    public static event EventHandler OnFailedCrafting;

    [SerializeField] private RectTransform greenArea;
    [SerializeField] private RectTransform pointer;
    [SerializeField] private float pointerSpeed;
    [SerializeField] private float greenAreaSize;
    [SerializeField] private Button craftingButton;

    private RectTransform thisRectTransform;
    private float startPointer;
    private float endPointer;
    private bool pointerMove = false;

    private void Awake()
    {
        PointerHide();
        GreenAreaHide();
    }

    private void Start()
    {
        thisRectTransform = GetComponent<RectTransform>();
        HandlePointerMovement();

        startPointer = pointer.rect.width * pointer.pivot.x;
        endPointer = thisRectTransform.rect.width - startPointer;

        pointer.anchoredPosition = new Vector2(startPointer, 0);
    }

    private void Update()
    {
        if (pointerMove)
        {
            HandlePointerMovement();
        }
    }

    private void HandlePointerMovement()
    {
        // Smooth back-and-forth movement of the pointer within the specified range
        float newX = Mathf.PingPong(Time.time * pointerSpeed, endPointer - startPointer) + startPointer;
        pointer.anchoredPosition = new Vector2(newX, pointer.anchoredPosition.y);
    }

    private void RandomGreenArea()
    {
        SetGreenAreaWidth(greenAreaSize);
        float minArea = greenArea.rect.width * greenArea.pivot.x;
        float maxArea = thisRectTransform.rect.width - minArea;

        float randomPosition = UnityEngine.Random.Range(minArea, maxArea);

        // Set the position of the green area
        Vector2 newPosition = greenArea.anchoredPosition;
        newPosition.x = randomPosition;
        greenArea.anchoredPosition = newPosition;
    }

    private void HandleCrafting()
    {
        // Menghentikan pergerakan pointer
        StopPointer();

        // Mendapatkan posisi pointer dalam koordinat dunia
        Vector3 pointerWorldPos = pointer.position;

        // Mengonversi posisi pointer ke dalam koordinat lokal green area
        Vector2 pointerLocalPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(greenArea, pointerWorldPos, null, out pointerLocalPos);

        // Mengambil ukuran setengah dari lebar green area
        float halfGreenAreaWidth = greenArea.rect.width * 0.5f;

        // Memeriksa apakah pointer berada di dalam green area
        if (Mathf.Abs(pointerLocalPos.x) < halfGreenAreaWidth)
        {
            OnSuccessCrafting?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnFailedCrafting?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetGreenAreaWidth(float newWidth)
    {
        Vector2 newSizeDelta = greenArea.sizeDelta;
        newSizeDelta.x = newWidth;
        greenArea.sizeDelta = newSizeDelta;
    }

    private void StopPointer()
    {
        pointerMove = false;
    }

    private void PointerShow()
    {
        pointer.gameObject.SetActive(true);
    }

    private void PointerHide()
    {
        pointer.gameObject.SetActive(false);
    }

    private void GreenAreaShow()
    {
        greenArea.gameObject.SetActive(true);
    }

    private void GreenAreaHide()
    {
        greenArea.gameObject.SetActive(false);
    }

    public void StartCraftingMinigame()
    {
        RandomGreenArea();

        PointerShow();
        GreenAreaShow();

        pointerMove = true;

        craftingButton.onClick.AddListener(HandleCrafting);
    }
}
