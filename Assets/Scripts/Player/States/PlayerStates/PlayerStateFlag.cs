using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFlag : PlayerStateBase
{
    GameObject Object;
    PlayerStateManager PlayerManager;
    HealthManager Health;
    public override void EnterState(PlayerStateManager player)
    {
        print("you are carrying a flag");
        Object = player.gameObject;
        try
        {
            Health = Object.GetComponent<HealthManager>();
        }
        catch
        {
            Health = null;
        }
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if(Health != null)
        {
            if(Health.ReturnHealth() <= 0)
            {
                player.SwitchState(player.DeadState);
            }
        }
    }
}
