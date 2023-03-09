using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemy : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        Destroy(gameObject, 3f);
        anim = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Health player = collision.collider.gameObject.GetComponent<Health>();
        if (player != null)
        {
            anim.SetBool("instantiate", true);
        }
    }


}
