using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] GameObject StartDNK;

    



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DNKCHECK")
        {
            Destroy(StartDNK);
        }
        
            
    }

    
}
