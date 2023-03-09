using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

     public Vector2 offset = new Vector2(0f, 0f);

    Transform Player;
    bool FaceRight;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void LateUpdate()
    {
        if (Player.transform.localScale == new Vector3(1, 1, 1))
        {
            transform.position = new Vector3(Player.position.x, Player.position.y + offset.y, -10);
        }
        else
            transform.position = new Vector3(Player.position.x, Player.position.y + offset.y, -10);
    }
}
