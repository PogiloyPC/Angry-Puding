using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWave : MonoBehaviour
{
    public float speed;
    public float impulseBody;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Health player = collision.gameObject.GetComponent<Health>();
        Touch enemy = collision.gameObject.GetComponent<Touch>();
        RatAttack rat = collision.gameObject.GetComponent<RatAttack>();
        if (player != null)
        {
            Rigidbody2D bodyPlayer = player.GetComponent<Rigidbody2D>();
            bodyPlayer.AddForce(transform.right * impulseBody, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
        if (enemy != null)
        {
            Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>();
            enemyBody.AddForce(transform.right * impulseBody, ForceMode2D.Impulse);            
        }
        if (rat != null)
        {
            Destroy(rat.gameObject);
        }
    }
}
