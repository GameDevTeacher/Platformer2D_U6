using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 7f;

    public int playerHealth;
    
    public bool playerIsGrounded;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f,0.2f);

    public float damageCooldown;
    private float _damageCoolDownTimer;

    private Animator _animator;
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerIsGrounded = Physics2D.OverlapBox(transform.position, groundBoxSize, 0f, whatIsGround);
        
        
        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
        UpdateAnimation();
        Attack();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            RestartScene();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, groundBoxSize);
    }

    private static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Attack()
    {
        if (!Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Enemy"))) return;

        var enemyColliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemyColliders)
        {
            Destroy(enemy.gameObject); 
        }
        _rigidbody2D.linearVelocityY = jumpSpeed/1.3f;
    }

    private void TakeDamage()
    {
        if (Time.time > _damageCoolDownTimer)
        {
            playerHealth -= 1;
            _damageCoolDownTimer = Time.time + damageCooldown;
        }

        if (playerHealth <= 0)
        {
            RestartScene();
        }
    }

    private void UpdateAnimation()
    {

        if (_input.Horizontal != 0)
        {
            transform.localScale = new Vector2(
                Mathf.Sign(_input.Horizontal), 
                1f);
        }
       
        
        if (playerIsGrounded)
        {
            if (_input.Horizontal != 0)
            {
                _animator.Play("Player_Walk");
            }
            else
            {
                _animator.Play("Player_Idle");
            }
        }
        else
        {
            if (_rigidbody2D.linearVelocityY > 0)
            {
                _animator.Play("Player_Jump");
            }
            else
            {
                _animator.Play("Player_Fall");
            }
        }
    }
    
}