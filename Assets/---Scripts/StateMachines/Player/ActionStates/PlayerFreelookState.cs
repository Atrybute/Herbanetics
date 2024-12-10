using UnityEngine;

public class PlayerFreelookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float AnimatorDampTime = 0.1f;
    private const float idleBlendValue = 0f;
    private const float runBlendValue = 1f;
    bool hasBeenCalled;
    public PlayerFreelookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log($"Player just entered the: {stateMachine.currentState}", stateMachine);
        ActionAndEventsManager.Instance.playerInputActions.OnPlayerStompFront += SwitchToStompFrontAttackState;
        ActionAndEventsManager.Instance.playerInputActions.OnPlayerSmolBolts += SwitchToSmolBoltsAttackState;
        ActionAndEventsManager.Instance.playerInputActions.OnPlayerDash += SwitchToDashState;
    }
    public override void Tick(float deltaTime)
    {
        if (!hasBeenCalled)
        {
            stateMachine.PlayerAnimator.CrossFadeInFixedTime(FreeLookBlendTreeHash, AnimatorDampTime);
            hasBeenCalled = true;
        }

        SwitchToMeleeAttackState();
        Vector3 movement = CalculateMovement();
        float movementMagnitude = movement.magnitude;
        Move(movement * stateMachine.MovementSpeed, deltaTime);

        UpdateFreeLookBlend(movementMagnitude, deltaTime, movement);
    }
    public override void Exit()
    {
        ActionAndEventsManager.Instance.playerInputActions.OnPlayerStompFront -= SwitchToStompFrontAttackState;
        ActionAndEventsManager.Instance.playerInputActions.OnPlayerSmolBolts -= SwitchToSmolBoltsAttackState;
        ActionAndEventsManager.Instance.playerInputActions.OnPlayerDash -= SwitchToDashState;
    }


    private void UpdateFreeLookBlend(float movementMagnitude, float deltaTime, Vector3 movement)
    {
        if (stateMachine.PlayerInputManager.MovementValue == Vector2.zero)
        {
            stateMachine.PlayerAnimator.SetFloat(FreeLookSpeedHash, idleBlendValue, AnimatorDampTime, deltaTime);
            return;
        }
        else
        {
            stateMachine.PlayerAnimator.SetFloat(FreeLookSpeedHash, runBlendValue, AnimatorDampTime, deltaTime);
            FacemovementDirection(movement, deltaTime);
        }
    }
    private void FacemovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSmoothValue);
    }
}
