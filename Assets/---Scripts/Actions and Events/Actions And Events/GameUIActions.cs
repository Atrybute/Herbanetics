using System;

public class GameUIActions 
{
    public event Action OnGameRestartUI;

    public void GameRestartUIEvent()
    {
        OnGameRestartUI?.Invoke();
    }
   
}
