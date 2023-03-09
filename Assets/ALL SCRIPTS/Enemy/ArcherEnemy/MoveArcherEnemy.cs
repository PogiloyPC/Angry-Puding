using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArcherEnemy : MonoBehaviour
{
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform player;
    [SerializeField] private Transform attackPos;
    private Animator anim;
    private float rotateZ;
    public float speed;
    public float distanceTowardsPlayer;
    public float distanceEscape;
    public float minDistanceAttack;
    public float maxDistanceAttack;
    public bool patrol;
    private bool moveTowardPoints;    
    [Header("bullet")]
    [SerializeField] private GameObject bullet;    
    [Header("TimerAttack")]
    public float startAttackTimer;
    private float finishAttackTimer;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {        
        rotateZ = Mathf.Atan2(player.position.y + 0.2f - attackPos.position.y, player.position.x + 0.2f- attackPos.position.x) * Mathf.Rad2Deg;
        attackPos.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        if (Vector2.Distance(transform.position, player.position) < distanceTowardsPlayer && Vector2.Distance(transform.position, player.position) > maxDistanceAttack)
        {
            ChisingPlayer();
        }
        else if (Vector2.Distance(transform.position, player.position) < distanceEscape && Vector2.Distance(transform.position, player.position) < minDistanceAttack)
        {
            EscapePlayer();
        }
        else if (Vector2.Distance(transform.position, player.position) <= maxDistanceAttack && Vector2.Distance(transform.position, player.position) >= minDistanceAttack)
        {
            Attack();
        }
        else
        {
            if (patrol == true)
            {
                Patrol();
            }
        }
        if (finishAttackTimer >= 0)
        {
            finishAttackTimer -= 1f * Time.deltaTime;
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

    public void ChisingPlayer()
    {        
        anim.SetBool("run", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void EscapePlayer()
    {
        anim.SetBool("run", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void Attack()
    {
        anim.SetBool("run", false);
        if (finishAttackTimer < 0)
        {
            anim.SetTrigger("attack");    
            if (transform.position.x < player.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                attackPos.position = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.35f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                attackPos.position = new Vector2(transform.position.x - 0.2f, transform.position.y + 0.35f);
            }
            finishAttackTimer = startAttackTimer;
        }
    }

    public void CreateArrow()
    {
        Instantiate(bullet, attackPos.position, Quaternion.Euler(0f, 0f, rotateZ));       
    }
}
