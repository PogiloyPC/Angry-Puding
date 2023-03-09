using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPlayer : MonoBehaviour
{
    public int genes;
    [SerializeField] private Text countGenes;

    void Start()
    {
        countGenes.text = genes.ToString();
    }
    
    void Update()
    {
        
    }

    public void CountGenes(int price)
    {
        genes = Mathf.Clamp(genes - price, 0, 2000);
        countGenes.text = genes.ToString();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Gene")
        {
            genes += 1;
            countGenes.text = genes.ToString();
        }
    }
}
