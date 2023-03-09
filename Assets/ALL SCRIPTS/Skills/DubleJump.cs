using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DubleJump : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D body;    
    float timer = 1f;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && timer >= 1f)
        {           
            Jump();
            anim.SetTrigger("jump");
            timer = 0f;
        }
        if (timer < 1f)
        {
            timer += 1f * Time.deltaTime;
        }
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, 3);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
