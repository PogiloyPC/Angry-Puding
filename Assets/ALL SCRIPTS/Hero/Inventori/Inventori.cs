using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventori : MonoBehaviour
{
    public List<DataItems> items = new List<DataItems>();
    [SerializeField] private Transform checkItemPoint;
    [SerializeField] public Transform cells;
    [SerializeField] private Transform attackPos;   
    [SerializeField] public GameObject inv;
    [SerializeField] public GameObject button;    
    [SerializeField] Transform player;
    [SerializeField] private GameObject takeItemButton;
    public float impulse;
    public float distance;
    private int idCell = 0;
    [Header("CheckItem")]
    public float radiusCircle;
    public bool itemCheck;
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    public float disttance;

    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {            
            items[i].CountItem = 0;
        }
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform activeItemSlot = iconItem.GetChild(0);
            Image imgActiveItemSlot = activeItemSlot.GetComponent<Image>();
            ItemPickUp idCell = cell.GetComponent<ItemPickUp>();
            if (idCell.idCell == 0)
            {
                imgActiveItemSlot.enabled = true;
            }
            else
            {
                imgActiveItemSlot.enabled = false;
            }
        }
    }

    void Update()
    {
        itemCheck = Physics2D.OverlapCircle(checkItemPoint.position, radiusCircle, LayerMask.GetMask("Item"));
        ray = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z),
        0, Vector2.left, 0, LayerMask.GetMask("Item"));
        if (itemCheck == true)
        {
            takeItemButton.transform.position = new Vector2(ray.collider.gameObject.transform.position.x, ray.collider.gameObject.transform.position.y + 0.5f);
            takeItemButton.SetActive(true);           
            if (Input.GetKeyDown(KeyCode.K))
            {
                PickUpItem();
            }
        }
        else
        {
            takeItemButton.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AttackItem();
        }  
        if (Input.GetKeyDown(KeyCode.J))
        {
            CreateItem();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IdCell(-1);
            SwapSlotItemLeft();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            IdCell(1);
            SwapSlotItemRight();
        }
        Collider2D check = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + disttance), radiusCircle, LayerMask.GetMask("Checkpoint"));
        if (Input.GetKeyDown(KeyCode.T) && check != null)
        {
            if (!takeCheck)
            {
                takeCheck = true;
                //this.gameObject.GetComponent<HingeJoint2D>().connectedBody = check.GetComponent<Rigidbody2D>();
            }
            else
            {
                //this.gameObject.GetComponent<HingeJoint>().connectedBody = null;
                takeCheck = false;
            }
        }
        if (takeCheck == true)
        {
            check.transform.position = new Vector2(transform.position.x, transform.position.y + disttance * 2f);
        }
    }  
    public bool takeCheck;

    public void PickUpItem()
    {                
        if (ray.collider.gameObject.GetComponent<Item1>())
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i] = ray.collider.gameObject.GetComponent<Item1>().ItemItm;
                if (items[i].IdItem != 0)
                {
                    if (items[i].CountItem < 1)
                    {
                        items[i].CountItem++;
                        AddItem(items[i] = ray.collider.GetComponent<Item1>().ItemItm, ray);
                    }
                    else if (items[i].CountItem >= 1)
                    {
                        items[i].CountItem++;
                        AddCount(items[i] = ray.collider.GetComponent<Item1>().ItemItm, ray);
                    }
                    break;
                }
            }
            Debug.Log("item");
        }
    }

    public void IdCell(int side)
    {
        idCell = Mathf.Clamp(idCell + side, 0, 5);
    }

    public void SwapSlotItemLeft()
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform itemIcon = cell.GetChild(0);
            Transform activeItemSlot = itemIcon.GetChild(0);
            Image imgActiveItemSlot = activeItemSlot.GetComponent<Image>();
            ItemPickUp cellId = activeItemSlot.GetComponent<ItemPickUp>();
            if (idCell == i)
            {
                imgActiveItemSlot.enabled = true;
            }          
            else
            {
                imgActiveItemSlot.enabled = false;
            }
        }
    }

    public void SwapSlotItemRight()
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform itemIcon = cell.GetChild(0);
            Transform activeItemSlot = itemIcon.GetChild(0);
            Image imgActiveItemSlot = activeItemSlot.GetComponent<Image>();
            ItemPickUp cellId = activeItemSlot.GetComponent<ItemPickUp>();
            if (idCell == i)
            {
                imgActiveItemSlot.enabled = true;
            }
            else
            {
                imgActiveItemSlot.enabled = false;
            }
        }
    }

    public void AttackItem()
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform iconActiveSlot = iconItem.GetChild(0);
            Image imgItem = iconItem.GetComponent<Image>();
            Image imgActiveSlot = iconActiveSlot.GetComponent<Image>();
            Item1 item = cell.GetComponent<Item1>();
            if (item.ItemItm.CountItem != 0 && imgActiveSlot.enabled == true && imgItem.sprite == item.ItemItm.IconItem)
            {
                GameObject _item = Instantiate(item.ItemItm.ObjItem, attackPos.position, Quaternion.identity);
                Rigidbody2D bullet = _item.GetComponent<Rigidbody2D>();
                if (transform.localScale == new Vector3(1f, 1f, 1f))
                {
                    bullet.AddForce(transform.right * impulse, ForceMode2D.Impulse);
                }
                else if (transform.localScale == new Vector3(-1f, 1f, 1f))
                {
                    bullet.AddForce(transform.right * -impulse, ForceMode2D.Impulse);
                }
                item.ItemItm.CountItem--;
                if (item.ItemItm.CountItem >= 1)
                {
                    RemoveItem(item.ItemItm);
                }
                else if (item.ItemItm.CountItem < 1)
                {
                    DeleteItemInInventori(item.ItemItm);
                }
                break;
            }
        }
    }

    public void CreateItem()
    {
        for(int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform activeItemSlot = iconItem.GetChild(0);
            Item1 item = cell.GetComponent<Item1>();
            Image imgActiveItemSlot = activeItemSlot.GetComponent<Image>();
            if (item.ItemItm.CountItem != 0 && imgActiveItemSlot.enabled == true)
            {
                Instantiate(item.ItemItm.ObjItem, attackPos.position, Quaternion.identity);                
                item.ItemItm.CountItem -= 1;
                if (item.ItemItm.CountItem >= 1)
                {
                    RemoveItem(item.ItemItm);
                }
                else if (item.ItemItm.CountItem < 1)
                {
                    DeleteItemInInventori(item.ItemItm);
                }
                break;
            }
        }
    }

    public void ActivePickItem(int idCell)
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform iconActiveSlot = iconItem.GetChild(0);
            Image imgItem = iconItem.GetComponent<Image>();
            Image imgActiveSlot = iconActiveSlot.GetComponent<Image>();            
            if (imgItem.enabled == true && i == idCell)
            {
                imgActiveSlot.enabled = true;                
            }
            else
            {
                imgActiveSlot.enabled = false;
            }
        }
    }

    public void RemoveItem(DataItems item)
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform iconActiveSlot = iconItem.GetChild(0);
            Transform count = iconActiveSlot.GetChild(0);
            Image imgItem = iconItem.GetComponent<Image>();
            Image imgActiveSlot = iconActiveSlot.GetComponent<Image>();
            Text txt = count.GetComponent<Text>();
            if (imgItem.enabled == true && imgItem.sprite == item.IconItem)
            {
                txt.text = item.CountItem.ToString();
                break;
            }            
        }
    }

    public void DeleteItemInInventori(DataItems item)
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform iconActiveSlot = iconItem.GetChild(0);
            Transform count = iconActiveSlot.GetChild(0);
            Image imgItem = iconItem.GetComponent<Image>();
            Image imgActiveSlot = iconActiveSlot.GetComponent<Image>();
            Text txt = count.GetComponent<Text>();
            Item1 itemPickUp = cell.GetComponent<Item1>();
            ItemPickUp activeSlotId = cell.GetComponent<ItemPickUp>();
            if (item.CountItem == 0 && imgActiveSlot.enabled == true && imgItem.enabled == true)
            {
                itemPickUp.ItemItm = item;
                imgItem.enabled = false;
                txt.enabled = false;
                //ActiveItemSlot(activeSlotId.idCell);
                break;
            }
        }
    }

    public void ActiveItemSlot(int idCell)
    {        
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform activeItemSlot = iconItem.GetChild(0);
            Image imgActiveItemSlot = activeItemSlot.GetComponent<Image>();
            Image imgIconItem = iconItem.GetComponent<Image>();
            ItemPickUp itemSlot = cell.GetComponent<ItemPickUp>();            
        }
    }

    public void AddCount(DataItems item, RaycastHit2D ray)
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform iconActiveSlot = iconItem.GetChild(0);
            Transform count = iconActiveSlot.GetChild(0);
            Image imgItem = iconItem.GetComponent<Image>();
            Image imgActiveSlot = iconActiveSlot.GetComponent<Image>();
            Text txt = count.GetComponent<Text>();
            if (imgItem.enabled == true && imgItem.sprite == item.IconItem)
            {
                txt.enabled = true;
                txt.text = item.CountItem.ToString();
                Destroy(ray.collider.gameObject);
                break;
            }
        }
    }

    public void AddItem(DataItems item, RaycastHit2D ray)
    {                
        for (int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            Transform iconItem = cell.GetChild(0);
            Transform iconActiveSlot = iconItem.GetChild(0);
            Transform count = iconActiveSlot.GetChild(0);
            Image imgItem = iconItem.GetComponent<Image>();
            Image imgActiveSlot = iconActiveSlot.GetComponent<Image>();
            Text txt = count.GetComponent<Text>();
            Item1 itemPickUp = cell.GetComponent<Item1>();
            if (item.CountItem >= 1 && imgItem.enabled == false)
            {
                itemPickUp.ItemItm = item;
                imgItem.enabled = true;
                imgItem.sprite = item.IconItem;
                txt.enabled = true;
                txt.text = item.CountItem.ToString();
                Destroy(ray.collider.gameObject);
                break;
            }            
        }
    }   

    public void ActiveInventori()
    {
        inv.SetActive(!inv.activeSelf);
        if (!inv.activeSelf)
        {
            button.transform.position = new Vector2(button.transform.position.x - distance, button.transform.position.y);
            Time.timeScale = 1f;
        }
        else
        {
            button.transform.position = new Vector2(button.transform.position.x + distance, button.transform.position.y);
            Time.timeScale = 0.5f;
        }
    }  
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
        Gizmos.DrawWireSphere(checkItemPoint.position, radiusCircle);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + disttance), radiusCircle);
    }
}