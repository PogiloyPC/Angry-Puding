using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timerDestroy;

    void Update()
    {
        Destroy(gameObject, timerDestroy);
    }
}
