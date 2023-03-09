using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnenmy : MonoBehaviour
{
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform player;
    private HealthEnemy health;
    public float distance;
    public float speed;
    private bool leftMove;
    public float startTimerEscape;
    private float finishTimerEscape;
    private bool escapeFromThePlayer;
    private bool chisingPlayer = true;
    [Header("HeadPoint")]
    [SerializeField] private Transform headPoint;
    [SerializeField] private float radiusCircle;
    [SerializeField] private float impulseBodyPlayer;
    private bool checkPlayer;

    private void Start()
    {
        health = GetComponent<HealthEnemy>();
    }

    private void Update()
    {
        checkPlayer = Physics2D.OverlapCircle(headPoint.position, radiusCircle, LayerMask.GetMask("Player"));
        if (checkPlayer == true)
        {
            Death();           
        }
        AllState();
    }

    public void AllState()
    {
        if (escapeFromThePlayer == true)
        {
            if (finishTimerEscape >= 0)
            {
                finishTimerEscape -= 1f * Time.deltaTime;
                EscapeFromThePlayer();
                chisingPlayer = false;
            }
            else
            {
                escapeFromThePlayer = false;
                chisingPlayer = true;
            }
        }
        else if (Vector2.Distance(transform.position, player.position) < distance && chisingPlayer == true)
        {
            ChisingPlayer();
        }
        else
        {
            WalkEnemy();
        }
    }

    public void Death()
    {
        Rigidbody2D rbPlayer = player.GetComponent<Rigidbody2D>();
        if (health.currentArmor == 0)
        {
            rbPlayer.AddForce(transform.up * impulseBodyPlayer, ForceMode2D.Impulse);
            health.TakeDamage(1);
        }
        else
        {
            health.TakeDamageArmor(1);
        }
    }

    public void WalkEnemy()
    {
        if (leftMove == false)
        {
            if (transform.position.x > leftPoint.position.x)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
            else
                leftMove = true;
        }
        else
        {
            if (transform.position.x < rightPoint.position.x)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
                leftMove = false;
        }
    }

    public void ChisingPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 1.5f * Time.deltaTime);
    }

    public void EscapeFromThePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * 0.5f * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            finishTimerEscape = startTimerEscape;
            escapeFromThePlayer = true;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(headPoint.position, radiusCircle);
    }
}
