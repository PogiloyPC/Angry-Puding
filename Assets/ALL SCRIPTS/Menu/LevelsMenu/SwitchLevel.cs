using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Transform levels;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject leftButton;
    public int idLevels;
    
    void Start()
    {
        leftButton.SetActive(false);
    }

    void Update()
    {
        
    }

    public void SumIdLevel(int id)
    {
        idLevels = Mathf.Clamp(idLevels + id, 0, 5);
    }

    public void SwitchRightLevel()
    {
        SumIdLevel(1);
        OnLevels(idLevels);
    }

    public void SwithcLeftLevels()
    {
        SumIdLevel(-1);
        OnLevels(idLevels);
    }

    public void OnLevels(int numberLevel)
    {
        for (int i = 0; i < levels.childCount; i++)
        {
            Transform level = levels.GetChild(i);
            if (numberLevel == i)
            {
                level.gameObject.SetActive(true);
            }
            else
            {
                level.gameObject.SetActive(false);
            }
            if (numberLevel == 5)
            {
                rightButton.SetActive(false);
                leftButton.SetActive(true);
            }
            else if (numberLevel == 0)
            {
                rightButton.SetActive(true);
                leftButton.SetActive(false);
            }
            else
            {
                rightButton.SetActive(true);
                leftButton.SetActive(true);
            }
        }
    }

    public void BackMenu()
    {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
