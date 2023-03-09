using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject rope;
    [SerializeField] private List<GameObject> masRope = new List<GameObject>();
    [SerializeField] private List<float> masDist = new List<float>();
    [SerializeField] private List<float> masDist2 = new List<float>();
    [SerializeField] public GameObject[] Cell;
    [SerializeField] private GameObject startPos;
    public float healthCell = 0f;
    public bool ropeLine = true;
    public bool activeCheckpoint;

    public void Start()
    {
        transform.position = startPos.transform.position;
        float dist = Vector2.Distance(startPos.transform.position, transform.position);        
        for (int i = 0; i < 150; i++)
        {
            masDist.Add(dist += 0.02f);
            Debug.Log(masDist[i]);
        }
    }

    public void Update()
    {
        float dist = Vector2.Distance(startPos.transform.position, transform.position);
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
    
    public void CreateRopeLine()
    {
        if (ropeLine && !activeCheckpoint)
        {            
            GameObject rope = Instantiate(this.rope, transform.position, Quaternion.identity) as GameObject;            
            masRope.Add(rope);
            for (int i = 0; i < masRope.Count; i++)
            {
                masRope[i].GetComponent<HingeJoint2D>().anchor = new Vector2(0.5f, 0f);
                masRope[i].GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0.001000007f, -0.07917771f);                               
                if (masRope.Count > 1)
                {
                    masRope[0].GetComponent<HingeJoint2D>().connectedBody = startPos.GetComponent<Rigidbody2D>();
                    masRope[i + 1].GetComponent<HingeJoint2D>().connectedBody = masRope[i].GetComponent<Rigidbody2D>();
                    this.gameObject.GetComponent<HingeJoint2D>().connectedBody = masRope[masRope.Count - 1].GetComponent<Rigidbody2D>();                    
                }                
            }
        }
    }

    public void DeleteRopeLine()
    {
        if (ropeLine && !activeCheckpoint)
        {
            Destroy(masRope[0].gameObject);
            masRope.RemoveAt(0);
            for (int i = 0; i < masRope.Count; i++)
            {
                if (masRope.Count == 0)
                {
                    this.gameObject.GetComponent<HingeJoint2D>().connectedBody = startPos.GetComponent<Rigidbody2D>();
                }
                if (masRope.Count == 1)
                {
                    masRope[i].GetComponent<HingeJoint2D>().connectedBody = startPos.GetComponent<Rigidbody2D>();
                    this.gameObject.GetComponent<HingeJoint2D>().connectedBody = masRope[i].GetComponent<Rigidbody2D>();
                }
                if (masRope.Count > 1)
                {
                    this.gameObject.GetComponent<HingeJoint2D>().connectedBody = masRope[masRope.Count - 1].GetComponent<Rigidbody2D>();
                    masRope[i + 1].GetComponent<HingeJoint2D>().connectedBody = masRope[i].GetComponent<Rigidbody2D>();
                    masRope[0].GetComponent<HingeJoint2D>().connectedBody = startPos.GetComponent<Rigidbody2D>();
                }
            }
        }
    }

    public void ActiveCheckpoint()
    {
        if (ropeLine)
        {
            foreach (GameObject i in Cell)
            {
                i.SetActive(true);
            }
            activeCheckpoint = true;
            Invoke("DeactivatedCells", 0.5f);
        }
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

    public void CreateRope()
    {
        
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
}
