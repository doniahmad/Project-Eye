using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        TaskManager.ResetStaticData();
        TableWastafel.ResetStaticData();
        RakDoorSlide.ResetStaticData();
        PlaceableItem.ResetStaticData();
        Item.ResetStaticData();
        GloveBox.ResetStaticData();
        Door.ResetStaticData();
    }
}
