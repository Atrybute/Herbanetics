using System;

public class ScoreActions
{
    public event Action<int> OnAddScore;

    public void AddScore(int score)
    {
        OnAddScore?.Invoke(score);
    }

   
}
