using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum Status
    {
        DirtyGloved,
        Dirty,
        Clean,
        AfterReadBook,
        AfterWritingRecipe,
        CleanGloved
    }

    public Status status;

    private void Awake()
    {
        status = Status.DirtyGloved;
    }
}
