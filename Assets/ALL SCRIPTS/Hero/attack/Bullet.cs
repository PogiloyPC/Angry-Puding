using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D body;
    public int damage;
   
    private void Start()
    {
        player = GameObject.Find("Hero");
        body = GetComponent<Rigidbody2D>();       
    }    

    public void TakeImpulse(float impulse)
    {
        if (player.transform.localScale == new Vector3(1, 1, 1))
        {
            body.AddForce(transform.right * impulse, ForceMode2D.Impulse);
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (player.transform.localScale == new Vector3(-1, 1, 1))
        {
            body.AddForce(transform.right * -impulse, ForceMode2D.Impulse);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        HealthEnemy enemy = collision.collider.gameObject.GetComponent<HealthEnemy>();
        if (enemy != null)
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
        if (collision.collider.gameObject.tag == "ground")
        {
            damage = 0;
        }
    }
}
