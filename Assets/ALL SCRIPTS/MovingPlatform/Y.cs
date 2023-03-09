using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y : MonoBehaviour
{
    [SerializeField] Transform Downpoint;
    [SerializeField] Transform Toppoint;
    public float speed;
    bool move = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (move == false)
        {
            if (transform.position.y > Downpoint.position.y)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
            else
                move = true;
        }
        else
        {
            if (transform.position.y < Toppoint.position.y)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
                move = false;
        }
    }
}
