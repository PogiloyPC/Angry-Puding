using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPMouse : MonoBehaviour
{
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            body.transform.position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);
        }
        
    }

    
}
