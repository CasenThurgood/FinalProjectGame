using System;
using System.Net.Security;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

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
    
    [SerializeField] private bool isGrounded;

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
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, .5f, groundLayer);

        float inputY = playerControls.ReadValue<Vector2>().y;
        
        if (inputY > 0.01f && isGrounded)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpHeight));
            isGrounded = false;
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

}
