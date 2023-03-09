using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigantEnemyMove : MonoBehaviour
{
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform player;
    [SerializeField] private Transform truncheon;
    private Animator anim;
    private Rigidbody2D body;
    public float speed;
    public bool patrol;
    private bool moveTowardPoints;
    public float chisingPlayerDistance;
    [Header("Attack")]
    public int damage;
    public float startTimerAttack;
    private float finishTimerAttack;
    [Header("CheckPlayer")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;    
    [Header("TremblingAttack")]
    [SerializeField] private GameObject rightWave;
    [SerializeField] private GameObject leftWave;
    [Header("JumpAttack")]
    public float impulseBody;
    private bool createWaves;
    [SerializeField] private GameObject rightWaveAttack;
    [SerializeField] private GameObject leftWaveAttack;
    public float impulseBodyPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
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
                MoveStartPoint();
            }
        }
        if (finishTimerAttack >= 0)
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

    public void MoveStartPoint()
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
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    public void ChisingPlayer()
    {
        moving player = ray.collider.gameObject.GetComponent<moving>();
        if (player != null)
        {
            anim.SetBool("run", false);
            if (finishTimerAttack < 0f)
            {
                Attack();
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
        int figuresAnim = UnityEngine.Random.Range(1, 4);
        anim.Play(figuresAnim + "attack");
    }

    public void DefoltAttack()
    {
        if (player.position.x < transform.position.x)
        {
            truncheon.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            truncheon.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }

    public void DefoltAttackFinish()
    {
        truncheon.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void TremblingAttack()
    {
        Instantiate(rightWave, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Instantiate(leftWave, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    public void JumpAttack()
    {
        body.AddForce(transform.up * impulseBody, ForceMode2D.Impulse);
    }

    public void RotateInAir()
    {
        if (transform.localScale == new Vector3(-1f, 1f, 1f))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        if (transform.localScale == new Vector3(1f, 1f, 1f))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        createWaves = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Health player = collision.collider.gameObject.GetComponent<Health>();
        if (player != null && createWaves == true)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
            if (transform.position.x < this.player.position.x)
            {
                playerBody.AddForce(transform.right * impulseBodyPlayer, ForceMode2D.Impulse);
            }
            else
            {
                playerBody.AddForce(transform.right * -impulseBodyPlayer, ForceMode2D.Impulse);
            }
            player.TakeDamage(damage);           
            createWaves = false;
        }
        if (collision.collider.gameObject.tag == "ground")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
