using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trancheon : MonoBehaviour
{
    public int damage;    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Health player = collision.gameObject.GetComponent<Health>();        
        HealthEnemy enemy = collision.gameObject.GetComponent<HealthEnemy>();
        Item1 item = collision.gameObject.GetComponent<Item1>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        else if (enemy != null)
        {
            if (enemy.currentArmor != 0)
            {
                enemy.TakeDamageArmor(damage);
            }
            else
            {
                enemy.TakeDamage(damage);
            }
        }
        if (item != null)
        {
            Destroy(item.gameObject);
        }
    }
}
