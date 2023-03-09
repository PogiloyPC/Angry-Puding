using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActive : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public Image activeCheckpointScale;
    public float activeCheckpointTimer = 0;
    public bool activeTimer;

    void Start()
    {        
    }
    
    void Update()
    {    
        if (activeTimer == true)
        {
            activeCheckpointTimer += 1f * Time.deltaTime;
            activeCheckpointScale.fillAmount = activeCheckpointTimer / 3f;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        activeTimer = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        activeTimer = false;
        if (activeCheckpointTimer < 3f)
        {            
            activeCheckpointTimer = 0f;
            activeCheckpointScale.fillAmount = 0f;
        }
    }
}
