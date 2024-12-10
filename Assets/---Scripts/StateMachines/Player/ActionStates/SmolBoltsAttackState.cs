using UnityEngine;

public class SmolBoltsAttackState : PlayerBaseState
{
    private readonly int SmolBoltsAttackHash = Animator.StringToHash("Shooting");
    private readonly string smolboltsAttackTag = "Attack";
    //private readonly float animationTransitionTime = 0.1f;
    public SmolBoltsAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log($"Player just entered the: {stateMachine.currentState}", stateMachine);
       // stateMachine.PlayerAnimator.CrossFadeInFixedTime(smolboltsAttackTag, animationTransitionTime);
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
        float normalizedTime = GetNormalizedTime(stateMachine.PlayerAnimator, smolboltsAttackTag);


        if (normalizedTime < 1f)
        {
            stateMachine.PlayerAnimator.Play(SmolBoltsAttackHash);
        }
        else
        {

            stateMachine.SwitchState(new PlayerFreelookState(stateMachine));

        }
    }
}
