using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttack : MonoBehaviour
{
    [SerializeField] public Transform player;   
    private Animator anim;
    private Rigidbody2D body;
    public float speed;
    public float distance;
    public float impulse;
    [Header("CheckPlayer")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [Header("CheckPlayer2")]
    private RaycastHit2D ray2;
    public float rayDistance2;
    public float rayDistanceY2;
    [SerializeField] private BoxCollider2D boxCollider2;
    [SerializeField] private float colliderDistance2;
    [Header("Attack")]
    public int damage;
    public float startAttackTimer;
    private float finishAttackTimer;

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
        ray2 = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rayDistance2 * transform.localScale.x * colliderDistance2,
        new Vector3(boxCollider.bounds.size.x * rayDistance2, boxCollider.bounds.size.y * rayDistanceY2, boxCollider.bounds.size.z),
        0, Vector2.left, 0, LayerMask.GetMask("Player"));
        if (finishAttackTimer >= 0f)
        {
            finishAttackTimer -= 1f * Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, player.position) < distance)
        {
            chisingPlayer();
            if (ray2.collider != null)
            {
                if (finishAttackTimer < 0f)
                {
                    if (transform.position.x > player.position.x)
                    {
                        body.AddForce(transform.right * -impulse, ForceMode2D.Impulse);
                        body.AddForce(transform.up * impulse, ForceMode2D.Impulse);
                    }
                    else
                    {
                        body.AddForce(transform.right * impulse, ForceMode2D.Impulse);
                        body.AddForce(transform.up * impulse, ForceMode2D.Impulse);
                    }
                    finishAttackTimer = startAttackTimer;
                }
            }
        }
        
    }   

    public void chisingPlayer()
    {
        Health player = ray.collider.gameObject.GetComponent<Health>();                
        if (transform.position.x < this.player.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (player != null)
        {
            
        }
        else
        {            
            transform.position = Vector2.MoveTowards(transform.position, this.player.position, speed * Time.deltaTime);
        }        
    }    

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
        Gizmos.DrawCube(boxCollider.bounds.center + transform.right * rayDistance2 * transform.localScale.x * colliderDistance2,
        new Vector3(boxCollider.bounds.size.x * rayDistance2, boxCollider.bounds.size.y * rayDistanceY2, boxCollider.bounds.size.z));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Health player = collision.collider.gameObject.GetComponent<Health>();
        if (player != null)
        {
            player.TakeDamage(damage);
            body.velocity = new Vector2(0f, 0f);
        }
    }
}
