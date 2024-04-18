using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public string InteractCommand = "Take Item";

    public virtual void Interact(PlayerController player)
    {
        Debug.Log("Interact");
    }
}
