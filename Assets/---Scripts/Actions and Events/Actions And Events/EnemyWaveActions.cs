using System;

public class EnemyWaveActions 
{
    public event Action<int, int> OnWaveTextStart;

    public void InitializeWaveTextEvent(int currentWave, int totalWaves)
    {
        OnWaveTextStart?.Invoke(currentWave, totalWaves);
    }

    public event Action<int, int> OnWaveTextUpdate;

    public void UpdateWaveTextEvent(int currentWave, int totalWaves)
    {
        OnWaveTextUpdate?.Invoke(currentWave,  totalWaves);
    }

    public event Action OnWavesCompleted;

    public void WaveCompletedEvent()
    {
        OnWavesCompleted?.Invoke();
    }

    public event Action OnEnemyDefeated;

    public void EnemyDefeatedEvent()
    {
        OnEnemyDefeated?.Invoke();
    }
}
