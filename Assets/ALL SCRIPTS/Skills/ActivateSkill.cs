using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSkill : MonoBehaviour
{
    [SerializeField] public GameObject obj;
    [SerializeField] public Sprite imgSkills;
    private SkillsUpgrade skillUpgrade;    

    void Start()
    {
        skillUpgrade = GameObject.Find("Hero").GetComponent<SkillsUpgrade>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            obj.SetActive(true);
            skillUpgrade.ActiveSkillLine(imgSkills);
        }
    }
}
