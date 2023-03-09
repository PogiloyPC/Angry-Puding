using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    [SerializeField] Y obj;

    bool touch;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && touch == true)
        {
            if (obj.enabled == false)
            {
                obj.enabled = true;
            }
            else
            {
                obj.enabled = false;
            }

        }
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touch = false;
        }
    }
}
