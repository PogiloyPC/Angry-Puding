using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteallItem : MonoBehaviour
{    
    [SerializeField] private Inventori stealItem;
    [SerializeField] private Transform spawnItem;
    private Item1 item;
    public bool takeItem;
    private bool steal;
    public float startTimerSteal;
    private float finishTimerSteal;
    [Header("CheckItem")]
    private RaycastHit2D ray;
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    void Update()
    {
        ray = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z),
        0, Vector2.left, 0);
        if (steal == true)
        {
            for (int i = 0; i < stealItem.cells.childCount; i++)
            {
                Transform cell = stealItem.cells.GetChild(i);
                Transform iconItem = cell.GetChild(0);
                Transform activeItemSlot = iconItem.GetChild(0);
                Image imgActiveItemSlot = activeItemSlot.GetComponent<Image>();
                Item1 item = cell.GetComponent<Item1>();
                if (item.ItemItm.CountItem != 0 && imgActiveItemSlot.enabled == true)
                {                    
                    Instantiate(item.ItemItm.ObjItem, spawnItem.position, Quaternion.identity);
                    item.ItemItm.CountItem--;
                    if (item.ItemItm.CountItem >= 1)
                    {
                        stealItem.RemoveItem(item.ItemItm);
                    }
                    else if (item.ItemItm.CountItem < 1)
                    {
                        stealItem.DeleteItemInInventori(item.ItemItm);
                    }                    
                }
            }           
        }
        item = ray.collider.gameObject.GetComponent<Item1>();
        if (item != null)
        {
            item.transform.position = spawnItem.position;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rayDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            steal = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            steal = false;
        }
    }
}
