using System;

public class PlayerInputActions 
{
    public event Action OnPlayerDash;

    public void DashEvent()
    {
        OnPlayerDash?.Invoke();
    }

    public event Action OnPlayerSmolBolts;

    public void SmolBoltsEvent()
    {
        OnPlayerSmolBolts?.Invoke();
    }

    public event Action OnPlayerStompFront;

    public void StompFrontEvent()
    {
        OnPlayerStompFront?.Invoke();
    }

    public event Action OnPlayerMeleeAttack;

    public void PlayerMeleeAttackEvent()
    {
        OnPlayerMeleeAttack?.Invoke();
    }

    public event Action OnPlayerMove;

    public void MoveEvent()
    {
        OnPlayerMove?.Invoke();
    }
}
