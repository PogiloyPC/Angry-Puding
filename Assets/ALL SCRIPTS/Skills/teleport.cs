using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] Animator anim;

    public float TP;
    float timer = 5f;

    private void Update()
    {
        if (timer < 5f)
        {
            timer += 1f * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timer >= 5f)
            {
                if (body.transform.localScale == new Vector3(1, 1, 1))
                {
                    body.transform.position = new Vector2(body.transform.position.x + TP, body.transform.position.y);
                    anim.SetTrigger("roll");
                    timer = 0f;
                }
                if (body.transform.localScale == new Vector3(-1, 1, 1))
                {
                    body.transform.position = new Vector2(body.transform.position.x - TP, body.transform.position.y);
                    anim.SetTrigger("roll");
                    timer = 0f;
                }
            }            
        }     
    }

}
