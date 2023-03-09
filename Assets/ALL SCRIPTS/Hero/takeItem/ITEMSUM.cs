using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ITEMSUM : MonoBehaviour
{


    public float sum;
    bool Sitem;

    [SerializeField] Text txt;

    [SerializeField] Transform Player;

    attack attack;
    Vector3 size;

    void Start()
    {
        attack = GetComponent<attack>();

    }

    // Update is called once per frame
    void Update()
    {
        size = new Vector3(Player.localScale.x, Player.localScale.y, Player.localScale.z);

        txt.text = sum.ToString();

        if (Input.GetKeyDown(KeyCode.Y))
        {

            Physics2D.queriesStartInColliders = false;
            Collider2D touch = Physics2D.OverlapBox(transform.position, size / 5f, 0f, LayerMask.GetMask("Item"));
            
            if (touch != null)
            {
                sum++;
                Destroy(touch.gameObject);
            }

        }
        

    }
    public void TakeItem(float item)
    {
        sum = Mathf.Clamp(sum - item, 0f, 8f);
    }
}
