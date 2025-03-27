using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //chi so
    public int maxHealth = 5;
    public Text health;

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

    //attack
    public Transform attackPoint;
    public float attackRadius = 1.92f;
    public LayerMask attackLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //die
        if(maxHealth <= 0)
        {

            Die();

        }
        //chi so
        health.text = maxHealth.ToString();
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

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);

        if (collInfo)
        {
            //Debug.Log(collInfo.gameObject.name + " takes dameages");
            if (collInfo.gameObject.GetComponent<PatrolEnemy>() != null)
            {
                collInfo.gameObject.GetComponent<PatrolEnemy>().TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(maxHealth <= 0)
        {
            return;
        } maxHealth -= damage;
    }    

    void Die()
    {
        Debug.Log("You Die");
        FindObjectOfType<GameManager>().isGameActive = false;
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        //attack
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
