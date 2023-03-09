using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : MonoBehaviour
{
    public DataItems ItemItm;
    private string nameItem { get { return ItemItm.NameItem; } }
    private int idItem { get { return ItemItm.IdItem; } }
    private Sprite iconItem { get { return ItemItm.IconItem; } }
    private GameObject objItem { get { return ItemItm.ObjItem; } }
    private int countItem { get { return ItemItm.CountItem; } }
    private bool IsstakingItem { get { return ItemItm.IsStakingItem; } }    
}
