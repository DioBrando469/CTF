using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxhealth;
    public int currenthealth;
    
    void Start()
    {
        currenthealth = maxhealth;
    }

    void Update()
    {
        if(currenthealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
    }
}
