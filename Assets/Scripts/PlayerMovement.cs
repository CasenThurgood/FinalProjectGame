using System;
using System.Net.Security;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float friction;
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Jumping")]
    [SerializeField] private float jumpHeight;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask dieLayer;
    [SerializeField] private LayerMask ladderLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] public bool isDie;
    [SerializeField] private bool onLadder;

    public InputAction playerControls;
    public TextMeshProUGUI usernameText;

    Vector2 moveDirection = Vector2.zero;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        string username = PlayerPrefs.GetString("username", "Player");
        usernameText.text = "Welcome, " + username + "!";
        rb = GetComponent<Rigidbody2D>();
        jumpHeight *= 300;
    }

    void FixedUpdate()
    {
        
        if (!isDie)
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, .5f, groundLayer);
            isDie = Physics2D.Raycast(transform.position, Vector2.down, .5f, dieLayer);

            float inputY = playerControls.ReadValue<Vector2>().y;
            
            if (inputY > 0.01f && isGrounded && !onLadder)
            {
                rb.AddForce(new Vector2(rb.linearVelocity.x, jumpHeight));
                isGrounded = false;
            }
            else if (onLadder)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, inputY * moveSpeed);
            }
            
            float input = playerControls.ReadValue<Vector2>().x;

            // target speed
            float targetSpeed = input * maxSpeed;

            // difference between current and target
            float speedDif = targetSpeed - rb.linearVelocityX;

            // acceleration rate
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? moveSpeed : friction;

            // calculate force
            float movement = speedDif * accelRate;

            rb.AddForce(movement * Vector2.right);

        }

        if (transform.position.y < -100 || isDie)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            StartCoroutine(AnimationWait());
        }

        if (transform.position.x <= -12f)
        {
            rb.freezeRotation = false;
        }
        else
        {
            rb.freezeRotation = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(0,-1.16002f,0);
        isDie = false;
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            isDie = true;
        }

        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            rb.gravityScale = 0;
            onLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            rb.gravityScale = 2;
            onLadder = false;
        }
    }
}
