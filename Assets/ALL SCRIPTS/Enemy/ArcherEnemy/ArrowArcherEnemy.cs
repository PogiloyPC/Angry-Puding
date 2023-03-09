using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowArcherEnemy : MonoBehaviour
{
    public float speed;
    public int damage;
    Rigidbody2D body;

    private void Start()
    {
        Destroy(gameObject, 3f);
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }   

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Health player = collision.collider.gameObject.GetComponent<Health>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
