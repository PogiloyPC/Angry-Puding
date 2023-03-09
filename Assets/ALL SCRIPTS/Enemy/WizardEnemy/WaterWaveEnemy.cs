using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveEnemy : MonoBehaviour
{
    private Rigidbody2D body;
    public float impulseBullet;

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
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
