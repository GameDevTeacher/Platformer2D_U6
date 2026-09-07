using Player;
using UnityEngine;

public class UpdatedPlayerController : MonoBehaviour
{
        // TODO: Add Acceleration & Friction to Player  ✓
                // TODO: -> Add Air & Ground Friction   ✓
        // TODO: Add CoyoteTime                         ✓
        // TODO: Add Max Vertical Velocity              ✓
        // TODO: Add Double Jump                        ✓
        // TODO: Add Variable Jump Height               ✓
        
        [Header("Movement")]
        public float maxVelocityX = 6f;
        public float maxVelocityY = 16f;
        public float acceleration = 1f;
        public float groundFriction = .3f;
        public float airFriction = 0.005f;
        private Vector2 _currentVelocity;
        private float _moveSpeed;
        
        [Header("Jumping")]
        public float jumpForce = 10f;
        public float coyoteTime = 0.15f;
        public int maxDoubleJumpValue = 1;
        public int _doubleJumpValue;

        public float jumpTimeCounter;
        public float jumpTime = 0.25f;
        
        public float _coyoteTimeCounter;
        private bool _isCoyoteTime;
        public bool _isJumping;

        [Header("Components")]
        private UpdatedInputManager _input;
        private PlayerCollision _collision;
        private Rigidbody2D _rigidbody2D;

        // Start is called before the first frame update
        private void Start()
        {
            _input = GetComponent<UpdatedInputManager>();
            _collision = GetComponent<PlayerCollision>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            UpdateJumping();
            
            if (_collision.IsGroundedBox() && _rigidbody2D.linearVelocity.y < 0f)
            {
                _isJumping = false;
                _doubleJumpValue = maxDoubleJumpValue;
            }
        }
        
        private void FixedUpdate()
        {
            UpdateMovement();
        }
        
        private void UpdateJumping()
        {
            VariableJumpHeight();
            // TODO: Add Fine Tuned Apex Gravity Control
            if (!_isJumping && !_collision.IsGroundedBox())
            {
                _coyoteTimeCounter += Time.deltaTime;
            }
            else
            {
                _coyoteTimeCounter = 0; 
                
            }
            
            if (_input.JumpPressed && (_collision.IsGroundedBox() || (_coyoteTimeCounter > 0.03f 
                                                                               && _coyoteTimeCounter < coyoteTime)))
            {
                _rigidbody2D.linearVelocity = Vector2.up * jumpForce;
                jumpTimeCounter = jumpTime;
                _isJumping = true;
            }
            else if (_input.JumpPressed && _doubleJumpValue > 0)
            {
                _rigidbody2D.linearVelocity = Vector2.up * jumpForce;
                _doubleJumpValue--;
              
                /* Add this if you want Variable Jump Height in the AIR
                jumpTimeCounter = jumpTime;
                _isJumping = true;
                */
            }
        }
        
        private void UpdateMovement()
        {
            // Store Rigidbody2D.Velocity in _velocity
            _currentVelocity = _rigidbody2D.linearVelocity;
            _currentVelocity.y = Mathf.Clamp(_currentVelocity.y, -maxVelocityY, maxVelocityY);

            // Change the Velocity
            if (_input.MoveVector.x != 0)
            {
                _moveSpeed += _input.MoveVector.x * acceleration;
                _moveSpeed = Mathf.Clamp(_moveSpeed, -maxVelocityX, maxVelocityX);
            }
            else
            {
                _moveSpeed = Mathf.Lerp(_moveSpeed, 0f, _collision.IsGroundedBox() ? groundFriction : airFriction);
            }

            _currentVelocity.x = _moveSpeed;

            // Return current Velocity into Rigidbody2D.velocity
            _rigidbody2D.linearVelocity = _currentVelocity;
        }

        private void VariableJumpHeight()
        {
            if (_input.JumpValue > 0f || _isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    _rigidbody2D.linearVelocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
        }
    }
