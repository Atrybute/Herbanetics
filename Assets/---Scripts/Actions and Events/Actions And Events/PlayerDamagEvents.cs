using System;

public class PlayerDamageActions
{
    public event Action<float> OnPlayerHit;
    
    public void PlayerHitEvent(float damage)
    {
        OnPlayerHit?.Invoke(damage);
    }
}
