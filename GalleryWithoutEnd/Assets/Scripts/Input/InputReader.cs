using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="InputReader", menuName ="Gameplay/Input Reader")]
public class InputReader : DescriptionBaseSO, GameInput.IGameplayActions
{
    // Assign delegate{} to events to skip null checks.

    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction<Vector2> lookEvent = delegate { };
    public event UnityAction pauseEvent = delegate { };

    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.Gameplay.Enable();
        }
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Disable();
    }

    #region Gameplay

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public Vector2 GetMovementVector()
    {
        return _gameInput.Gameplay.Move.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        lookEvent.Invoke(context.ReadValue<Vector2>());
    }

    public Vector2 GetLookVector()
    {
        return _gameInput.Gameplay.Look.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        pauseEvent.Invoke();
    }

    #endregion
}
