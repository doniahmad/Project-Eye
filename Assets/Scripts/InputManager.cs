using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions player;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerInteract interact;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        player = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        interact = GetComponent<PlayerInteract>();

        player.Jump.performed += ctx => motor.Jump();
        player.Interact.performed += ctx => interact.OnInteractAction();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from movement action.
        motor.ProcessMove(GetVector2Normalized());
    }
    private void LateUpdate()
    {
        look.ProcessLook(player.Look.ReadValue<Vector2>());
    }

    private Vector2 GetVector2Normalized()
    {
        Vector2 inputVector = player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
    private void OnEnable()
    {
        player.Enable();
    }
    private void OnDisable()
    {
        player.Disable();
    }
}
