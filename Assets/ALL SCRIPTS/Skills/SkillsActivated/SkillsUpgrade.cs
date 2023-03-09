using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUpgrade : MonoBehaviour
{
    [SerializeField] public GameObject skillsPanel;
    [SerializeField] public Transform skillsLines;
    public bool snap;
    
    void Start()
    {
        skillsPanel.SetActive(false);   
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && snap == true)
        {
            ActiveSkillsPanel();
        }
    }

    public void ActiveSkillsPanel()
    {
        if (skillsPanel.activeSelf == false)
        {
            skillsPanel.SetActive(true);
            Time.timeScale = 0.5f;
        }
        else
        {
            skillsPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ActiveSkillLine(Sprite imgSkill)
    {
        for (int i = 0; i < skillsLines.childCount; i++)
        {
            Transform skillLine = skillsLines.GetChild(i);
            Transform skillImage = skillLine.GetChild(0);        
            Image imgSkl = skillImage.GetComponent<Image>();
            if (imgSkl.sprite == null)
            {               
                imgSkl.enabled = true;
                imgSkl.sprite = imgSkill;                
            }
        }
    }    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Checkpoint check = collision.gameObject.GetComponent<Checkpoint>();
        if (check != null && check.enabled == true)
        {
            snap = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Checkpoint check = collision.gameObject.GetComponent<Checkpoint>();
        if (check != null && check.enabled == true)
        {
            snap = false;
            Time.timeScale = 1f;
            skillsPanel.SetActive(false);
        }
    }
}
