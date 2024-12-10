using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    private readonly int playerDeathHash = Animator.StringToHash("Death");
    private readonly float transitionTime = 0.25f;

    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log($"Player just entered the: {stateMachine.currentState}", stateMachine);
        stateMachine.PlayerAnimator.CrossFadeInFixedTime(playerDeathHash, transitionTime);
    }

 
    public override void Tick(float deltaTime)
    {   

        MoveZero(deltaTime);
    }

    public override void Exit()
    {

    }
}
