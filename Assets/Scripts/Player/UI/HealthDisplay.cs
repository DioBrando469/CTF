using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField] GameObject player;
    HealthManager currentHealth;
    [SerializeField] TextMeshProUGUI curHealth;
    [SerializeField] int curHp;
    [SerializeField] TextMeshProUGUI maxHealth;
    [SerializeField] int mHp;

    void Start()
    {
        currentHealth = player.GetComponent<HealthManager>();
        mHp = currentHealth.ReturnMaxHealth();
    }

    void Update()
    {
        curHp = currentHealth.ReturnHealth();
        curHealth.text = System.Convert.ToString(curHp);
        maxHealth.text = System.Convert.ToString(mHp);
    }
}
