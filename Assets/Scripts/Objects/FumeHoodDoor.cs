using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumeHoodDoor : BaseItem
{
    [SerializeField] private float openRadius;
    [SerializeField] private float openSpeed = 2f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;
    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
        initialPosition = transform.localPosition;
        targetPosition = new Vector3(initialPosition.x, openRadius, initialPosition.z);
    }

    public override void Interact(PlayerController player)
    {
        if (FumeCupboard.Instance.isSolved)
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
        else
        {

        }
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            InteractCommand = "Tutup";
            StartCoroutine(AnimateDoor(targetPosition));
        }
    }

    public void CloseDoor()
    {
        if (isOpening)
        {
            isOpening = false;
            InteractCommand = "Buka";
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
