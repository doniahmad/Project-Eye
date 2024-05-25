using System;
using UnityEngine;
using UnityEngine.UI;

public class BautSlot : MonoBehaviour
{
    public static BautSlot Instance { get; set; }

    public ItemObjectSO itemObjectSO;
    public bool isFilled = false;

    private Image bautImg;

    private void Start()
    {
        Instance = this;
        bautImg = GetComponent<Image>();
        bautImg.color = new Color(1, 1, 1, .25f);
    }

    public void InsertBaut(ItemObjectSO itemObjectSO)
    {
        if (itemObjectSO.objectName == "Baut")
        {
            this.itemObjectSO = itemObjectSO;
            isFilled = true;
            FumehoodUI.Instance.CheckBaut();
            bautImg.color = new Color(1, 1, 1, 1);
        }
    }
}
