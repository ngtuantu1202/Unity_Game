using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    //health dameage
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [SerializeField] private bool facingLeft = true;
    [SerializeField] private float moveSpeed = 2f;
    public Transform checkPoint;
    public float distance = 1f;
    public LayerMask layerMask;
    public Transform player;
    public float attackRange = 10f;
    public float retrieveDistance = 2.5f;
    public float chaseSpeed = 4f;
    public Animator animator;

    //tan cong
    public bool inRange = false;
    //event
    public Transform attackPoint;
    public float attackRadius = 1.4f;
    public LayerMask attackLayer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //end game
        if (FindObjectOfType<GameManager>().isGameActive == false)
        {
            return;
        }
        //die
        if (currentHealth <= 0)
        {
            Die();
        }

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (inRange)
        {
            //Flip
            if(player.position.x > transform.position.x && facingLeft)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingLeft = false;
            }
            else if (player.position.x < transform.position.x && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingLeft = true;
            }

            if(Vector2.Distance(transform.position, player.position) > retrieveDistance)
            {
                animator.SetBool("Attack1", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }    
            else
            {
                animator.SetBool("Attack1", true);
            }    
        }    
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);

            RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);

            if (hit == false && facingLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                facingLeft = false;
            }
            else if (hit == false && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingLeft = true;
            }
        }    
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);

        if (collInfo)
        {
            if(collInfo.gameObject.GetComponent<Player>() != null)
            {
                collInfo.gameObject.GetComponent<Player>().TakeDamage(1);
            }
        }    
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        animator.SetTrigger("Hurt");
    }

    void Die()
    {
        //Debug.Log(this.transform.name + " Died");

        animator.SetBool("Attack1", false);
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        if(checkPoint == null)
        {
            return;
        }    
        //Ve checkpoint
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);

        //Ve vung trigger enemy
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //attack
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
