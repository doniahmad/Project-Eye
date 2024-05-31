using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakDoorSlide : BaseItem
{
    public static event EventHandler OnActionRakDoorSlide;

    [SerializeField] private float openRadius;
    [SerializeField] private float openSpeed = 2f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;
    private Collider coll;

    public static void ResetStaticData()
    {
        OnActionRakDoorSlide = null;
    }

    private void Start()
    {
        coll = GetComponent<Collider>();
        InteractCommand = "Open Rak";
        initialPosition = transform.localPosition;
        targetPosition = new Vector3(openRadius, initialPosition.y, initialPosition.z);
    }

    public override void Interact(PlayerController player)
    {
        OnActionRakDoorSlide?.Invoke(this, EventArgs.Empty);
        if (!isOpening)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            InteractCommand = "Close Rak";
            StartCoroutine(AnimateDoor(targetPosition));
        }
    }

    public void CloseDoor()
    {
        if (isOpening)
        {
            isOpening = false;
            InteractCommand = "Open Rak";
            StartCoroutine(AnimateDoor(initialPosition));
        }
    }

    private IEnumerator AnimateDoor(Vector3 target)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.localPosition;

        while (elapsedTime < 1f)
        {
            transform.localPosition = Vector3.Lerp(startPosition, target, elapsedTime);
            coll.enabled = false;
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        coll.enabled = true;
        transform.localPosition = target;
    }
}
