using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDNK : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] BoxCollider2D box;
    

    //Vector2 offset = new Vector2(1f, 0f);

    void Start()
    {            
        StartCoroutine(DNK());
    }

    
    void Update()
    {
    }

    IEnumerator DNK()
    {
        
        while(true)
        {
            transform.position = new Vector2(transform.position.x + 0.3f , transform.position.y);
            
            Instantiate(obj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0f);
            
        }
    }

   
}
