using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Health player = collision.gameObject.GetComponent<Health>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
