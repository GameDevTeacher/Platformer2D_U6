using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 2f;
    
    public Transform target;
    public float sightRadius = 4f;
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
        
        if (Vector3.Distance(target.position, transform.position) < sightRadius)
        {
            canChase = true;
        }
    }

    private void FixedUpdate()
    {
        if (canChase)
        {
            _rigidbody2D.linearVelocity = _moveDirection * moveSpeed;
        }
    }
}