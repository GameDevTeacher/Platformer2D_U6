using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpSpeed = 4f;
    
    public Transform target;
    public float sightRadius = 4f;
    public bool canJump;
    public bool enemyIsOnGround;

    public Transform wallCheck;
    public Transform groundCheck;

    public LayerMask whatIsGround;

    private float timeCounter;
    private float timeCountValue = 2f;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {

        enemyIsOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        
        
        if (target != null)
        {
            _moveDirection = target.position - transform.position;
            if (enemyIsOnGround)
            {
                transform.localScale = new Vector3(transform.localScale.x * Mathf.Sign(_moveDirection.x), 1f);
            }
        
            if (enemyIsOnGround && Vector3.Distance(target.position, transform.position) < sightRadius)
            {
                canJump = true;
            }
        }
        
        if (canJump && !Physics2D.OverlapCircle(wallCheck.position, 0.2f, whatIsGround))
        {
            if (Time.time > timeCounter)
            {
                Jump();
                timeCounter = Time.time + timeCountValue;
            }
            
        }
        
    }

    private void Jump()
    {
        _rigidbody2D.linearVelocityX = _moveDirection.x * moveSpeed;
        _rigidbody2D.linearVelocityY = jumpSpeed;
        canJump = false;
    }

    private void Bounce()
    {
        _rigidbody2D.linearVelocityY = jumpSpeed/1.3f;
        _rigidbody2D.linearVelocityX = 0;
    }
}
