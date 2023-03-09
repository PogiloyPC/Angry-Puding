using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INVENTORISYSTEM : MonoBehaviour
{
    public List<DataItems> items = new List<DataItems>();
    [SerializeField] Transform cells;
    [SerializeField] Transform attackPos;
    [SerializeField] GameObject inv;
    [SerializeField] GameObject button;
    [SerializeField] RectTransform ceLLS;
    public float distance;

    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].CountItem = 0;
        }
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.H))
        {
           // Attack(items);
        }
    }

    public void Attack(DataItems item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].CountItem != 0)
            {
                AddItem(items[i]);
                Instantiate(item.ObjItem, attackPos.position, Quaternion.identity);
                items[i].CountItem--;
                break;
            }
        }
    }

    public void AddItem(DataItems item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Transform cell = cells.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform count = icon.GetChild(0);
            Image img = icon.GetComponent<Image>();
            Text txt = count.GetComponent<Text>();
            if (img.enabled == true && img.sprite == item.IconItem)
            {                
                txt.enabled = true;
                txt.text = item.CountItem.ToString();
            }
            else if (img.enabled == false)
            {
                txt.enabled = true;
                txt.text = item.CountItem.ToString();
                img.enabled = true;
                img.sprite = item.IconItem;                
                break;
            }
            
        }
    }

    public void OnMouseDown()
    {           
    }

    public void ActiveInventori()
    {
        inv.SetActive(!inv.activeSelf);
        if (!inv.activeSelf)
        {
            button.transform.position = new Vector2(button.transform.position.x - distance, button.transform.position.y);
        }
        else
            button.transform.position = new Vector2(button.transform.position.x + distance, button.transform.position.y);
    }
}
