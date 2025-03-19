using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //nhay
    public Rigidbody2D rb;
    public float jumpHeight = 15f;
    private bool isGround = true;

    //animation
    public Animator animator;


    //di chuyen
    private float movement;
    public float moveSpeed = 6f;

    //xoay huong
    private bool facingRight = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //di chuyen
        movement = Input.GetAxis("Horizontal");
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }  
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        //nhay
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
            animator.SetBool("Jump", true);
        }

        //tan cong
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }    

        //animation trans
        if (Mathf.Abs(movement) >= .1f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if (movement < .1f)
        {
            animator.SetFloat("Run", 0f);
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2 (0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
            animator.SetBool("Jump", false) ;
        }    
    }
}
