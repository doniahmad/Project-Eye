using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private PlayerController playerController;
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
        }
        else
        {
            playerController.GetPlayerAnimation().StartIdleAnimation();
        }

        // playerVelocity.y += gravity * Time.deltaTime;
        // if (isGrounded && playerVelocity.y < 0)
        //     playerVelocity.y = -2f;
        // controller.Move(playerVelocity * Time.deltaTime);
    }

    // public void Jump()
    // {
    //     if (isGrounded)
    //     {
    //         playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    //     }
    // }
}
