using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "item", menuName = "DataItem/item")]
public class DataItems : ScriptableObject
{
    [SerializeField] private string nameItem;
    [SerializeField] private int idItem;
    [SerializeField] private Sprite iconItem;
    [SerializeField] private GameObject objItem;
    [SerializeField] private int countItem;
    [SerializeField] private bool IsstakingItem;
    public string NameItem { get { return nameItem; } }
    public int IdItem { get { return idItem; } }
    public Sprite IconItem { get { return iconItem; } }
    public GameObject ObjItem { get { return objItem; } }
    public int CountItem { get; set; }
    public bool IsStakingItem { get { return IsstakingItem; } }
}
