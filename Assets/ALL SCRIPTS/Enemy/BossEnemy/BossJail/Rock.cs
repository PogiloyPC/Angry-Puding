using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Transform bossJail;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();    
    }    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        moving player = collision.collider.gameObject.GetComponent<moving>();
        if (player != null)
        {
            body.velocity = new Vector2(0f, 0f);
        }       
    }
}
