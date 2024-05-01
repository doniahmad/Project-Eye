using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakDoor : BaseItem
{
    [SerializeField] private float openAngle;
    [SerializeField] private float rotationSpeed = 2f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isOpening = false;
    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
        InteractCommand = "Open Rak";
        initialRotation = transform.localRotation;
        targetRotation = Quaternion.Euler(0f, openAngle, 0f) * initialRotation;
    }

    public override void Interact(PlayerController player)
    {
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
            StartCoroutine(AnimateDoor(targetRotation));
        }
    }

    public void CloseDoor()
    {
        if (isOpening)
        {
            isOpening = false;
            InteractCommand = "Open Rak";
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
