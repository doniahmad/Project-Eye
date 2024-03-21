using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public virtual void Interact(PlayerInteract player)
    {
        Debug.Log("Interact");
    }
}
