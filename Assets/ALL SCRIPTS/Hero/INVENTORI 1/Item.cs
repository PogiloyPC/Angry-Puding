using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int itemid;
    public string itemName;
    public int itemCount;
    [SerializeField] public Sprite itemImg;
    [SerializeField] public GameObject itemObj;
}
