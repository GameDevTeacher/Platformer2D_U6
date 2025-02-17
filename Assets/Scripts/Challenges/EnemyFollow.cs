using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed;
    
    public Transform target;
    public float sightRange;
    public bool canChase;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (target == null) return;

        _moveDirection = target.position - transform.position;
        
        if (Vector2.Distance(target.position, transform.position) < sightRange)
        {
            canChase = true;
        }

        if (transform.position.x > target.position.x)
        {
            transform.localScale = transform.position.x > target.position.x ? 
                new Vector2(1, 1) : new Vector2(-1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (canChase)
        {
            _rigidbody2D.linearVelocity = _moveDirection * moveSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;                                
        Gizmos.DrawWireSphere(transform.position, sightRange);    
    }
}