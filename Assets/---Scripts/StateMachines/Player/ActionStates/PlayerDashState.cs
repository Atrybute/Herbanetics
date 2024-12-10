using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private readonly int dashHash = Animator.StringToHash("Dash");
    private readonly string dashTag = "Dash";
    private float animationTransitionTime = 0.1f;
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.PlayerAnimator.CrossFadeInFixedTime(dashHash, animationTransitionTime);
        Debug.Log($"Player just entered the: {stateMachine.currentState}", stateMachine);
    }

 
    public override void Tick(float deltaTime)
    {
        MoveZero(deltaTime);
        AttackCheck();
    }

    public override void Exit()
    {

    }
    private void AttackCheck()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.PlayerAnimator, dashTag);


        if (normalizedTime < 1f)
        {
            stateMachine.PlayerAnimator.Play(dashHash);
        }
        else
        {


            stateMachine.SwitchState(new PlayerFreelookState(stateMachine));


        }


    }
}
