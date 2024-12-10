using System;

public class PlayerHealthActions 
{
    public event Action<int> OnHealthFillStart;

    public void FillPlayerHealth(int maxHealth)
    {
        OnHealthFillStart?.Invoke(maxHealth);
    }

    public event Action<float> OnHealthIncrease;

    public void IncreaseHealth(float healthPercentageIncrease)
    {
        OnHealthIncrease?.Invoke(healthPercentageIncrease);
    }

    public event Action OnHealthDepleted;

    public void PlayerDeathEvent()
    {
        OnHealthDepleted?.Invoke();
    }
}
