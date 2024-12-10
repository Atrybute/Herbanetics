using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    #region Utility Functions
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReciever.Movement) * deltaTime);
    }

    protected void MoveZero(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void UpdateRotation(float deltaTime)
    {
        if (stateMachine.PlayerInputManager.MovementValue.y == 0 && stateMachine.PlayerInputManager.MovementValue.x == 0) return;

        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movementDir = forward * stateMachine.PlayerInputManager.MovementValue.y + right * stateMachine.PlayerInputManager.MovementValue.x;

        movementDir.Normalize();

        Quaternion toRot = Quaternion.LookRotation(movementDir, Vector3.up);

        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, toRot, deltaTime * stateMachine.PlayerRotationValue);

    }

    protected Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.PlayerInputManager.MovementValue.y + right * stateMachine.PlayerInputManager.MovementValue.x;
    }

    protected void LookAtTarget(Transform target)
    {
        // Rotate towards the target smoothly
        Quaternion targetRotation = Quaternion.LookRotation(target.position - stateMachine.transform.position);
        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, Time.deltaTime * stateMachine.PlayerRotationValue);
    }

    #endregion

    #region Attack Switch Functions
    protected void SwitchToStompFrontAttackState()
    {
        if (stateMachine.CombatManager.CanStomp)
        {
            stateMachine.SwitchState(new StompFrontAttackState(stateMachine));
        }
       
    }

    protected void SwitchToSmolBoltsAttackState()
    {   
        if(stateMachine.CombatManager.CanShoot)
        {
            stateMachine.SwitchState(new SmolBoltsAttackState(stateMachine));

        }
       
    }

    protected void SwitchToMeleeAttackState()
    {
        if (stateMachine.PlayerInputManager.IsMeleeAttacking)
        {
          
            stateMachine.SwitchState(new MeleeAttackState(stateMachine, 0));
            return;
        }
    }

    protected void SwitchToDashState()
    {
        if (stateMachine.CombatManager.CanDash)
        {
            stateMachine.SwitchState(new PlayerDashState(stateMachine));
        }
       
    }

    #endregion
}
