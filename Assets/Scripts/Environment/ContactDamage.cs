using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    HealthManager player;
    [SerializeField] int damage;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<HealthManager>();
            if(player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
