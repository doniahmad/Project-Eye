using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCupboardDoor : BaseItem
{
    public event EventHandler OnCloseDoor;

    [SerializeField] private LockedCupboardUI lockedCupboardUI;
    [SerializeField] private float openAngle;
    [SerializeField] private float rotationSpeed = 2f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isOpening = false;
    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
        initialRotation = transform.localRotation;
        targetRotation = Quaternion.Euler(0f, openAngle, 0f) * initialRotation;
    }

    public override void Interact(PlayerController player)
    {
        if (!isOpening)
        {
            lockedCupboardUI.SetPlayer(player);
            lockedCupboardUI.Show();
        }
        else
        {
            OnCloseDoor?.Invoke(this, EventArgs.Empty);
        }
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            InteractCommand = "Close Door";
            StartCoroutine(AnimateDoor(targetRotation));
        }
    }

    public void CloseDoor()
    {
        if (isOpening)
        {
            isOpening = false;
            InteractCommand = "Open Door";
            StartCoroutine(AnimateDoor(initialRotation));
        }
    }

    private IEnumerator AnimateDoor(Quaternion target)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.localRotation;

        while (elapsedTime < 1f)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, target, elapsedTime);
            coll.enabled = false;
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        coll.enabled = true;
        transform.localRotation = target;
    }
}
