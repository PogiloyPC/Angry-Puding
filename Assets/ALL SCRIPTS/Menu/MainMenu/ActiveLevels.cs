using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLevels : MonoBehaviour
{
    [SerializeField] private GameObject levelsMenu;

    void Start()
    {
        
    }
   
    void Update()
    {
        
    }

    public void SwithPerLevelsMenu()
    {
        SwitchLevel levelId = levelsMenu.GetComponent<SwitchLevel>();
        levelId.idLevels = 0;
        levelId.OnLevels(0);
        levelsMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
