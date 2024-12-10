using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float AnimatorDampTime = 0.1f;
   
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.PlayerAnimator.CrossFadeInFixedTime(FreeLookBlendTreeHash, AnimatorDampTime);
          
        Debug.Log($"Player just entered the: {stateMachine.currentState}", stateMachine);
    }

    public override void Tick(float deltaTime)
    {
        MoveZero(deltaTime);
    }

    public override void Exit()
    {

    }
}
