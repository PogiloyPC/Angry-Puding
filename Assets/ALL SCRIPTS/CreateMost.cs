using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMost : MonoBehaviour
{
    public int countObject;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform startCreateObjPos;

    void Start()
    {
        
    }
   
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (float i = 0; i < countObject; i++)
            {
                Instantiate(obj, new Vector2(startCreateObjPos.position.x + i/4, startCreateObjPos.position.y), Quaternion.identity);
            }
        }
    }
}
