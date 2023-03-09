using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumDNK : MonoBehaviour
{
    int sum = 0;
    [SerializeField] Text DNK;

    void Start()
    {
        DNK.text = sum.ToString();        
    }

    // Update is called once per frame
    void Update()
    {
        DNK.text = sum.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DNK")
        {
            sum += 1;
            
        }
    }
}
