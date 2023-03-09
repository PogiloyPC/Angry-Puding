using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWildEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject explodeParticle;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private GameObject enemy;
    private Animator anim;
    private Vector2 posExplode;
    public float distanceChising;
    public float distanceExplode;
    public float speed;
    public bool patrol;
    private bool movePoints;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < distanceChising)
        {
            posExplode = new Vector2(transform.position.x, transform.position.y);
            ChisingPlayer();
        }
        else
        {
            if (patrol == true)
            {
                Patrol();
            }
        }
    }

    public void Patrol()
    {
        if (movePoints == false)
        {
            if (transform.position.x < rightPoint.position.x)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                movePoints = true;
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
                movePoints = false;
            }
        }
    }

    public void ChisingPlayer()
    {        
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);            
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.position) < distanceExplode)
        {
            anim.Play("explode");
            speed = 0f;
        }
        else
        {
            speed = 2.5f;
        }
    }

    public void Explode()
    {
        Instantiate(explodeParticle, posExplode + offset, Quaternion.identity);
        Destroy(enemy);
    }
}
