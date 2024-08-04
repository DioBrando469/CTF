using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : PlayerStateManager
{
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
}
