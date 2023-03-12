using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{    
    public float speed;
    public float defoltSpeed;
    public float jump;
    public float defoltJump; 
    public bool grounded;
    public bool movingHero;
    [Header("CheckGround")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    public Animator anim;
    Rigidbody2D body;

    void Awake()
    {
        defoltSpeed = speed;
        defoltJump = jump;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {                
        StatePlayer();   
    }

    private void FixedUpdate()
    {
        ray = Physics2D.BoxCast(boxCollider.bounds.center + transform.up * rayDistanceY * transform.localScale.y * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z),
        0, Vector2.left, 0, LayerMask.GetMask("Ground"));
        MovingPlayer();
    }

    public void StatePlayer()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("moving", true);
            transform.localScale = new Vector3(1, 1, 1);
            movingHero = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movingHero = false;
            anim.SetBool("moving", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("moving", true);
            movingHero = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            movingHero = false;
            anim.SetBool("moving", false);
        }
        if (Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            JUmp();
        }
    }

    public void MovingPlayer()
    {
        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector3(speed, body.velocity.y);                                   
        }        
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector3(-speed, body.velocity.y);            
        }                
        if (ray.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public void JUmp()
    {
        body.velocity = new Vector2(body.velocity.x, jump);
        anim.SetTrigger("jump");
    }   

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.up * rayDistanceY * transform.localScale.y * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
    }
}
