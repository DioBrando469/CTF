using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : PlayerStateBase
{
    public override void EnterState(PlayerStateManager player)
    {
        print("you're dead lol");
    }
    public override void UpdateState(PlayerStateManager player)
    {

    }
}
