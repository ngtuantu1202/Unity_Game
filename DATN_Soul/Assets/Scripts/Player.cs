using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //di chuyen
    private float movement;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool facingRight = true;

    //nhay
    private float jumpHeight = 17f;
    Rigidbody2D rb;
    private bool isGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //nhay
        isGround = CheckIfGrounded();
        Jump();

        //di chuyen
        if (Input.GetKey(KeyCode.A)) 
        {
            movement = -1f;
            if (facingRight)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            movement = 1f;
            if (!facingRight)
            {
                Flip();
            }
        }
        else
        {
            movement = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
    }


    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,groundLayer); 

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = Vector2.up * jumpHeight;
        }
    }
    private void Flip()
    {
        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f); 
            facingRight = false; 
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f); 
            facingRight = true;  
        }
    }
}
