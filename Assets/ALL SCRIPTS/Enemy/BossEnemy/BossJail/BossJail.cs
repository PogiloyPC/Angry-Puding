using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJail : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private GameObject player;
    public bool movingEnemy;
    public float speed;
    public float impulse;
    [Header("Actions")]
    [SerializeField] private Transform movePos;
    public float startTimerChangeAction;
    private float finishTimerChangeActions;
    private int numberActions;
    private float posX;
    private float timeChangePoint;
    [Header("Rock")]
    [SerializeField] private GameObject[] rocks;
    [SerializeField] private Transform posRock;
    [SerializeField] private Transform shotPoint;
    private float rotateZ;
    [Header("TimerAttack")]
    public float startTimerAttack;
    private float finishTimerAttack;
    [Header("CheckPlayer")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [Header("Jump")]
    [SerializeField] private Transform[] platforms;
    [SerializeField] private Transform jumpPos;
    private Collider2D checkGround;
    public bool checkGrounded;
    public float radiusCircle;
    public int numPlat;
    public float shootDelay;
    public float shotAngleInDeg;   
    public Vector2 shotDirection;
    public Vector2 shotDirectionX;
    [SerializeField] public GameObject projectile;
    Vector2 posRockAttack;

    void Start()
    {
        posRockAttack = new Vector2(transform.position.x, transform.position.y + 0.4f);
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Hero");
        ChangePoint();
    }

    void Update()
    {
        checkGrounded = Physics2D.OverlapCircle(jumpPos.position, radiusCircle, LayerMask.GetMask("Ground"));
        ray = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z),
        0, Vector2.left, 0);                    
        if (movingEnemy == true)
        {
            AllActions();
            if (finishTimerAttack >= 0)
            {
                finishTimerAttack -= 1f * Time.deltaTime;
            }
            if (finishTimerChangeActions >= 0)
            {
                finishTimerChangeActions -= 1f * Time.deltaTime;
            }
            else
            {
                PickActions();
                finishTimerChangeActions = startTimerChangeAction;
            }            
        }
        if (transform.position.x < player.transform.position.x)
        {
            posRock.rotation = Quaternion.Euler(0f, 0f, 75f);
            shotAngleInDeg = 55f;
        }
        else
        {
            posRock.rotation = Quaternion.Euler(0f, 0f, 115f);
            shotAngleInDeg = 135f;
        }
    }

    public void AllActions()
    {
        
        if (numberActions == 1)
        {
            MoveTowardPlayer();
        }
        else if (numberActions == 2)
        {
            WaitPlayer();
        }
        else if (numberActions == 3 && player.transform.position.y < posRockAttack.y)
        {            
            Rock rock = ray.collider.gameObject.GetComponent<Rock>();
            if (rock != null)
            {
                rock.transform.position = posRock.position;
                anim.SetBool("run", false);
                anim.SetTrigger("dropedrock");
            }
            else
            {
                TakeRock();
            }
        }
        else if (numberActions == 3 && player.transform.position.y >= posRockAttack.y)
        {
            JumpPerPlatform();
        }
    }

    public void PickActions()
    {
        numberActions = Random.Range(0, 4);
    }

    public void MoveTowardPlayer()
    {
        Health healthPlayer = ray.collider.gameObject.GetComponent<Health>();
        if (healthPlayer != null)
        {
            anim.SetBool("run", false);
            if (finishTimerAttack < 0)
            {
                AttackDefolt();
                finishTimerAttack = startTimerAttack;
            }
        }
        else
        {
            anim.SetBool("run", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1.1414f, 1.1414f, 1.1414f);
        }
        else
        {
            transform.localScale = new Vector3(1.1414f, 1.1414f, 1.1414f);
        }
    }

    public void WaitPlayer()
    {
        Vector3 pos = new Vector3(posX, transform.position.y);      
        if (transform.position.x != pos.x)
        {
            anim.SetBool("run", true);            
            transform.position = Vector2.MoveTowards(transform.position, pos, (speed / 2) * Time.deltaTime);               
        }   
        else
        {
            anim.SetBool("run", false);
            ChangePoint();
            //Invoke("ChangePoint", timeChangePoint);
        }
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1.1414f, 1.1414f, 1.1414f);
        }
        else
        {
            transform.localScale = new Vector3(1.1414f, 1.1414f, 1.1414f);
        }
    }

    public void TakeRock()
    {
        anim.SetBool("run", true);
        transform.position = Vector2.MoveTowards(transform.position, rocks[0].transform.position, speed * Time.deltaTime);        
    }

    public void DropedRock()
    {        
        Rigidbody2D rbRock = rocks[0].GetComponent<Rigidbody2D>();
        float g = 9.8f;
        shotDirection = player.transform.position - posRock.transform.position;
        shotDirectionX = new Vector2(shotDirection.x, 0f);
        float x = shotDirectionX.magnitude;
        float y = shotDirection.y;
        float shotAngleInRad = shotAngleInDeg * Mathf.PI / 180;
        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(shotAngleInRad) * x) * Mathf.Pow(Mathf.Cos(shotAngleInRad), 2));
        float v = Mathf.Sqrt(v2);                
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1.1414f, 1.1414f, 1.1414f);
            rbRock.velocity = posRock.transform.right * v;                        
        }
        else
        {
            transform.localScale = new Vector3(-1.1414f, 1.1414f, 1.1414f);
            rbRock.velocity = posRock.transform.right * -v;                       
        }      
        numberActions = 2;
    }

    public void PickPlatforms()
    {
        numPlat = Random.Range(0, 5);
    }

    public void JumpPerPlatform()
    {
        float g = 9.8f;
        shotDirection = player.transform.position - transform.position;
        shotDirectionX = new Vector2(shotDirection.x, 0f);
        float x = shotDirectionX.magnitude;
        float y = shotDirection.y;
        float shotAngleInRad = shotAngleInDeg * Mathf.PI / 180;
        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(shotAngleInRad) * x) * Mathf.Pow(Mathf.Cos(shotAngleInRad), 2));
        float v = Mathf.Sqrt(v2);
        if (checkGrounded == true)
        {
            body.velocity = posRock.transform.right * v * 1.5f;            
        }       
    }

    public void ChangePoint()
    {
        posX = Random.Range(movePos.position.x + -0.5f, movePos.position.x + 0.5f);
        //timeChangePoint = Random.Range(2, 3);
    }

    public void AttackDefolt()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
        Gizmos.DrawWireSphere(jumpPos.position, radiusCircle);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Health playerHealth = col.collider.gameObject.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1f);
        }
    }
}
