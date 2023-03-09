using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileEnemyMove : MonoBehaviour
{
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPos;
    private Animator anim;
    public float chisingPlayerDistance;
    public float speed;
    public bool patrol;
    private bool moveTowardPoints;
    [Header("AttackPlayer")]
    public float damage;
    public float startTimerAttack;
    private float finishTimerAttack;
    [Header("CheckPlayer")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;    

    void Start()
    {
        startPos.position = transform.position;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        ray = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z),
        0, Vector2.left, 0);
        if (Vector2.Distance(transform.position, player.position) < chisingPlayerDistance)
        {
            ChisingPlayer();
        }
        else
        {
            if (patrol == true)
            {
                Patrol();
            }
            else
            {
                MoveStartPos();
            }
        }
        if (finishTimerAttack >= 0f)
        {
            finishTimerAttack -= 1f * Time.deltaTime;
        }
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

    public void MoveStartPos()
    {
        if (transform.position.x != startPos.position.x)
        {
            anim.SetBool("run", true);
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
            if (transform.position.x < startPos.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }    
            else
            {
                transform.position = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    public void ChisingPlayer()
    {
        Health player = ray.collider.gameObject.GetComponent<Health>();
        if (player != null)
        {
            anim.SetBool("run", false);
            if (finishTimerAttack < 0f)
            {
                anim.SetTrigger("attack");
                finishTimerAttack = startTimerAttack;
            }
        }
        else
        {
            anim.SetBool("run", true);
            transform.position = Vector2.MoveTowards(transform.position, this.player.position, speed * Time.deltaTime);
            if (transform.position.x < this.player.position.x)
            {            
                transform.localScale = new Vector3(1f, 1f, 1f);                
            }
            else
            {                
                transform.localScale = new Vector3(-1f, 1f, 1f);                
            }
        }
    }

    public void Attack()
    {
        Health player = ray.collider.gameObject.GetComponent<Health>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
    }
}
