using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private GameObject player;
    public float impulse;
   
    void Start()
    {
        player = GameObject.Find("Hero");
        Destroy(gameObject, 1f); 
    }
   
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Health player = collision.gameObject.GetComponent<Health>();
        if (player != null)
        {
            Rigidbody2D bodyPlayer = this.player.GetComponent<Rigidbody2D>();
            if (transform.position.x < this.player.transform.position.x)
            {
                bodyPlayer.AddForce(transform.right * impulse, ForceMode2D.Impulse);
                bodyPlayer.AddForce(transform.up * impulse, ForceMode2D.Impulse);
            }
            else
            {
                bodyPlayer.AddForce(transform.right * -impulse, ForceMode2D.Impulse);
                bodyPlayer.AddForce(transform.up * impulse, ForceMode2D.Impulse);
            }
            player.TakeDamage(2f);
        }
    }
}
