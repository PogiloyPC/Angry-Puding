using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private Image H1;
    [SerializeField] private Image H2;
    [SerializeField] private Image H3;
    [SerializeField] private Transform startPos;
    [SerializeField] private Collider2D[] Check;
    private SpriteRenderer spritePlayer;
    private moving move;
    private Vector3 size;
    public float currentHealth;
    public float startTimerImmortality;
    private float finishTimerImmortality;
    public float startTimerSpriteChange;
    private float finishTimerSpriteChange;
    [Header("Checkpoint")]    
    [SerializeField] private GameObject buttonActiveCheckpoint;
    [SerializeField] private ButtonActive button;
    public float activeCheckpointTimer;
    public float distanceButton;
    public float radiusCircle;
    public bool checkSafePoint;

    void Start()
    {
        move = GetComponent<moving>();       
        spritePlayer = GetComponent<SpriteRenderer>();
        currentHealth = health;        
        size = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.position = startPos.position;       
    }

    public void Update()
    {
        if (finishTimerSpriteChange >= 0f)
        {
            spritePlayer.color = Color.white;
            finishTimerSpriteChange -= 1f * Time.deltaTime;
        }
        if (finishTimerImmortality >= 0f)
        {
            Immortality();
            finishTimerImmortality -= 1f * Time.deltaTime;
        }               
        checkSafePoint = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 0.12f), radiusCircle, LayerMask.GetMask("Checkpoint"));
        Collider2D check = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 0.12f), radiusCircle, LayerMask.GetMask("Checkpoint"));
        if (checkSafePoint == true && check.GetComponent<Checkpoint>().activeCheckpoint == false && check.GetComponent<Checkpoint>().ropeLine == true)
        {
            buttonActiveCheckpoint.SetActive(true);
            buttonActiveCheckpoint.transform.position = new Vector2(check.transform.position.x, check.transform.position.y + distanceButton);
            if (button.activeCheckpointTimer >= 3f)
            {
                ActiveCheckPoint();
            }
        }
        else
        {
            buttonActiveCheckpoint.SetActive(false);
        }
    }   

    public void ActiveCheckPoint()
    {       
        for (int i = 0; i < 1; i++)
        {
            Check[2] = Check[1];
            Check[1] = Check[0];
            Check[0] = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 0.12f), radiusCircle, LayerMask.GetMask("Checkpoint"));            
        }        
        Collider2D check = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 0.12f), radiusCircle, LayerMask.GetMask("Checkpoint"));
        check.GetComponent<Checkpoint>().ActiveCheckpoint();        
        check.GetComponent<Checkpoint>().TakeDamage(-5);
        buttonActiveCheckpoint.SetActive(false);
        button.activeTimer = false;
        button.activeCheckpointTimer = 0f;
        button.activeCheckpointScale.fillAmount = 0f;
    }

    public void Immortality()
    {
        if (finishTimerSpriteChange < 0f)
        {
            spritePlayer.color = Color.red;
            Invoke("SpriteRed", 0.1f);
        }
    }
    
    public void SpriteRed()
    {
        finishTimerSpriteChange = startTimerSpriteChange;
    }

    public void KillPlayer()
    {
        move.enabled = false;
        if (Check[0] != null && Check[0].GetComponent<Checkpoint>().activeCheckpoint == true)
        {           
            Check[0].GetComponent<Checkpoint>().TakeDamage(1f);
            for (int i = 0; i < 1; i++)
            {
                if (Check[i].GetComponent<Checkpoint>().healthCell < 1f)
                {
                    Check[i] = null;
                }
                if (Check[0] == null)
                {               
                    Check[0] = Check[1];
                    Check[1] = Check[2];
                    Check[2] = null;
                }                
            }
        }
        Invoke("Respawn", 2f);        
    }

    public void ReloadHealthImage()
    {
        if (currentHealth <= 2f)
        {
            H1.enabled = false;
        }
        if (currentHealth <= 1f)
        {
            H2.enabled = false;
        }
        if (currentHealth <= 0f)
        {            
            H3.enabled = false;
            KillPlayer();
        }
    }

    public void TakeDamage(float _damage)
    {
        if (finishTimerImmortality < 0f)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, health);
            ReloadHealthImage();
            finishTimerImmortality = startTimerImmortality;
        }
    }

    public void Respawn()
    {
        H3.enabled = true;        
        H2.enabled = true;        
        H1.enabled = true;            
        move.enabled = true;                                   
        currentHealth = health;                                     
        if (Check[0] != null)
        {        
            transform.position = Check[0].gameObject.transform.position;            
        }
        else
        {
            transform.position = startPos.position;
        }        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 0.12f), radiusCircle);
    }
}
