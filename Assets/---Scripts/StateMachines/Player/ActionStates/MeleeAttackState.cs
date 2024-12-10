using UnityEngine;

public class MeleeAttackState : PlayerBaseState
{
    private readonly string meleeAttackTag = "Attack";
    readonly MeleeAttackConfig currentAttack;
    public MeleeAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        currentAttack = stateMachine.MeleeAttackConfig[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.PlayerAnimator.CrossFadeInFixedTime(currentAttack.AnimationName, currentAttack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        MoveZero(deltaTime);
        AttackCheck();
        UpdateRotation(deltaTime);
    }

    public override void Exit()
    {
        
    }
    private void TryComboAttack(float normalizedTime)
    {
        if (currentAttack.ComboStateIndex == -1) { return; }

        if (normalizedTime < currentAttack.ComboAttackTime) { return; }

        stateMachine.SwitchState
        (
           new MeleeAttackState(stateMachine, currentAttack.ComboStateIndex)
        );
    }

    private void AttackCheck()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.PlayerAnimator, meleeAttackTag);


        if (normalizedTime < 1f)
        {
            if (stateMachine.PlayerInputManager.IsMeleeAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
           
            
             stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
            

        }

       
    }
}
