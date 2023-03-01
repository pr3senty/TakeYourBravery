using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputHandler", menuName = "Input/InputController")]
public class InputHandler : ScriptableObject, GameInput.IGameplayActions
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<Vector2> OnCameraMoveEvent = delegate { };
    public event UnityAction OnRunEvent = delegate { };
    public event UnityAction StopRunEvent = delegate { };
    public event UnityAction StopMoveEvent = delegate { };
    public event UnityAction OnIntractEvent = delegate { };

    private GameInput _gameInput;

    public InputActionAsset GameInputActionAsset { get => _gameInput.asset; }

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
        }
        _gameInput.Gameplay.Enable();

    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Disable();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            StopMoveEvent.Invoke();
        }
    }
    
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRunEvent.Invoke();
        }
        else if (context.canceled)
        {
            StopRunEvent.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnCameraMoveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnIntractEvent.Invoke();
        }
    }
}
