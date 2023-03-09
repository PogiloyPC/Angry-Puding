using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    
    [SerializeField] float speedP;
    [SerializeField] Animator anim;
    
    Rigidbody2D box;
    Transform Player;

    

    public Vector2 offset = new Vector2(0f, 0f);


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        box = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetBool("touchidel", false);
        anim.SetBool("touch", false);
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z) && collision.gameObject.tag == "Player")
        {
           
            box.AddForce(transform.right * speedP);            
           
           
        }
        if (Input.GetKey(KeyCode.J) && collision.gameObject.tag == "player") 
        { 
       
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.J) && collision.gameObject.tag == "Player")
        {
            anim.SetBool("touch", true);
            transform.position = new Vector2(Player.position.x + offset.x, Player.position.y + offset.y);            
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.J) && collision.gameObject.tag == "Player")
        {
            anim.SetBool("touch", true);
            transform.position = new Vector2(Player.position.x - offset.x, Player.position.y + offset.y);
            
        }
    }
}
