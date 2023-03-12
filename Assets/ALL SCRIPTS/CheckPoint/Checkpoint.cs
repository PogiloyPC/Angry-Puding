using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject rope;
    [SerializeField] public GameObject[] Cell;
    [SerializeField] private GameObject startPos;
    [SerializeField] private HingeJoint2D hingeCheckP;
    [SerializeField] private DistanceJoint2D distCheckP;
    [SerializeField] private LayerMask layerMask;
    private Rigidbody2D rbStartP;
    private moving hero;
    public float healthCell = 0f;
    public bool ropeLine;
    public bool activeCheckpoint;   
    [Header("ListDistance")]
    private List<float> masDist = new List<float>();
    private List<float> masDist2 = new List<float>();
    [Header("ListRope")]
    [SerializeField] private List<HingeJoint2D> masRope = new List<HingeJoint2D>();
    private List<SpriteRenderer> colorRope = new List<SpriteRenderer>();
    private List<Rigidbody2D> rbRope = new List<Rigidbody2D>();
    [Header("RayCastDist")]
    public float rayDistance;
    public float rayDistanceY;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    public void Start()
    {
        rbStartP = startPos.GetComponent<Rigidbody2D>();
        hero = GameObject.Find("Hero").GetComponent<moving>();
        transform.position = startPos.transform.position;
        float dist = Vector2.Distance(startPos.transform.position, transform.position);        
        for (int i = 0; i < 150; i++)
        {
            masDist.Add(dist += 0.06f);            
        }
    }

    private void Update()
    {
    }

    public void FixedUpdate()
    {        
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.up * rayDistanceY * transform.localScale.y * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z),
        0, Vector2.left, 0, LayerMask.GetMask("Ground"));
        if (hit.collider.gameObject.GetComponent<Ground>() && ropeLine)
        {
            distCheckP.connectedAnchor = hit.point - new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y);
            distCheckP.autoConfigureDistance = true;
        }
        if (!ropeLine)
        {
            distCheckP.autoConfigureDistance = false;
            distCheckP.distance = 0.001f;
        }
        FindDistance();
    }

    public void FindDistance()
    {
        float dist = Vector2.Distance(startPos.transform.position, transform.position);
        if (ropeLine && hero.movingHero == true)
        {
            for (int i = 0; i < masDist.Count; i++)
            {
                if (masDist[i] < dist)
                {
                    masDist2.Add(masDist[i]);
                    masDist.RemoveAt(i);
                    CreateRopeLine();
                }
            }
            for (int i = 0; i < masDist2.Count; i++)
            {
                if (masDist2[i] > dist)
                {
                    masDist.Add(masDist2[i]);
                    masDist2.RemoveAt(i);
                    DeleteRopeLine();
                }
            }
        }
    }   
       
    public void CreateRopeLine()
    {
        GameObject rope = Instantiate(this.rope, transform.position, Quaternion.identity) as GameObject;                        
        masRope.Add(rope.GetComponent<HingeJoint2D>());
        colorRope.Add(rope.GetComponent<SpriteRenderer>());
        rbRope.Add(rope.GetComponent<Rigidbody2D>());
        if (ropeLine && !activeCheckpoint)
        {            
            for (int i = 0; i < masRope.Count; i++)
            {                
                if (masRope.Count < 1)
                {
                    hingeCheckP.connectedBody = rbStartP;
                }
                if (masRope.Count > 1)
                {
                    masRope[0].connectedBody = rbStartP;
                    masRope[i + 1].connectedBody = rbRope[i];
                    hingeCheckP.connectedBody = rbRope[rbRope.Count - 1];                    
                }                
                if (masRope.Count < 135 && colorRope[i].color != Color.green)
                {
                    colorRope[i].color = Color.green;
                }
                if (masRope.Count >= 135)
                {
                    ColorRope();
                }               
            }
        }
    }    

    public void DeleteRopeLine()
    {
        Destroy(masRope[0].gameObject);
        masRope.RemoveAt(0);
        rbRope.RemoveAt(0);
        colorRope.RemoveAt(0);
        if (ropeLine && !activeCheckpoint)
        {
            for (int i = 0; i < masRope.Count; i++)
            {
                if (masRope.Count < 1)
                {
                    hingeCheckP.connectedBody = rbStartP;
                }               
                if (masRope.Count >= 1)
                {
                    hingeCheckP.connectedBody = rbRope[rbRope.Count - 1];
                    masRope[i + 1].connectedBody = rbRope[i];
                    masRope[0].connectedBody = rbStartP;
                }
            }
        }
    }

    public void ColorRope()
    {       
        for (int i = 0; i < masRope.Count; i++)
        {

            if (masRope.Count >= 135 && masRope.Count < 139)
            {
                colorRope[i].color = new Color(176, 255, 0);
            }
            else if (masRope.Count >= 139 && masRope.Count < 142)
            {
                colorRope[i].color = new Color(253, 255, 0);
            }
            else if (masRope.Count >= 142 && masRope.Count < 145)
            {
                colorRope[i].color = new Color(255, 185, 0);
            }
            else if (masRope.Count >= 145 && masRope.Count < 149)
            {
                colorRope[i].color = new Color(255, 100, 0);
            }        
            else if (masRope.Count >= 149)
            {
                colorRope[i].color = new Color(255, 5, 0);
            }
        }        
    }

    public void ActiveCheckpoint()
    {       
        foreach (GameObject i in Cell)
        {
            i.SetActive(true);
        }
        activeCheckpoint = true;
        Invoke("DeactivatedCells", 0.5f);        
    }

    public void DeactivatedCells()
    {
        foreach(GameObject i in Cell)
        {
            i.SetActive(false);            
        }
    }

    public void TakeDamage(float damage)
    {
        healthCell = Mathf.Clamp(healthCell - damage, 0f, 5f);
        if (healthCell < 1f)
        {
            activeCheckpoint = false;
        }
        Debug.Log(healthCell);
    }   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && activeCheckpoint == true)
        {
            if (healthCell == 5f)
            {
                Cell[0].SetActive(true);
                Cell[1].SetActive(true);
                Cell[2].SetActive(true);
                Cell[3].SetActive(true);
                Cell[4].SetActive(true);
            }
            if (healthCell == 4f)
            {
                Cell[0].SetActive(true);
                Cell[1].SetActive(true);
                Cell[2].SetActive(true);
                Cell[3].SetActive(true);
            }
            if (healthCell == 3f)
            {
                Cell[0].SetActive(true);
                Cell[1].SetActive(true);
                Cell[2].SetActive(true);               
            }
            if (healthCell == 2f)
            {
                Cell[0].SetActive(true);
                Cell[1].SetActive(true);               
            }
            if (healthCell == 1f)
            {
                Cell[0].SetActive(true);                              
            }                                       
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Player" && activeCheckpoint == true)
       {
            foreach (GameObject i in Cell)
            {
                i.SetActive(false);
            }            
       }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.up * rayDistanceY * transform.localScale.y * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y * rayDistanceY, boxCollider.bounds.size.z));
    }
}
