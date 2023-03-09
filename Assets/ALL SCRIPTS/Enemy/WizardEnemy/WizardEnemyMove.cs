using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyMove : MonoBehaviour
{
    [SerializeField] public Transform rightPoint;
    [SerializeField] public Transform leftPoint;
    [SerializeField] public Transform player;
    [SerializeField] private Transform startPos;    
    private Animator anim;
    private float rotateZ;
    public bool patrol;
    private bool moveTowardPoints;
    public float speed;
    public float distanceTowardsPlayer;
    public float maxDistanceAttack;
    public float minDistanceAttack;
    public float distanceEscape;
    [Header("Attack")]
    [SerializeField] private Transform attackPos;
    public float startAttack;
    private float finishAttack;
    public float impulseBullet;
    [Header("IceAttack")]
    [SerializeField] private GameObject ice;
    public float startTimerFreeze;
    private float finishTimerFreeze;    
    [Header("FirebollAttack")]
    [SerializeField] private GameObject fireboll;
    [Header("WaterWaveAttack")]
    [SerializeField] private GameObject waterWave;

    void Start()
    {        
        startPos.position = transform.position;
        anim = GetComponent<Animator>();        
    }
  
    void Update()
    {
        rotateZ = Mathf.Atan2(player.position.y + 0.4f - attackPos.position.y, player.position.x + 0.4f - attackPos.position.x) * Mathf.Rad2Deg;
        attackPos.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        if (Vector2.Distance(transform.position, player.transform.position) < distanceTowardsPlayer && Vector2.Distance(transform.position, player.transform.position) > maxDistanceAttack)
        {
            ChisingPlayer();
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < distanceEscape && Vector2.Distance(transform.position, player.transform.position) < minDistanceAttack)
        {
            EscapePlayer();
        }
        else if (Vector2.Distance(transform.position, player.transform.position) <= maxDistanceAttack && Vector2.Distance(transform.position, player.transform.position) >= minDistanceAttack)
        {
            Attack();
        }
        else
        {
            if (patrol == true)
            {
                Patrol();
            }
            else
            {
                MoveStartPosition();
            }
        }
        if (finishTimerFreeze >= 0f)
        {
            finishTimerFreeze -= 1f * Time.deltaTime;
        }
        else
        {
            //Animator animIce = ice.GetComponent<Animator>();
            //animIce.SetTrigger("destroy");
            moving movePlayer = player.GetComponent<moving>();
            movePlayer.speed = movePlayer.defoltSpeed;
            movePlayer.jump = movePlayer.defoltJump;
        }
        if (finishAttack >= 0)
        {
            finishAttack -= 1f * Time.deltaTime;
            Debug.Log(finishAttack);
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

    public void MoveStartPosition()
    {
        if (transform.position.x != startPos.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
            anim.SetBool("run", true);
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
        anim.SetBool("run", true);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }   
    
    public void EscapePlayer()
    {
        anim.SetBool("run", true);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void Attack()
    {

        anim.SetBool("run", false);
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (finishAttack < 0)
        {
            attackPos.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
            int animFigures = UnityEngine.Random.Range(1, 4);
            anim.Play(animFigures + "attack");
            finishAttack = startAttack;
        }
    }

    public void Fireboll()
    {
        Instantiate(fireboll, attackPos.position, Quaternion.Euler(0f, 0f, rotateZ));        
    }

    public void WaterWave()
    {
        Instantiate(waterWave, attackPos.position, Quaternion.Euler(0f, 0f, rotateZ));        
    }

    public void Ice()
    {
        Instantiate(ice, player.transform.position, Quaternion.identity);
        //Animator animIce = ice.GetComponent<Animator>();
        //animIce.SetBool("instantiate", true);
        moving movePlayer = player.GetComponent<moving>();
        movePlayer.speed = 0f;
        movePlayer.jump = 0f;
        finishTimerFreeze = startTimerFreeze;
    }
}
