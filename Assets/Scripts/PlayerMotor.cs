using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private PlayerController playerController;
    private bool onChangePosition;
    private bool isWalking;
    private float footstepTimer;
    public float footstepTimerMax = .7f;
    // private Vector3 playerVelocity;
    // private bool isGrounded;
    public float moveSpeed = 5.0f;
    // public float gravity = -9.8f;
    // public float jumpHeight = 1f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            if (isWalking == true)
            {
                SoundManager.Instance.PlayFootstep(transform.position, 0.8f * SoundManager.Instance.GetVolume());
            }
        }
    }

    private void FixedUpdate()
    {
        ProcessMove(InputManager.Instance.GetVector2Normalized());
    }

    // Update is called once per frame
    // void Update()
    // {
    //     isGrounded = controller.isGrounded;
    // }

    //receive input for inputmanager and apply to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            playerController.GetPlayerAnimation().StartMoveAnimation();
            isWalking = true;
        }
        else
        {
            playerController.GetPlayerAnimation().StartIdleAnimation();
            isWalking = false;
        }

        // playerVelocity.y += gravity * Time.deltaTime;
        // if (isGrounded && playerVelocity.y < 0)
        //     playerVelocity.y = -2f;
        // controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ChangePlayerPosition(Transform pos)
    {
        controller.enabled = false;
        transform.position = pos.position;
        controller.enabled = true;
    }

    // public void Jump()
    // {
    //     if (isGrounded)
    //     {
    //         playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    //     }
    // }
}
