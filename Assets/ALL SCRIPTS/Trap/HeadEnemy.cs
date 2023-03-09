using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnemy : MonoBehaviour
{
    [SerializeField] moving hero;
    [SerializeField] Rigidbody2D Hero;
    [SerializeField] GameObject enemy;
    [SerializeField] public int jumpHero; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (hero.grounded == false)
            {
                hero.anim.SetTrigger("jump");
                Hero.AddForce(Vector2.up * jumpHero);
                Destroy(enemy);
            }
        }
    }
}