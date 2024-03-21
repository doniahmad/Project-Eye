using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TritanomalyChem : BaseObject
{
    private Camera mainCamera;
    [Range(0.0f, 1.0f)] public float severity = 1f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public override void Interact(PlayerInteract player)
    {
        // Get the colorBlindness from a child object of the camera
        ColorBlindness colorBlindness = mainCamera.GetComponent<ColorBlindness>();

        // Check if the colorBlindness is found
        if (colorBlindness != null)
        {
            Debug.Log("Active the Color Blind");
            colorBlindness.blindType = ColorBlindness.BlindTypes.Tritanomaly;
            colorBlindness.severity = severity;
            if (!colorBlindness.enabled)
            {
                colorBlindness.enabled = true;
            }
        }
        else
        {
            Debug.LogError("colorBlindness not found.");
        }
    }
}
