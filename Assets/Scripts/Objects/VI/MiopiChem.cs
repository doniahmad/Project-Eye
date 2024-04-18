using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MiopiChem : BaseItem
{
    public PostProcessVolume postProcessVolume;
    public PostProcessProfile postProcessProfile;

    public override void Interact(PlayerController player)
    {
        Debug.Log(postProcessVolume.sharedProfile);
        if (postProcessVolume.sharedProfile != postProcessProfile)
        {
            postProcessVolume.sharedProfile = postProcessProfile;
        }
        else
        {
            postProcessVolume.sharedProfile = null;
        }

    }
}
