using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebollEnemy : MonoBehaviour
{
    public float damage;
    public float impulseBullet;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * impulseBullet, ForceMode2D.Impulse);
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
