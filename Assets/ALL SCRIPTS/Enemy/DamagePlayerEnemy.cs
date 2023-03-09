using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerEnemy : MonoBehaviour
{
    [SerializeField] int damage;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
