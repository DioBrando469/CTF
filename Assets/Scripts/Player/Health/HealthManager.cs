using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int playerHealth;
    [SerializeField] int maxPlayerHealth;
    void Start()
    {
        playerHealth = maxPlayerHealth;
    }
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }
    public void TakeHealing(int healing)
    {
        playerHealth += healing;
    }
    public int ReturnHealth()
    {
        return playerHealth;
    }
    public int ReturnMaxHealth()
    {
        return maxPlayerHealth;
    }
}
