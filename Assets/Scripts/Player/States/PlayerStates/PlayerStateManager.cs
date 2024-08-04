using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public GameObject PlayerObject;
    public bool isFlagPickedUp;
    public PlayerStateBase currentState;
    public PlayerStateAlive AliveState;
    public PlayerStateDead DeadState;
    public PlayerStateFlag FlagState;

    void Start()
    {
        isFlagPickedUp = false;

        PlayerObject = this.gameObject;
        AliveState = new PlayerStateAlive();
        DeadState = new PlayerStateDead();
        FlagState = new PlayerStateFlag();

        currentState = AliveState;
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(PlayerStateBase state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
