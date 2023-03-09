using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Transform AttackPos;
    

    float item = 1f;

    ITEMSUM Isum;


    void Start()
    {
        Isum = GetComponent<ITEMSUM>();        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Instantiate(obj, AttackPos.position, Quaternion.identity);
            Isum.TakeItem(item);
        }
    }
}
