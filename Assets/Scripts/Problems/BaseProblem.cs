using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProblem
{
    public bool isSolved = false;

    public virtual void HandleProblem()
    {
        Debug.Log("Start Problem");
    }

    public void SetProblemSolved()
    {
        isSolved = true;
    }
}
