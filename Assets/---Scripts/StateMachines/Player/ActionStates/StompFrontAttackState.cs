using UnityEngine;

public class StompFrontAttackState : PlayerBaseState
{
    private readonly int StompAttackHash = Animator.StringToHash("Stomp");
    private readonly string stompAttackTag = "Attack";
    private readonly float animationTransitionTime = 0.1f;
    public StompFrontAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log($"Player just entered the: {stateMachine.currentState}", stateMachine);
        stateMachine.PlayerAnimator.CrossFadeInFixedTime(StompAttackHash, animationTransitionTime);
    }

   
    public override void Tick(float deltaTime)
    {
        UpdateRotation(deltaTime);
        AttackCheck();
    }
    public override void Exit()
    {

    }


    private void AttackCheck()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.PlayerAnimator, stompAttackTag);


        if (normalizedTime < 1f)
        {
            stateMachine.PlayerAnimator.Play(StompAttackHash);
        }
        else
        {

            stateMachine.SwitchState(new PlayerFreelookState(stateMachine));  

        }
    }
}
