using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAssign : MonoBehaviour
{
    [SerializeField] PlayerStateManager PlayerManager;
    [SerializeField] int team;
    void Start()
    {
        PlayerManager = gameObject.GetComponent<PlayerStateManager>();
    }
    public int ReturnTeam()
    {
        return team;
    }
    public void FlagPicked()
    {
        if(PlayerManager != null)
            PlayerManager.isFlagPickedUp = true;
    }
}
