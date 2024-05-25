using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumeCupboard : BaseItem
{
    public static FumeCupboard Instance { get; set; }

    public FumehoodUI fumehoodUI;
    public bool isSolved;

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(PlayerController player)
    {
        if (!isSolved)
        {
            fumehoodUI.Show();
        }
        else
        {

        }
    }

    public void SetSolved()
    {
        isSolved = true;
    }
}
