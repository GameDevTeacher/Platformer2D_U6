using UnityEngine;
using UnityEngine.InputSystem;


public class DirectInput : MonoBehaviour
{
    public float Horizontal;
    private float _left, _right, _up, _down;
    
    public bool Jump;
    public bool Interact;
    
    public bool useGamepad;
    public bool useDpad;

    private void Update()
    {
        if (useGamepad == true)
        {
            if (useDpad == true)
            {
                _left = Gamepad.current.dpad.left.isPressed ? -1 : 0;
                _right = Gamepad.current.dpad.right.isPressed ? 1 : 0;
            }
            else
            {
                Horizontal = Gamepad.current.leftStick.ReadValue().x;
            }

            Jump = Gamepad.current.buttonSouth.wasPressedThisFrame;
            Interact = Gamepad.current.buttonEast.wasPressedThisFrame;

        }
        else
        {
            _left = Keyboard.current.aKey.isPressed ? -1 : 0;
            _right = Keyboard.current.dKey.isPressed ? 1 : 0;
            Horizontal = _left + _right;
        
            Jump = Keyboard.current.spaceKey.wasPressedThisFrame;
            Interact = Keyboard.current.fKey.wasPressedThisFrame;
        }
    }
}
