using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumehoodTriggerEnter : MonoBehaviour
{
    public bool isFirstTimeEnter = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFirstTimeEnter = true;
        }
    }
}
