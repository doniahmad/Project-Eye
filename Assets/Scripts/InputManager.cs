using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDING = "PlayerGameInput";

    public static InputManager Instance { get; private set; }

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Interact,
        Inventory1,
        Inventory2
    }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInventory1Action;
    public event EventHandler OnInventory2Action;
    public event EventHandler OnPauseAction;

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions player;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        playerInput = new PlayerInput();
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDING))
        {
            playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING));
        }
        player = playerInput.OnFoot;

        player.Enable();

        player.Interact.performed += Interact_perfomed;
        player.Escape.performed += Escape_Perfomed;
        player.Inventory1.performed += Invetory1_Perfomed;
        player.Inventory2.performed += Invetory2_Perfomed;
    }


    private void Interact_perfomed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Invetory2_Perfomed(InputAction.CallbackContext context)
    {
        OnInventory2Action?.Invoke(this, EventArgs.Empty);
    }

    private void Invetory1_Perfomed(InputAction.CallbackContext context)
    {
        OnInventory1Action?.Invoke(this, EventArgs.Empty);
    }

    private void Escape_Perfomed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetVector2Normalized()
    {
        Vector2 inputVector = player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetLookVector2()
    {
        Vector2 inputVector = player.Look.ReadValue<Vector2>();
        return inputVector;
    }

    public void OnEnable()
    {
        player.Enable();
    }
    public void OnDisable()
    {
        player.Disable();
    }

    public void StopMotion()
    {
        player.Look.Disable();
        player.Movement.Disable();
    }

    public void StartMotion()
    {
        player.Look.Enable();
        player.Movement.Enable();
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInput.OnFoot.Movement.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInput.OnFoot.Movement.bindings[2].ToDisplayString();
            case Binding.Move_Right:
                return playerInput.OnFoot.Movement.bindings[3].ToDisplayString();
            case Binding.Move_Left:
                return playerInput.OnFoot.Movement.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInput.OnFoot.Interact.bindings[0].ToDisplayString();
            case Binding.Inventory1:
                return playerInput.OnFoot.Inventory1.bindings[0].ToDisplayString();
            case Binding.Inventory2:
                return playerInput.OnFoot.Inventory2.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInput.OnFoot.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInput.OnFoot.Movement;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInput.OnFoot.Movement;
                bindingIndex = 2;
                break;
            case Binding.Move_Right:
                inputAction = playerInput.OnFoot.Movement;
                bindingIndex = 3;
                break;
            case Binding.Move_Left:
                inputAction = playerInput.OnFoot.Movement;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInput.OnFoot.Interact;
                bindingIndex = 0;
                break;
            case Binding.Inventory1:
                inputAction = playerInput.OnFoot.Inventory1;
                bindingIndex = 0;
                break;
            case Binding.Inventory2:
                inputAction = playerInput.OnFoot.Inventory2;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            playerInput.OnFoot.Enable();
            onActionRebound();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDING, playerInput.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
}
