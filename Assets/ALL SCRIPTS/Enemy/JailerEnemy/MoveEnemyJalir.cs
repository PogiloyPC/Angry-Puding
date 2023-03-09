using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyJalir : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private GameObject helmet;
    [SerializeField] private HealthEnemy health;
    private Animator anim;
    public float speed;
    public float distanceChising;
    public bool patrol;
    private bool moveTowardPoints;
    [Header("CheckPlayer")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [Header("AttackPlayer")]
    public float damage;
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
        0, Vector2.left, 0);
        if (finishAttackTimer >= 0f)
        {
            finishAttackTimer -= 1f * Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, player.position) < distanceChising)
        {
            ChisingPlayer();
            Health player = ray.collider.gameObject.GetComponent<Health>();
            if (player != null)
            {
                if (finishAttackTimer < 0f)
                {                    
                    AttackPlayer();
                    player.TakeDamage(damage);
                    finishAttackTimer = startAttackTimer;
                } 
            }
        }
        else
        {
            Patrol();
        }
        if (health.currentArmor <= 0)
        {
            Destroy(helmet);
        }
    }

    public void AttackPlayer()
    {
        anim.SetTrigger("attack");
    }

    public void Patrol()
    {
        anim.SetBool("run", true);
        if (moveTowardPoints == false)
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
        else
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
    }

    public void ChisingPlayer()
    {
        Health player = ray.collider.gameObject.GetComponent<Health>();
        if (player != null)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
            transform.position = Vector2.MoveTowards(transform.position, this.player.position, speed * Time.deltaTime);
            if (transform.position.x > this.player.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
    }
}
