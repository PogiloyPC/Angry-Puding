using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJailActive : MonoBehaviour
{
    [SerializeField] private BossJail bossJail;
    [SerializeField] private GameObject plat1;
    [SerializeField] private GameObject plat2;

    private void Start()
    {
        plat1.SetActive(false);
        plat2.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            plat1.SetActive(true);
            plat2.SetActive(true);
            bossJail.movingEnemy = true;
            Destroy(this.gameObject, 2f);
        }
    }
}
