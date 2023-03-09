using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public int damage;
       
    public void OnCollisionEnter2D(Collision2D collis)
    {
        HealthEnemy health = collis.collider.gameObject.GetComponent<HealthEnemy>();
        if (health != null)
        {
            if (health.currentArmor != 0)
            {
                health.TakeDamageArmor(damage);
            }
            else
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
