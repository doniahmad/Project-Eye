using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public string MOVE_PARAMS = "IsMoving";
    public string RUN_PARAMS = "IsRunning";
    [SerializeField] private Animator anim;

    private void Start()
    {

    }

    public void StartMoveAnimation()
    {
        anim.SetBool(MOVE_PARAMS, true);
    }

    public void StartRunAnimation()
    {
        anim.SetBool(RUN_PARAMS, true);
    }

    public void StartIdleAnimation()
    {
        anim.SetBool(MOVE_PARAMS, false);
        anim.SetBool(RUN_PARAMS, false);
    }
}
