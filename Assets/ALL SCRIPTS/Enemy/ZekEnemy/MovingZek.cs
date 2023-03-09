using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingZek : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public Transform rightPoint;
    [SerializeField] public Transform leftPoint;
    [SerializeField] private Health playerHealth;
    private Animator anim;
    public float speed;
    public float distance;
    private bool moveTowardPoints;
    private bool attackPlayer;
    [Header("CheckItem")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [Header("Attack")]
    public int damage;
    public float startAttackTimer;
    private float finishAttackTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        ray = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z),
        0, Vector2.left, 0, LayerMask.GetMask("Player"));        
        if (finishAttackTimer >= 0f)
        {
            finishAttackTimer -= 1f * Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, player.position) < distance)
        {
            chisingPlayer();
            if (attackPlayer == true)
            {
                if (finishAttackTimer < 0f)
                {
                    AttackPlayer();
                    playerHealth.TakeDamage(damage);
                    finishAttackTimer = startAttackTimer;
                }
            }
        }
        else
        {
            Patrol();
        }        
    }

    public void Patrol()
    {
        anim.SetBool("run", true);
        if (moveTowardPoints == true)
        {
            if (transform.position.x > leftPoint.position.x)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
               moveTowardPoints = false;
            }
        }
        else
        {
            if (transform.position.x < rightPoint.position.x)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                moveTowardPoints = true;
            }
        }
    }

    public void chisingPlayer()
    {
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (ray.collider != null)
        {
            anim.SetBool("run", false);
            attackPlayer = true;
        }
        else
        {
            attackPlayer = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void AttackPlayer()
    {
        anim.SetTrigger("attack");
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
    }
}
