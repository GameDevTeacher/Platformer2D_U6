using UnityEngine;

public class InputActions : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    public float Horizontal;
    
    public bool Jump;
    public bool Attack;
    public bool Interact;


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
