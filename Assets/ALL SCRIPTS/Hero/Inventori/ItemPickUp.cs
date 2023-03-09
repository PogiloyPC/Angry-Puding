using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public int idCell;
    [SerializeField] private Inventori inv;

    public void IDCell()
    {
        inv.ActivePickItem(idCell);
    }

}

