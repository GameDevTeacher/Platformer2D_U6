using UnityEngine;

public class InputActions : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    public float Horizontal {get; private set; }

    public bool Jump {get; private set; }
    public bool Attack {get; private set; }
    public bool Interact {get; private set; }


    private void Update()
    {
        Horizontal = _inputActions.Player.Move.ReadValue<Vector2>().x;
        
        Jump = _inputActions.Player.Jump.WasPressedThisFrame();
        Attack = _inputActions.Player.Attack.WasPressedThisFrame();
        Interact = _inputActions.Player.Interact.WasPressedThisFrame();
    }

    private void Awake() { _inputActions = new InputSystem_Actions(); }

    private void OnEnable() { _inputActions.Enable(); }

    private void OnDisable() { _inputActions.Disable(); }
}
