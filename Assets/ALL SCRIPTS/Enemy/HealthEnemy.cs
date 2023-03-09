using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    public int startHealth;
    public int startArmor;
    private int currentHealth;
    public int currentArmor;

    void Start()
    {
        currentHealth = startHealth;
        currentArmor = startArmor;
    }

    
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(enemy);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startHealth);
    }

    public void TakeDamageArmor(int takeDamageArmor)
    {
        currentArmor = Mathf.Clamp(currentArmor - takeDamageArmor, 0, startArmor);
    }
}
