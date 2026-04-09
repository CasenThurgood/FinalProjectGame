using System;
using System.Net.Security;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float sx;
    [SerializeField] private float friction;
    
    [Header("Gravity")]
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float gravityLevel;
    [SerializeField] private float sy;
    
    [SerializeField] private bool isGrounded;


    public bool isSlippery = false;

    public InputAction playerControls;

    Vector2 moveDirection = Vector2.zero;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        //Right
        if (playerControls.ReadValue<Vector2>().x > 0.1f)
        {
            if (sx < 0)
            {
                sx = 0;
            }
            sx += moveSpeed * Time.deltaTime;
        }
        //Left
        else if (playerControls.ReadValue<Vector2>().x < -0.1f)
        {
            if (sx > 0)
            {
                sx = 0;
            }
            sx -= moveSpeed * Time.deltaTime;
        }
        else
        {
            if (!isSlippery)
            {
                sx = 0;
                return;
            }
            sx = Math.Sign(sx) * (Math.Abs(sx) - friction * Time.deltaTime);
            if (sx < 0.1f && sx > -0.1f)
            {
                sx = 0;
            }
        }          
        
        sx = Math.Clamp(sx, -maxSpeed, maxSpeed);

        if (playerControls.ReadValue<Vector2>().y > 0.1f && isGrounded)
        {
            sy = moveSpeed * 2 * Time.deltaTime;
        }
        if (!isGrounded)
        {
            sy -= gravityLevel * Time.deltaTime;
            sy = Math.Clamp(sy, -maxFallSpeed, maxFallSpeed*2);
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(sx, 0, 0) * Time.fixedDeltaTime;
    }
}
