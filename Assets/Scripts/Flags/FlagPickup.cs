using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{
    TeamAssign playerTeam;
    [SerializeField] GameObject basePosition;
    [SerializeField] int team;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerTeam = collision.gameObject.GetComponent<TeamAssign>();
            if(playerTeam != null && playerTeam.ReturnTeam() != team)
            {
                playerTeam.FlagPicked();
            }
        }
    }
}
