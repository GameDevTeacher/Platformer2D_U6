using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed;
    
    [Space(5)]
    public Transform leftWallCheck;
    public Transform rightWallCheck;
    public LayerMask whatIsWall;
    [Space(5)]
    public int distanceToTravel;
    private Vector2 _startPosition;
    
    [Space(5)]
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (HitWall())
        {
            platformSpeed *= -1;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = platformSpeed * Time.fixedDeltaTime;
    }

    private bool HitWall()
    {
        return Physics2D.OverlapCircle(leftWallCheck.position, 0.1f, whatIsWall) || Physics2D.OverlapCircle(rightWallCheck.position, 0.1f, whatIsWall)
            || transform.position.x > (_startPosition.x + distanceToTravel) || transform.position.x < (_startPosition.x - distanceToTravel);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(leftWallCheck.position, 0.1f);
        Gizmos.DrawWireSphere(rightWallCheck.position, 0.1f);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector2(_startPosition.x - distanceToTravel, _startPosition.y), 0.1f);
        Gizmos.DrawWireSphere(new Vector2(_startPosition.x + distanceToTravel, _startPosition.y), 0.1f);
    }
}