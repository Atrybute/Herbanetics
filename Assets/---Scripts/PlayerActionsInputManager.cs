using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionsInputManager : Singleton<PlayerActionsInputManager>, PlayerControls.IPlayerActions
{
    #region Input Variables
    public Vector2 MovementValue { get; private set; }
    public bool IsMeleeAttacking { get; private set; }

    private PlayerControls playerControls;
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        playerControls =  new PlayerControls();
        playerControls.Player.SetCallbacks(this);
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.RemoveCallbacks(this);
        playerControls.Disable();
    }
    #endregion

    #region Player Action Controls
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        else
        {
            ActionAndEventsManager.Instance.playerInputActions.DashEvent();
        }       
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
      MovementValue = context.ReadValue<Vector2>();  
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           IsMeleeAttacking = true;
        }
        else
        {   
            IsMeleeAttacking = false;
            //ActionAndEventsManager.Instance.playerInputActions.PlayerMeleeAttackEvent();
        }
    }

    public void OnSmolBolts(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        else
        {
            ActionAndEventsManager.Instance.playerInputActions.SmolBoltsEvent();
        }
    }

    public void OnStompFront(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        else
        {
            ActionAndEventsManager.Instance.playerInputActions.StompFrontEvent();
        }
    }

    #endregion
}
