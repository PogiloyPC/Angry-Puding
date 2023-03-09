using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSkill : MonoBehaviour
{
    [SerializeField] private GameObject lineSkill;
    [SerializeField] private GameObject lineSkill1;
    [SerializeField] private Text priceGenesImage;
    private GameObject player;
    public int priceGenes;
    public int priceGenes2;

    void Start()
    {
        lineSkill.SetActive(false);
        player = GameObject.Find("Hero");
    }
    
    void Update()
    {
        
    }

    public void UpgradeSkill()
    {
        ManagerPlayer genes = player.GetComponent<ManagerPlayer>();
        if (genes.genes >= priceGenes && lineSkill.activeSelf == false)
        {
            lineSkill.SetActive(true);
            genes.CountGenes(priceGenes);            
        }
    }

    public void UpgradeSkill2()
    {
        ManagerPlayer genes = player.GetComponent<ManagerPlayer>();
        if (genes.genes >= priceGenes2 && lineSkill1.activeSelf == true && lineSkill.activeSelf == false)
        {
            lineSkill.SetActive(true);
            genes.CountGenes(priceGenes2);
        }
    }    
}
